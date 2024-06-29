using AutoRestarter.Util;
using AutoRestarter.Managers;
using AutoRestarter.Properties;
using System.ComponentModel;

namespace AutoRestarter.Core.Forms;

public partial class Main : Form
{
    public FormManager FormManager { get; private set; }
    public TimerManager TimerManager { get; private set; }
    public SettingManager SettingManager { get; private set; }
    public TextBoxManager TextBoxManager { get; private set; }
    public BackgroundManager BackgroundManager { get; private set; }
    public OldADBManager OldADBManager { get; private set; }
    public EmguManager EmguManager { get; private set; }
    public WebhookManager WebhookManager { get; private set; }
    public ImageManager ImageManager { get; private set; }

    public ImageEditor ImageForm { get; private set; } = new();

    private static bool stopped = false;

    public static bool Stopped
    {
        get { return stopped; }
        set { stopped = value; }
    }

    private readonly ColumnHeader usernameColumn;
    private readonly ColumnHeader adbPortColumn;
    private readonly ColumnHeader webhookColumn;
    private readonly ColumnHeader privateServerColumn;
    private TextBox? editBox;
    private int editSubItemIndex;

    public Main()
    {
        InitializeComponent();

        FormManager = new(StartButton, AccountListView, ConnectButton);
        FormManager.WebhookCheckBox = WebhookCheckBox;
        TimerManager = new();
        SettingManager = new();
        TextBoxManager = new();
        BackgroundManager = new(FormManager);
        OldADBManager = new();
        EmguManager = new();
        WebhookManager = new();

        usernameColumn = new ColumnHeader();
        adbPortColumn = new ColumnHeader();
        webhookColumn = new ColumnHeader();
        privateServerColumn = new ColumnHeader();

        MaximizeBox = false;

        FormClosing += Main_Closing;
        Second.ValueChanged += TimerManager.Second_ValueChanged;
        Minute.ValueChanged += TimerManager.Minute_ValueChanged;
        Hour.ValueChanged += TimerManager.Hour_ValueChanged;
    }



    private void Main_Load(object sender, EventArgs e)
    {
        CreateConsole();
        Folder.InitializeFolders();
        InitializeAccountBox();
        TimerManager.InitializeTimer();
        BackgroundManager.InitializeBackgroundWorker();

        SettingManager.LoadAccountData(AccountListView);
        SettingManager.LoadTimerData(Second, Minute, Hour);
        SettingManager.LoadWebhookData(WebhookCheckBox);
        SettingManager.LoadConsoleData(ConsoleCheckBox);

        ConsoleHelper.SetConsoleWindowPositionAndSize(this, 800, 300);

        if (ConsoleCheckBox.Checked != true) 
        {
            ConsoleHelper.HideConsoleWindow();
        }

        ImageForm.Show();
        ImageForm.Close();
        
    }

    private void Main_Closing(object? sender, CancelEventArgs e)
    {
        Logger.SendMessage("Closing...");

        _ = OldADBManager.SendADB("kill-server");

        Settings.Default.Save();
    }

