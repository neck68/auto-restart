using AutoRestarter.Util;
using System.ComponentModel;

namespace AutoRestarter.Managers;

public class BackgroundManager(FormManager formManager)
{
    private FormManager formManager = formManager;

    public BackgroundWorker RefreshWorker { get; set; } = new BackgroundWorker();
    public BackgroundWorker ConnectWorker { get; set; } = new BackgroundWorker();

    public void InitializeBackgroundWorker()
    {
        RefreshWorker = new BackgroundWorker();
        RefreshWorker.DoWork += new DoWorkEventHandler(RefreshWorker_DoWork);
        RefreshWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RefreshWorker_RunWorkerCompleted);

        ConnectWorker = new BackgroundWorker();
        ConnectWorker.DoWork += new DoWorkEventHandler(ConnectWorker_DoWork);
        ConnectWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ConnectWorker_RunWorkerCompleted);
    }

    private void RefreshWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        FormManager.AccountNumbers.Clear();

        int itemCount = 0;

        formManager.AccountBox.Invoke((MethodInvoker)delegate {
            itemCount = formManager.AccountBox.Items.Count;
        });

        List<int> newAccountNumbers = [];

        for (int i = 0; i < itemCount; i++)
        {
            ListViewItem? listViewItem = null;

            formManager.AccountBox.Invoke((MethodInvoker)delegate {
                listViewItem = formManager.AccountBox.Items[i];
            });

            if (listViewItem != null)
            {
                string adbPortText = listViewItem.SubItems[1].Text;

                if (string.IsNullOrWhiteSpace(adbPortText) || adbPortText == "0")
                {
                    newAccountNumbers.Add(0);
                }
                else
                {
                    if (int.TryParse(adbPortText, out int adbPort))
                    {
                        newAccountNumbers.Add(adbPort);
                        FormManager.ActiveAccounts.Add(false);
                    }
                    else
                    {
                        Logger.SendMessage($"Invalid ADB port value for {listViewItem.SubItems[0].Text}. Setting to 0.");
                        newAccountNumbers.Add(0);
                    }
                }

                

            }
        }

        e.Result = newAccountNumbers;
    }

    private void RefreshWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Logger.SendMessage("Error: " + e.Error.Message, ConsoleColor.Red);
        }
        else if (e.Cancelled)
        {
            Logger.SendMessage("Operation was cancelled.", ConsoleColor.Yellow);
        }
        else
        {
            if (e.Result is List<int> newAccountNumbers)
            {
                FormManager.AccountNumbers.Clear();
                FormManager.AccountNumbers.AddRange(newAccountNumbers);

                for (int i = 0; i < FormManager.AccountNumbers.Count; i++)
                {
                    if (i < formManager.AccountBox.Items.Count)
                    {
                        formManager.AccountBox.Items[i].SubItems[1].Text = FormManager.AccountNumbers[i].ToString();
                    }
                    else
                    {
                        Logger.SendMessage($"Index {i} out of bounds for AccountBox items.", ConsoleColor.Red);
                    }
                }

                formManager.ConnectButton.Enabled = true;

                _ = OldADBManager.SendADB("devices");
                Logger.SendMessage("Account numbers refreshed and set to: [ " + string.Join(", ", FormManager.AccountNumbers) + " ]", ConsoleColor.Blue);
            }
        }
    }
    private void ConnectWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        if (FormManager.AccountNumbers == null)
        {
            return;
        }

        for (int i = 0; i < FormManager.AccountNumbers.Count; i++)
        {
            if (FormManager.AccountNumbers[i] != 0)
            {
                OldADBManager.SendRegularADB("connect 127.0.0.1:" + FormManager.AccountNumbers[i]);
            }
        }
    }
    private void ConnectWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Logger.SendMessage("Error: " + e.Error.Message, ConsoleColor.Red);
        }
        else if (e.Cancelled)
        {
            Logger.SendMessage("Connection operation was cancelled.", ConsoleColor.Yellow);
        }
        else
        {
            Logger.SendMessage("All connection attempts have completed successfully.");
            formManager.StartButton.Enabled = true;
        }
    }

}
