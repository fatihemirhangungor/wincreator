using System;

public static class ServiceLib
{
    public static void Initialize()
    {
        // Common initialization logic for services
        Console.WriteLine("ServiceLib initialized for: MySvc1");
    }
n    public static void Cleanup()
    {
        // Cleanup logic
        Console.WriteLine("ServiceLib cleanup for: MySvc1");
    }
}