    private static void CreateConsole()
    {
        ConsoleHelper.AllocConsole();
        ConsoleHelper.SetConsoleWindowTitle("Console");
        Logger.SendMessage("Initializing...");
    }
    private void InitializeAccountBox()
    {
        AccountListView.DoubleClick += AccountBox_DoubleClick;

        AccountListView.Columns.AddRange([
            usernameColumn,
            adbPortColumn,
            webhookColumn,
            privateServerColumn]);

        AccountListView.FullRowSelect = true;
        AccountListView.GridLines = false;
        AccountListView.HideSelection = false;
        AccountListView.Location = new Point(12, 12);
        AccountListView.MultiSelect = true;
        AccountListView.Name = "accountBox";
        AccountListView.TabIndex = 0;
        AccountListView.UseCompatibleStateImageBehavior = false;
        AccountListView.View = View.Details;
        AccountListView.LabelEdit = true;
        AccountListView.AllowDrop = true;

        usernameColumn.Text = "Username";
        usernameColumn.Width = 75;

        adbPortColumn.Text = "ADB Port";
        adbPortColumn.Width = 75;

        webhookColumn.Text = "Webhook";
        webhookColumn.Width = 75;

        privateServerColumn.Text = "Private Server Link";
        privateServerColumn.Width = 155;

        AccountListView.ItemDrag += new ItemDragEventHandler(AccountBox_ItemDrag);
        AccountListView.DragEnter += new DragEventHandler(AccountBox_DragEnter);
        AccountListView.DragOver += new DragEventHandler(AccountBox_DragOver);
        AccountListView.DragDrop += new DragEventHandler(AccountBox_DragDrop);
    }
    private void AddAccount_Click(object sender, EventArgs e)
    {
        using var form = new AddAccount();

        form.StartPosition = FormStartPosition.CenterParent;

        if (form.ShowDialog() == DialogResult.OK)
        {
            var item = new ListViewItem(form.Username);
            item.SubItems.Add(form.ADBPort);
            item.SubItems.Add(form.WebhookLink);
            item.SubItems.Add(form.PrivateServerLink);
            AccountListView.Items.Add(item);
        }

        SettingManager.SaveAccountData(AccountListView);
    }
    private void Remove_Click(object sender, EventArgs e)
    {
        if (AccountListView.SelectedItems.Count > 0)
        {
            foreach (ListViewItem selectedItem in AccountListView.SelectedItems)
            {
                AccountListView.Items.Remove(selectedItem);
            }

            SettingManager.SaveAccountData(AccountListView);
        }
    }
    private void AccountBox_DoubleClick(object? sender, EventArgs e)
    {
        if (AccountListView.SelectedItems.Count == 1)
        {
            var selectedItem = AccountListView.SelectedItems[0];
            var mousePosition = MousePosition;

            int subItemIndex = GetSubItemIndex(mousePosition);

            if (subItemIndex != -1)
            {
                editSubItemIndex = subItemIndex;

                Rectangle subItemBounds = selectedItem.SubItems[subItemIndex].Bounds;

                int columnWidth = AccountListView.Columns[subItemIndex].Width;

                Point textBoxLocation = new(
                    AccountListView.Left + subItemBounds.Left,
                    AccountListView.Top + subItemBounds.Top
                );
                Size textBoxSize = new(columnWidth, subItemBounds.Height);

                editBox = new TextBox
                {
                    Location = textBoxLocation,
                    Size = textBoxSize,
                    Text = selectedItem.SubItems[subItemIndex].Text,
                    TextAlign = HorizontalAlignment.Left
                };

                editBox.LostFocus += EditBox_LostFocus;
                editBox.KeyPress += EditBox_KeyPress;

                Controls.Add(editBox);
                editBox.BringToFront();
                editBox.Focus();
            }
        }
    }
    private int GetSubItemIndex(Point mousePosition)
    {
        Point listViewMousePosition = AccountListView.PointToClient(mousePosition);
        int x = 0;

        for (int i = 0; i < AccountListView.Columns.Count; i++)
        {
            int columnWidth = AccountListView.Columns[i].Width;

            if (listViewMousePosition.X >= x && listViewMousePosition.X < x + columnWidth)
            {
                return i;
            }

            x += columnWidth;
        }

        return -1;
    }
    private void EditBox_LostFocus(object? sender, EventArgs e)
    {
        SaveChanges();
        SettingManager.SaveAccountData(AccountListView);
    }
    private void EditBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SaveChanges();
        }
    }
    private void SaveChanges()
    {
        if (editBox != null)
        {
            var selectedItem = AccountListView.SelectedItems[0];
            if (editSubItemIndex != -1)
            {
                selectedItem.SubItems[editSubItemIndex].Text = editBox.Text;

                SettingManager.SaveAccountData(AccountListView);
            }
            editBox.LostFocus -= EditBox_LostFocus;
            editBox.KeyPress -= EditBox_KeyPress;
            AccountListView.Controls.Remove(editBox);
            editBox.Dispose();
            editBox = null;
        }
    }
    private void RefreshButton_Click(object sender, EventArgs e)
    {
        FormManager.AccountWebhooks.Clear();

        for (int i = 0; i < AccountListView.Items.Count; i++)
        {
            var item = AccountListView.Items[i];

            string webhook = item.SubItems[2].Text;
            string privateServer = item.SubItems[3].Text;
            string accountName = item.SubItems[0].Text;

            if (!string.IsNullOrEmpty(webhook))
            {
                FormManager.AccountWebhooks[i] = webhook;

                int maxLength = 50;
                if (webhook.Length > maxLength)
                {
                    webhook = webhook.Substring(32, maxLength);
                }
                Logger.SendMessage($"Added/Updated webhook for account {i}/{accountName}: {webhook}", ConsoleColor.White);
            }
            else
            {
                Logger.SendMessage($"No webhook found for account {i}/{accountName}", ConsoleColor.Yellow);
            }

            if (!string.IsNullOrEmpty(privateServer))
            {
                FormManager.AccountPrivateServer[i] = privateServer;

                int maxLength = 50;
                if (privateServer.Length > maxLength)
                {
                    privateServer = privateServer.Substring(28, maxLength);
                }
                Logger.SendMessage($"Added/Updated private server link for account {i}/{accountName}: {privateServer}", ConsoleColor.White);
            }
            else
            {
                Logger.SendMessage($"No private server link found for account {i}/{accountName}", ConsoleColor.Yellow);
            }
        }


        if (!BackgroundManager.RefreshWorker.IsBusy)
        {
            BackgroundManager.RefreshWorker.RunWorkerAsync();
        }
        else
        {
            Logger.SendMessage("Wait for the current refresh worker to finish.");
        }
    }

    private void ConnectButton_Click(object sender, EventArgs e)
    {
        if (!BackgroundManager.ConnectWorker.IsBusy)
        {
            BackgroundManager.ConnectWorker.RunWorkerAsync();
        }
        else
        {
            Logger.SendMessage("Wait for the current connect worker to finish.");
        }
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        Stopped = true;
        TimerManager.RefreshTimer.Stop();
        Logger.SendMessage("Stopped the auto restart system.");
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        Stopped = false;
        Logger.SendMessage("Starting the auto restart system");
        StopButton.Enabled = true;

        TimerManager.RefreshTimer.Start();
    }

    private void WebhookBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        Settings.Default.WebhookToggle = checkBox.Checked;
        Settings.Default.Save();
    }

    private void WebhookButton_Click(object sender, EventArgs e)
    {
        using var webhookEditor = new Webhook();
        webhookEditor.StartPosition = FormStartPosition.CenterParent;
        webhookEditor.ShowDialog();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        CheckBox? webhookCheckBox = FormManager.WebhookCheckBox;

        if (webhookCheckBox != null)
        {
            _ = WebhookManager.SendWebhook(0, WebhookManager.GetDisconnectWebhook(), webhookCheckBox);
            _ = WebhookManager.SendWebhook(0, WebhookManager.GetRejoinWebhook(), webhookCheckBox);
            _ = WebhookManager.SendWebhook(0, WebhookManager.GetEmulatorWebhook(), webhookCheckBox);
        }
    }

    private void ImageButton_Click(object sender, EventArgs e)
    {
        ImageForm = new();
        ImageForm.Show();
    }

    private void AccountBox_ItemDrag(object? sender, ItemDragEventArgs e)
    {
        AccountListView.DoDragDrop(e.Item, DragDropEffects.Move);
    }

    private void AccountBox_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(ListViewItem)))
        {
            e.Effect = DragDropEffects.Move;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void AccountBox_DragOver(object? sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(ListViewItem)))
        {
            e.Effect = DragDropEffects.Move;

            Point cp = AccountListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = AccountListView.GetItemAt(cp.X, cp.Y);

            hoverItem?.EnsureVisible();
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void AccountBox_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(ListViewItem)))
        {
            Point cp = AccountListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = AccountListView.GetItemAt(cp.X, cp.Y);

            if (hoverItem != null)
            {
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                int hoverIndex = hoverItem.Index;
                int dragIndex = draggedItem.Index;

                if (dragIndex != hoverIndex)
                {
                    // Remove the item first
                    AccountListView.Items.Remove(draggedItem);

                    // Insert the item at the hovered location
                    AccountListView.Items.Insert(hoverIndex, draggedItem);

                    // Optionally select the moved item
                    draggedItem.Selected = true;

                    SettingManager.SaveAccountData(AccountListView);
                }
            }
            else
            {
                // If the drop location is beyond the last item, append the item at the end
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                AccountListView.Items.Remove(draggedItem);
                AccountListView.Items.Add(draggedItem);
            }
        }
    }

    private void ConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.Checked)
        {
            ConsoleHelper.ShowConsoleWindow();
            Settings.Default.ShowConsole = true;
            Settings.Default.Save();
        }
        else
        {
            ConsoleHelper.HideConsoleWindow();
            Settings.Default.ShowConsole = false;
            Settings.Default.Save();
        }
    }
}
