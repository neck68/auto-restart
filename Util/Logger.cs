namespace AutoRestarter.Util;

public static class Logger
{
    private static string logFilePath = string.Empty;
    private static readonly object logFileLock = new();

    public static void InitializeLogFilePath()
    {
        string logFolderPath = "logs";
        string baseLogFileName = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
        logFilePath = Path.Combine(logFolderPath, baseLogFileName);

        try
        {
            // Ensure the log folder exists
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // Ensure the log file has a unique name
            int fileCount = 0;
            while (File.Exists(logFilePath))
            {
                fileCount++;
                string newLogFileName = $"log_{DateTime.Now:yyyy-MM-dd}_{fileCount}.txt";
                logFilePath = Path.Combine(logFolderPath, newLogFileName);
            }
        }
        catch (Exception ex)
        {
            SendMessage($"Error initializing log file path: {ex.Message}", ConsoleColor.Red);
        }
    }

    public static void SendMessage(string message, ConsoleColor color = ConsoleColor.White)
    {
        PrintColoredMessage($"{DateTime.Now}: {message}", color);
        WriteToLogFile(message);
    }
    public static void SendMessage(int message, ConsoleColor color = ConsoleColor.White)
    {
        PrintColoredMessage($"{DateTime.Now}: {message}", color);
        WriteToLogFile(message.ToString());
    }
    private static void PrintColoredMessage(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;

        Console.WriteLine(message);

        Console.ForegroundColor = originalForegroundColor;
        Console.BackgroundColor = originalBackgroundColor;
    }

    private static void WriteToLogFile(string message)
    {
        if (string.IsNullOrEmpty(logFilePath))
        {
            lock (logFileLock)
            {
                if (string.IsNullOrEmpty(logFilePath))
                {
                    InitializeLogFilePath();
                }
            }
        }

        try
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}
