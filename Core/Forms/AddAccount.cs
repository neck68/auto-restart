
using AutoRestarter.Managers;

namespace AutoRestarter.Core.Forms;

public partial class AddAccount : Form
{
    private readonly TextBoxManager textBoxManager;
    public string? Username { get; private set; }
    public string? ADBPort { get; private set; }
    public string? WebhookLink { get; private set; }
    public string? PrivateServerLink { get; private set; }

    public List<TextBox> TextBoxes { get; private set; }


    public AddAccount()
    {
        InitializeComponent();

        textBoxManager = new();
        TextBoxes = [AdbTextBox];

        textBoxManager.SubscribeEvents(TextBoxes);
    }

    private void AddAccount_Load(object sender, EventArgs e)
    {

    }

    private void Submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(UsernameTextBox.Text)) 
        {
            UsernameTextBox.Text = "nil";
        }

        if (string.IsNullOrEmpty(AdbTextBox.Text))
        {
            AdbTextBox.Text = "nil";
        }

        if (string.IsNullOrEmpty(WebhookTextBox.Text))
        {
            WebhookTextBox.Text = "nil";
        }

        if (string.IsNullOrEmpty(PrivateServerTextBox.Text))
        {
            PrivateServerTextBox.Text = "nil";
        }

        Username = UsernameTextBox.Text;
        ADBPort = AdbTextBox.Text;
        WebhookLink = WebhookTextBox.Text;
        PrivateServerLink = PrivateServerTextBox.Text;

        DialogResult = DialogResult.OK;
        Close();
    }
}
