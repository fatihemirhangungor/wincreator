using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var namesArg = GetArg(args, "--names");
        var outArg = GetArg(args, "--out") ?? Directory.GetCurrentDirectory();
        if (string.IsNullOrEmpty(namesArg))
        {
            Console.WriteLine("Usage: WinSvcScaffold --names ServiceA,ServiceB [--out outputPath]");
            return;
        }
        var names = namesArg.Split(new[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(s=>s.Trim()).ToArray();
        var templateDir = FindTemplateDir();
        foreach (var name in names)
        {
            var dest = Path.Combine(outArg, name);
            Console.WriteLine($"Scaffolding {name} -> {dest}");
            CopyAndReplace(templateDir, dest, name);
        }
    }

    static string GetArg(string[] args, string key)
    {
        for (int i=0;i<args.Length;i++)
        {
            if (args[i].Equals(key, StringComparison.OrdinalIgnoreCase) && i+1<args.Length) return args[i+1];
            if (args[i].StartsWith(key+":")) return args[i].Substring(key.Length+1);
        }
        return null;
    }

    static void CopyAndReplace(string srcDir, string destDir, string serviceName)
    {
        if (!Directory.Exists(srcDir)) { Console.WriteLine($"Template not found: {srcDir}"); return; }
        Directory.CreateDirectory(destDir);
        foreach (var dir in Directory.GetDirectories(srcDir, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dir.Replace(srcDir, destDir));
        }
        var textExt = new[]{".cs",".csproj",".config",".json",".txt",".xml",".ps1",".props",".targets",".sln",".packages.config"};
        foreach (var file in Directory.GetFiles(srcDir, "*", SearchOption.AllDirectories))
        {
            var rel = file.Substring(srcDir.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var destPath = Path.Combine(destDir, rel);
            var ext = Path.GetExtension(file).ToLowerInvariant();
            if (Array.Exists(textExt, e=>e.Equals(ext, StringComparison.OrdinalIgnoreCase)))
            {
                var txt = File.ReadAllText(file);
                txt = txt.Replace("__SERVICE_NAME__", serviceName).Replace("__SERVICE_GUID__", Guid.NewGuid().ToString());
                File.WriteAllText(destPath, txt);
            }
            else
            {
                File.Copy(file, destPath, true);
            }
        }
    }

    static string FindTemplateDir()
    {
        var dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (dir != null)
        {
            var candidate = System.IO.Path.Combine(dir.FullName, "templates", "service-template");
            if (System.IO.Directory.Exists(candidate)) return System.IO.Path.GetFullPath(candidate);
            dir = dir.Parent;
        }
        // Fallback: relative path from exe (may not exist)
        return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "templates", "service-template"));
    }
}