
using AutoRestarter.Properties;
using AutoRestarter.Util;

namespace AutoRestarter.Managers;

public class TimerManager
{
    private static System.Windows.Forms.Timer? _refreshTimer;

    public static System.Windows.Forms.Timer RefreshTimer
    {
        get
        {
            _refreshTimer ??= new System.Windows.Forms.Timer();
            return _refreshTimer;
        }
    }

    public TimerManager()
    {

    }

    public static void InitializeTimer()
    {
        RefreshTimer.Interval = 5000;
        RefreshTimer.Tick += Timer_Tick;
    }

    private static async void Timer_Tick(object? sender, EventArgs e)
    {
        Logger.SendMessage("Starting emulator tick.");

        if (FormManager.AccountNumbers == null)
        {
            Logger.SendMessage("Developer Error: AccountNumbers is null.", ConsoleColor.Red);
            return;
        }

        if (FormManager.ActiveAccounts == null)
        {
            Logger.SendMessage("Developer Error: ActiveAccounts is null.", ConsoleColor.Red);
            return;
        }

        int accountCount = FormManager.AccountNumbers.Count;

        for (int i = 0; i < accountCount; i++)
        {
            Logger.SendMessage($"Checking account index {i}.", ConsoleColor.Cyan);

            if (i >= FormManager.ActiveAccounts.Count)
            {
                Logger.SendMessage($"Account index {i} is out of range for ActiveAccounts.", ConsoleColor.Red);
                continue;
            }

            if (FormManager.ActiveAccounts[i]) // reverse this later when i finally implement it
            {
                Logger.SendMessage($"Account {i} is inactive.", ConsoleColor.Gray);
                continue;
            }

            if (FormManager.AccountNumbers[i] != 0)
            {
                string device = "127.0.0.1:" + FormManager.AccountNumbers[i];
                Logger.SendMessage($"Checking device {device} for account {i}.", ConsoleColor.Cyan);

                bool isAppRunning = await OldADBManager.IsAppRunning(device, "com.roblox.client");
                if (isAppRunning)
                {
                    if (!StateManager.accountStates.TryGetValue(i, out AppState value))
                    {
                        value = AppState.Idle;
                        StateManager.accountStates[i] = value;
                    }

                    // Use the state specific to this account
                    switch (value)
                    {
                        case AppState.Idle:
                            Logger.SendMessage($"Device {device} (Account {i}): App is running. Transitioning to LoadingScreen state.");
                            StateManager.accountStates[i] = AppState.LoadingScreen;
                            break;
                        case AppState.LoadingScreen:
                            Logger.SendMessage($"Device {device} (Account {i}): Checking for loading screen issues.");
                            await EmguManager.CheckAndKillApp(RefreshTimer, device, "com.roblox.client", i);
                            StateManager.accountStates[i] = AppState.Menu;
                            break;
                        case AppState.Menu:
                            Logger.SendMessage($"Device {device} (Account {i}): Checking for menu issues.");
                            await EmguManager.CheckAndKillApp(RefreshTimer, device, "com.roblox.client", i);
                            StateManager.accountStates[i] = AppState.Idle;
                            break;
                            // Add cases for other states as needed
                    }
                }
                else
                {
                    Logger.SendMessage($"Device {device} (Account {i}): App is not running. Relaunching into private server.", ConsoleColor.Yellow);

                    if (FormManager.WebhookCheckBox != null)
                    {
                        _ = WebhookManager.SendWebhook(i, WebhookManager.GetRejoinWebhook(), FormManager.WebhookCheckBox);
                    }

                    if (FormManager.AccountPrivateServer.TryGetValue(i, out string? privateServerUrl))
                    {
                        
                        await OldADBManager.SendADB($"-s {device} shell am start -a android.intent.action.VIEW -d '{privateServerUrl}' com.roblox.client");
                    }
                    else
                    {
                        
                        Logger.SendMessage($"No private server link found for account {i}.", ConsoleColor.Red);
                    }
                }
            }
            else
            {
                Logger.SendMessage($"Account {i} has an invalid AccountNumber (0).", ConsoleColor.Gray);
            }
        }

        Logger.SendMessage("Emulator tick completed.");
    }

    public static void Hour_ValueChanged(object? sender, EventArgs e)
    {
        if (sender is NumericUpDown numericUpDown)
        {
            Settings.Default.Hours = (int)numericUpDown.Value;
        }

        UpdateTimerInterval();
        Settings.Default.Save();
    }

    public static void Minute_ValueChanged(object? sender, EventArgs e)
    {
        if (sender is NumericUpDown numericUpDown)
        {
            Settings.Default.Minutes = (int)numericUpDown.Value;
        }

        UpdateTimerInterval();
        Settings.Default.Save();
    }

    public static void Second_ValueChanged(object? sender, EventArgs e)
    {
        if (sender is NumericUpDown numericUpDown)
        {
            Settings.Default.Seconds = (int)numericUpDown.Value;
        }

        UpdateTimerInterval();
        Settings.Default.Save();
    }

    private static void UpdateTimerInterval()
    {
        int intervalInSeconds = (Settings.Default.Hours * 3600) + (Settings.Default.Minutes * 60) + Settings.Default.Seconds;

        if (intervalInSeconds <= 0)
        {
            intervalInSeconds = 5;
        }

        RefreshTimer.Interval = intervalInSeconds * 1000;
        string formattedTime = ConvertSecondsToTimeFormat(intervalInSeconds);

        Logger.SendMessage($"Timer interval updated to {formattedTime}.");
    }

    public static void UpdateTimerInterval(NumericUpDown currentSeconds, int newSeconds, NumericUpDown currentMinutes, int newMinutes, NumericUpDown currentHours, int newHour)
    {
        currentSeconds.ValueChanged -= Second_ValueChanged;
        currentMinutes.ValueChanged -= Minute_ValueChanged;
        currentHours.ValueChanged -= Hour_ValueChanged;

        int intervalInSeconds = (newHour * 3600) + (newMinutes * 60) + newSeconds;
        if (intervalInSeconds <= 0)
        {
            intervalInSeconds = 5;
        }
        
        currentSeconds.Value = newSeconds;
        currentMinutes.Value = newMinutes;
        currentHours.Value = newHour;

        currentSeconds.ValueChanged += Second_ValueChanged;
        currentMinutes.ValueChanged += Minute_ValueChanged;
        currentHours.ValueChanged += Hour_ValueChanged;

        RefreshTimer.Interval = intervalInSeconds * 1000;
    }

    public static string ConvertSecondsToTimeFormat(int seconds)
    {
        int hours = seconds / 3600;
        int remainingSeconds = seconds % 3600;
        int minutes = remainingSeconds / 60;
        int remainingSeconds2 = remainingSeconds % 60;

        return $"{hours} hours, {minutes} minutes, {remainingSeconds2} seconds";
    }



}