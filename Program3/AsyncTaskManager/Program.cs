using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    // Asynchronous Method - Task-Based Programming
    // Simulates downloading a file without blocking a thread
    static async Task DownloadFileAsync(string fileName, int seconds)
    {
        Console.WriteLine($"Starting download: {fileName}");

        // Non-Blocking Delay - Async/Await
        await Task.Delay(seconds * 1000);

        Console.WriteLine($"Completed download: {fileName}");
    }

    // Sequential Execution - Tasks Run One After Another
    static async Task RunSequentialAsync()
    {
        Console.WriteLine("\n--- Sequential Downloads ---");

        // Stopwatch - Measuring Execution Time
        Stopwatch timer = Stopwatch.StartNew();

        await DownloadFileAsync("File A", 2);
        await DownloadFileAsync("File B", 3);
        await DownloadFileAsync("File C", 4);

        timer.Stop();

        Console.WriteLine(
            $"\nSequential Time: {timer.Elapsed.TotalSeconds:F2} seconds");
    }

    // Concurrent Execution - Multiple Tasks Can Overlap
    static async Task RunConcurrentAsync()
    {
        Console.WriteLine("\n--- Concurrent Downloads ---");

        Stopwatch timer = Stopwatch.StartNew();

        // Task Objects - Represent Asynchronous Operations
        Task task1 = DownloadFileAsync("File A", 2);
        Task task2 = DownloadFileAsync("File B", 3);
        Task task3 = DownloadFileAsync("File C", 4);

        // Task.WhenAll - Waits Until Every Task Finishes
        await Task.WhenAll(task1, task2, task3);

        timer.Stop();

        Console.WriteLine(
            $"\nConcurrent Time: {timer.Elapsed.TotalSeconds:F2} seconds");
    }

    // Async Main - Asynchronous Program Entry Point
    static async Task Main()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("       C# Asynchronous Task Manager          ");
        Console.WriteLine("=============================================");

        Console.WriteLine("\nThis program compares sequential and");
        Console.WriteLine("concurrent asynchronous operations.");

        Console.WriteLine("\nPress ENTER to start sequential downloads...");
        Console.ReadLine();

        await RunSequentialAsync();

        Console.WriteLine("\nPress ENTER to start concurrent downloads...");
        Console.ReadLine();

        await RunConcurrentAsync();

        Console.WriteLine("\n=============================================");
        Console.WriteLine("            Comparison Complete              ");
        Console.WriteLine("=============================================");

        Console.WriteLine("\nSequential: Tasks wait for previous tasks.");
        Console.WriteLine("Concurrent: Tasks can make progress together.");

        Console.WriteLine("\nPress ENTER to exit...");
        Console.ReadLine();
    }
}
