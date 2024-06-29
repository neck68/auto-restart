using AutoRestarter.Util;
using System.Diagnostics;

namespace AutoRestarter.Managers;

public class OldADBManager
{

    public static async Task SendADB(string command)
    {
        try
        {
            await Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo processInfo = new()
                    {
                        FileName = "adb",
                        Arguments = command,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using Process process = new() { StartInfo = processInfo };
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Logger.SendMessage(e.Data);
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Logger.SendMessage($"Error: {e.Data}", ConsoleColor.Red);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Logger.SendMessage($"Error: {ex.Message}", ConsoleColor.Red);
                }
            });
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error2: {ex.Message}", ConsoleColor.Red);
        }
    }

    public static async Task<bool> IsAppRunning(string device, string packageName)
    {
        bool isRunning = false;
        string command = $"-s {device} shell dumpsys activity activities | grep {packageName}";

        await Task.Run(() =>
        {
            try
            {
                ProcessStartInfo processInfo = new()
                {
                    FileName = "adb",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process? process = Process.Start(processInfo);
                if (process != null)
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(output) && output.Contains(packageName))
                    {
                        isRunning = true;
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        Logger.SendMessage($"Error: {error}", ConsoleColor.Red);
                    }

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Logger.SendMessage($"Error2: {ex.Message}", ConsoleColor.Red);
            }
        });

        return isRunning;
    }

    public static void SendRegularADB(string command)
    {
        try
        {
            ProcessStartInfo processInfo = new()
            {
                FileName = "adb",
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process? process = Process.Start(processInfo);
            if (process != null)
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(output.Trim()))
                {
                    Logger.SendMessage(output);
                }

                if (!string.IsNullOrEmpty(error))
                {
                    Logger.SendMessage($"Error: {error}", ConsoleColor.Red);
                }

                process.WaitForExit();
            }
            else
            {
                Logger.SendMessage("Failed to start the process.", ConsoleColor.Red);
            }
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error: {ex.Message}", ConsoleColor.Red);
        }
    }

    

}
