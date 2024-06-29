using AutoRestarter.Properties;
using AutoRestarter.Util;
using System.Text;

namespace AutoRestarter.Managers;

public class SettingManager()
{
    public static void SaveAccountData(ListView accountBox)
    {
        string accountDataFilePath = Path.Combine("settings", "accountData.txt");

        try
        {
            StringBuilder newData = new();

            foreach (ListViewItem item in accountBox.Items)
            {
                newData.Append("[\"");
                newData.Append(string.Join(", ", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subItem => subItem.Text)));
                newData.Append("\"]\n");
            }

            File.WriteAllText(accountDataFilePath, newData.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the data: " + ex.Message);
        }
    }
    public static void SaveImageData(ListView imageBox)
    {
        string accountDataFilePath = Path.Combine("settings", "imageData.txt");

        try
        {
            StringBuilder newData = new();

            foreach (ListViewItem item in imageBox.Items)
            {
                newData.Append("[\"");
                newData.Append(string.Join(", ", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(subItem => subItem.Text)));
                newData.Append("\"]\n");
            }

            File.WriteAllText(accountDataFilePath, newData.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the data: " + ex.Message);
        }
    }
    public static void LoadTimerData(NumericUpDown currentSeconds, NumericUpDown currentMinutes, NumericUpDown currentHours)
    {
        int seconds = Settings.Default.Seconds;
        int minutes = Settings.Default.Minutes;
        int hours = Settings.Default.Hours;

        if (seconds == minutes && seconds == hours)
        {

        }
        else
        {
            Logger.SendMessage($"Setting timer delay settings: [Hours={Settings.Default.Hours}, Minutes={Settings.Default.Minutes}, Seconds={Settings.Default.Seconds}]", ConsoleColor.Blue);
            TimerManager.UpdateTimerInterval(currentSeconds, Settings.Default.Seconds, currentMinutes, Settings.Default.Minutes, currentHours, Settings.Default.Hours);
        }
    }
    public static void LoadWebhookData(CheckBox discordWebHookToggle)
    {
        discordWebHookToggle.Checked = Settings.Default.WebhookToggle;
    }
    public static void LoadAccountData(ListView accountBox)
    {
        string accountDataFilePath = Path.Combine("settings", "accountData.txt");

        if (!File.Exists(accountDataFilePath))
        {
            Console.WriteLine("File does not exist: " + accountDataFilePath);
            return;
        }

        bool dataLoaded = false;

        try
        {
            accountBox.Items.Clear();
            using (StreamReader reader = File.OpenText(accountDataFilePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("[\"") && line.EndsWith("\"]"))
                    {
                        line = line[2..^2];
                    }

                    string[] parts = line.Split(',').Select(part => part.Trim()).ToArray();
                    if (parts.Length == 4)
                    {
                        accountBox.Items.Add(new ListViewItem(parts));
                        dataLoaded = true;
                    }
                }
            }

            if (dataLoaded)
            {
                Logger.SendMessage("Loaded Account Data.", ConsoleColor.Blue);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while reading the file: " + ex.Message);
        }
    }
    public static void LoadConsoleData(CheckBox consoleCheckBox)
    {
        consoleCheckBox.Checked = Settings.Default.ShowConsole;
    }
    public static void LoadImageData(ListView imageBox)
    {
        string accountDataFilePath = Path.Combine("settings", "imageData.txt");

        if (!File.Exists(accountDataFilePath))
        {
            Console.WriteLine("File does not exist: " + accountDataFilePath);
            return;
        }

        bool dataLoaded = false;

        try
        {
            imageBox.Items.Clear();
            using (StreamReader reader = File.OpenText(accountDataFilePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("[\"") && line.EndsWith("\"]"))
                    {
                        line = line[2..^2];
                    }

                    string[] parts = line.Split(',').Select(part => part.Trim()).ToArray();
                    if (parts.Length == 7)
                    {
                        imageBox.Items.Add(new ListViewItem(parts));
                        dataLoaded = true;
                    }
                }
            }

            if (dataLoaded)
            {
                Logger.SendMessage("Loaded Image Data.", ConsoleColor.Blue);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while reading the file: " + ex.Message);
        }


    }
}
