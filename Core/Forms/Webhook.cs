
using AutoRestarter.Managers;
using AutoRestarter.Util;

namespace AutoRestarter.Core.Forms;

public partial class Webhook : Form
{
    public Webhook()
    {
        InitializeComponent();
    }

    private void Webhook_Load(object sender, EventArgs e)
    {
        webhookBox.SelectedItem = "Disconnect Webhook";
        webhookBox.KeyPress += ComboBox_KeyPress;

        LoadJsonPayload();
    }

    private void ComboBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        e.Handled = true;
    }

    private void LoadJsonPayload()
    {
        if (webhookBox.SelectedItem == null)
        {
            return;
        }

        string? selectedWebhookType = webhookBox.SelectedItem.ToString();

        switch (selectedWebhookType)
        {
            case "Disconnect Webhook":
                webhookJsonBox.Text = WebhookManager.GetDisconnectWebhook();
                break;
            case "Rejoin Webhook":
                webhookJsonBox.Text = WebhookManager.GetRejoinWebhook();
                break;
            case "Emulator Webhook":
                webhookJsonBox.Text = WebhookManager.GetEmulatorWebhook();
                break;
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (webhookBox.SelectedItem != null && webhookJsonBox.Text != null)
        {
            // Get the selected webhook type from the ComboBox
            string? selectedWebhookType = webhookBox.SelectedItem.ToString();

            // Save the JSON payload based on the selected webhook type
            switch (selectedWebhookType)
            {
                case "Disconnect Webhook":
                    WebhookManager.SetDisconnectWebhook(webhookJsonBox.Text);
                    break;
                case "Rejoin Webhook":
                    WebhookManager.SetRejoinWebhook(webhookJsonBox.Text);
                    break;
                case "Emulator Webhook":
                    WebhookManager.SetEmulatorWebhook(webhookJsonBox.Text);
                    break;
            }

            Logger.SendMessage("Webhook saved successfully!");
            Close();
        }
        else
        {
            Logger.SendMessage("Error: Selected webhook type or JSON payload is null.", ConsoleColor.Red);
        }
    }

    private void WebhookBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadJsonPayload();
    }
}
