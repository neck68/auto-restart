namespace AutoRestarter.Util;

public static class Folder
{
    public static void CreateReferenceFolders()
    {
        string referenceDataFolderPath = "referenceData";

        try
        {
            if (!Directory.Exists(referenceDataFolderPath))
            {
                Logger.SendMessage("Reference folder not found.", ConsoleColor.Yellow);
                Directory.CreateDirectory(referenceDataFolderPath);
                Logger.SendMessage("Created directory.");

            }
            else
            {

            }

            string screenshotsFolderPath = Path.Combine(referenceDataFolderPath, "screenshots");
            if (!Directory.Exists(screenshotsFolderPath))
            {
                Logger.SendMessage("Screenshot directory not found.", ConsoleColor.Yellow);
                Directory.CreateDirectory(screenshotsFolderPath);
                Logger.SendMessage("Created directory.");
            }
            else
            {

            }
            // Create references folder
            string referencesFolderPath = Path.Combine(referenceDataFolderPath, "references");
            if (!Directory.Exists(referencesFolderPath))
            {
                Logger.SendMessage("References directory not found.", ConsoleColor.Yellow);
                Directory.CreateDirectory(referencesFolderPath);
                Logger.SendMessage("Created directory.");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error creating reference data folders: {ex.Message}", ConsoleColor.Red);
        }
    }

    public static void InitializeFolders()
    {
        string settingsFolderPath = "settings";
        string disconnectWebhookFilePath = Path.Combine(settingsFolderPath, "disconnectWebhook.json");
        string rejoinWebhookFilePath = Path.Combine(settingsFolderPath, "rejoinWebhook.json");
        string emulatorWebhookFilePath = Path.Combine(settingsFolderPath, "emulatorWebhook.json");
        string accountDataFilePath = Path.Combine(settingsFolderPath, "accountData.txt");
        string imageDataFilePath = Path.Combine(settingsFolderPath, "imageData.txt"); // New file path

        try
        {
            CreateReferenceFolders();

            if (!Directory.Exists(settingsFolderPath))
            {
                Logger.SendMessage("Settings folder not found.", ConsoleColor.Yellow);
                Directory.CreateDirectory(settingsFolderPath);
                Logger.SendMessage("Created settings directory.");
            }

            // Create disconnect webhook file if it doesn't exist
            if (!File.Exists(disconnectWebhookFilePath))
            {
                Logger.SendMessage("Disconnect webhook file not found.", ConsoleColor.Yellow);
                File.Create(disconnectWebhookFilePath).Close(); // Create the file if it doesn't exist
                Logger.SendMessage("Created disconnect webhook file.");
            }

            // Create rejoin webhook file if it doesn't exist
            if (!File.Exists(rejoinWebhookFilePath))
            {
                Logger.SendMessage("Rejoin webhook file not found.", ConsoleColor.Yellow);
                File.Create(rejoinWebhookFilePath).Close(); // Create the file if it doesn't exist
                Logger.SendMessage("Created rejoin webhook file.");
            }

            // Create emulator webhook file if it doesn't exist
            if (!File.Exists(emulatorWebhookFilePath))
            {
                Logger.SendMessage("Emulator webhook file not found.", ConsoleColor.Yellow);
                File.Create(emulatorWebhookFilePath).Close(); // Create the file if it doesn't exist
                Logger.SendMessage("Created emulator webhook file.");
            }

            // Create account data file if it doesn't exist
            if (!File.Exists(accountDataFilePath))
            {
                Logger.SendMessage("Account data file not found.", ConsoleColor.Yellow);
                File.Create(accountDataFilePath).Close(); // Create the file if it doesn't exist
                Logger.SendMessage("Created account data file.");
            }

            // Create image data file if it doesn't exist
            if (!File.Exists(imageDataFilePath))
            {
                Logger.SendMessage("Image data file not found.", ConsoleColor.Yellow);
                File.Create(imageDataFilePath).Close(); // Create the file if it doesn't exist
                Logger.SendMessage("Created image data file.");
            }
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error initializing folders and files: {ex.Message}", ConsoleColor.Red);
        }
    }

}

