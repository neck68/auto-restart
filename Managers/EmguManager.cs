
using AutoRestarter.Util;
using Emgu.CV.Structure;
using Emgu.CV;
using AutoRestarter.Core.Forms;

namespace AutoRestarter.Managers;

public class EmguManager
{
    private static readonly Dictionary<int, int> screenshotCounters = [];

    public EmguManager()
    {

    }
    

    public static async Task CheckAndKillApp(System.Windows.Forms.Timer timer, string device, string packageName, int accountBoxIndex)
    {
        timer.Stop();

        if (!screenshotCounters.TryGetValue(accountBoxIndex, out int value))
        {
            value = 0;
            screenshotCounters[accountBoxIndex] = value;
        }


        screenshotCounters[accountBoxIndex] = (value % 10) + 1;
        int screenshotCounter = screenshotCounters[accountBoxIndex];

        string screenshotsFolderPath = Path.Combine("referenceData", "screenshots", $"account_{accountBoxIndex}");
        Directory.CreateDirectory(screenshotsFolderPath);
        string capturedImagePath = Path.Combine(screenshotsFolderPath, $"screenshot_{screenshotCounter}.png");


        var referenceImages = new Dictionary<string, (double threshold, Rectangle roi)>
            {
                { "loadingScreen.png", (0.125, new Rectangle(0, 0, 380, 380)) },
                { "mainMenu.png", (0.35, new Rectangle(0, 0, 380, 380)) },
                { "mainMenu_2.png", (0.35, new Rectangle(0, 0, 380, 380)) },
                { "mainMenu_3.png", (0.35, new Rectangle(0, 0, 380, 380)) },
                { "errorCode273.png", (0.2, new Rectangle(71, 116, 238, 148)) },
                { "errorCode273_2.png", (0.2, new Rectangle(71, 116, 238, 148)) },
                { "errorCode769.png", (0.2, new Rectangle(71, 116, 238, 148)) },
                { "whiteScreen.png", (0.2, new Rectangle(0, 0, 380, 380)) },
                { "blackScreen.png", (0.2, new Rectangle(0, 0, 380, 380)) },
                { "windmillDarkFreeze.png", (0.3, new Rectangle(0, 0, 380, 380)) },
            };

        try
        {
            Logger.SendMessage($"Starting check for device {device}, account {accountBoxIndex}.", ConsoleColor.Cyan);

            await CaptureScreenshot(device, capturedImagePath);
            using var capturedImage = new Image<Bgr, byte>(capturedImagePath);

            foreach (var entry in referenceImages)
            {
                if (Main.Stopped)
                {
                    return;
                }

                string referenceImageName = entry.Key;
                string referenceImagePath = Path.Combine("referenceData", "references", referenceImageName);
                var (threshold, roi) = entry.Value;

                Logger.SendMessage($"Checking {referenceImageName} on {device} for similarities.", ConsoleColor.Yellow);

                int delay = GetDelayForImage(referenceImageName);

                await Task.Delay(TimeSpan.FromSeconds(delay));

                bool isAppRunning = await OldADBManager.IsAppRunning(device, packageName);
                if (!isAppRunning)
                {
                    Logger.SendMessage("App is not running. Skipping image scanning.", ConsoleColor.Yellow);
                    return;
                }

                if (CheckImageSimilarity(capturedImage, referenceImagePath, threshold, roi))
                {
                    Logger.SendMessage($"Images matched on device {device}, account {accountBoxIndex} for {referenceImageName}.", ConsoleColor.Red);

                    if (FormManager.WebhookCheckBox == null)
                    {
                        return;
                    }

                    await WebhookManager.SendWebhook(accountBoxIndex, WebhookManager.GetDisconnectWebhook(), FormManager.WebhookCheckBox);
                    await OldADBManager.SendADB($"-s {device} shell am force-stop {packageName}");

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error checking and killing app: {ex.Message}", ConsoleColor.Red);
        }
        finally
        {
            if (!timer.Enabled)
            {
                if (!Main.Stopped)
                {
                    timer.Start();
                }
            }
        }
    }
    private static bool CheckImageSimilarity(Image<Bgr, byte> capturedImage, string referenceImagePath, double threshold, Rectangle roi)
    {
        using var referenceImage = new Image<Bgr, byte>(referenceImagePath);

        var capturedRoi = capturedImage.GetSubRect(roi);
        var result = capturedRoi.AbsDiff(referenceImage);

        double nonZeroCount = result.CountNonzero()[0];
        double totalPixels = capturedRoi.Width * capturedRoi.Height;
        double similarityPercentage = (1 - (nonZeroCount / totalPixels)) * 100;

        Logger.SendMessage($"Similarity Percentage: {similarityPercentage:F2}%", ConsoleColor.Yellow);

        return nonZeroCount <= threshold * totalPixels;
    }
    private static int GetDelayForImage(string imageName)
    {
        Dictionary<string, int> delayDictionary = new()
            {
                { "loadingScreen.png", 2 },
                { "mainMenu.png", 2 },
                { "mainMenu_2.png", 2 },
                { "mainMenu_3.png", 2 },
                { "errorCode273.png", 2},
                { "errorCode273_2.png", 2},
                { "errorCode769.png", 2},
                { "whiteScreen.png", 2},
                { "blackScreen.png", 2},
                { "windmillDarkFreeze.png", 2},
                // Add more entries for other images as needed
            };


        if (delayDictionary.TryGetValue(imageName, out int delay))
        {
            return delay;
        }
        else
        {
            return 1;
        }
    }
    private static async Task CaptureScreenshot(string device, string capturedImagePath)
    {
        Logger.SendMessage("Attempting to capture a screenshot.");

        try
        {
            await OldADBManager.SendADB($"-s {device} shell screencap -p /sdcard/screenshot.png");
            await OldADBManager.SendADB($"-s {device} pull /sdcard/screenshot.png \"{capturedImagePath}\"");
            Logger.SendMessage($"Screenshot captured and saved to {capturedImagePath}", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"Error capturing and saving screenshot: {ex.Message}", ConsoleColor.Red);
        }
    }

}
