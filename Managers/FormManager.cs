
namespace AutoRestarter.Managers;

public class FormManager()
{
    private static List<int> accountNumbers = [];
    private static List<bool> activeAccounts = [];

    private static CheckBox? webhookCheckBox;
    public static Dictionary<int, string> AccountWebhooks { get; set; } = [];
    public static Dictionary<int, string> AccountPrivateServer {  get; set; } = [];

    public static List<int> AccountNumbers
    {
        get { return accountNumbers; }
        set { accountNumbers = value; }
    }

    public static List<bool> ActiveAccounts
    {
        get { return activeAccounts; }
        set { activeAccounts = value; }
    }

    public static CheckBox? WebhookCheckBox
    {
        get { return webhookCheckBox; }
        set { webhookCheckBox = value; }
    }

    public ListView? AccountBox;
    public Button? StartButton;
    public Button? ConnectButton;

    public FormManager(Button startButton, ListView accountBox, Button connectButton) : this()
    {
        AccountBox = accountBox;
        StartButton = startButton;
        ConnectButton = connectButton;
    }
}
