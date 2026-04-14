using System;

public static class ServiceLib
{
    public static void Initialize()
    {
        // Common initialization logic for services
        Console.WriteLine("ServiceLib initialized for: __SERVICE_NAME__");
    }
n    public static void Cleanup()
    {
        // Cleanup logic
        Console.WriteLine("ServiceLib cleanup for: __SERVICE_NAME__");
    }
}