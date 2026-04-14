param(
    [string]$Out = "$env:TEMP\winSvcScaffoldTest",
    [string]$Names = "TestSvc1,TestSvc2"
)

if (Test-Path $Out) { Remove-Item $Out -Recurse -Force }
New-Item -ItemType Directory -Path $Out -Force | Out-Null
$exe = Join-Path -Path $PSScriptRoot -ChildPath "..\src\WinSvcScaffold\bin\Debug\WinSvcScaffold.exe"
if (-not (Test-Path $exe)) { Write-Host "Build WinSvcScaffold first (msbuild)."; exit 1 }
& $exe --names $Names --out $Out
Write-Host "Scaffolded to $Out"