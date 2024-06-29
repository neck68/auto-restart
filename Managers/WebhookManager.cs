using AutoRestarter.Util;
using System.Text;

namespace AutoRestarter.Managers;

public class WebhookManager 
{
    private static string? _disconnectWebhook;
    private static string? _rejoinWebhook;
    private static string? _emulatorWebhook;

    private static readonly string settingsFolderPath = "settings";
    private static readonly string disconnectWebhookDataFilePath = Path.Combine(settingsFolderPath, "disconnectWebhook.json");
    private static readonly string rejoinWebhookDataFilePath = Path.Combine(settingsFolderPath, "rejoinWebhook.json");
    private static readonly string emulatorWebhookDataFilePath = Path.Combine(settingsFolderPath, "emulatorWebhook.json");

    public WebhookManager() 
    {
        InitializeWebhookData();
    }

    private static void InitializeWebhookData()
    {
        _disconnectWebhook = LoadJsonPayload(disconnectWebhookDataFilePath);
        _rejoinWebhook = LoadJsonPayload(rejoinWebhookDataFilePath);
        _emulatorWebhook = LoadJsonPayload(emulatorWebhookDataFilePath);

        if (string.IsNullOrEmpty(_disconnectWebhook))
        {
            _disconnectWebhook = DefaultDisconnectWebhook();
            SaveJsonPayload(disconnectWebhookDataFilePath, _disconnectWebhook);
        }

        if (string.IsNullOrEmpty(_rejoinWebhook))
        {
            _rejoinWebhook = DefaultRejoinWebhook();
            SaveJsonPayload(rejoinWebhookDataFilePath, _rejoinWebhook);
        }

        if (string.IsNullOrEmpty(_emulatorWebhook))
        {
            _emulatorWebhook = DefaultEmulatorWebhook();
            SaveJsonPayload(emulatorWebhookDataFilePath, _emulatorWebhook);
        }
    }

    private static string? LoadJsonPayload(string filePath)
    {
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            return !string.IsNullOrWhiteSpace(content) ? content : null;
        }
        return null;
    }

    private static string DefaultDisconnectWebhook()
    {
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string jsonTemplate = @"{
    ""username"": ""Roblox Auto-Restarter"",
    ""avatar_url"": ""https://i.imgur.com/cl7eVJX.png"",
    ""content"": ""<@userId>"",
    ""embeds"": [
        {
            ""title"": ""Disconnected!"",
            ""description"": ""Roblox has been forcibly closed."",
            ""url"": ""https://example.com"",
            ""color"": 0,
            ""fields"": [
                {
                    ""name"": ""Field 1"",
                    ""value"": ""Value 1"",
                    ""inline"": true
                },
                {
                    ""name"": ""Field 2"",
                    ""value"": ""Value 2"",
                    ""inline"": true
                }
            ],
            ""footer"": {
                ""text"": ""{time}"",
                ""icon_url"": ""https://i.imgur.com/cl7eVJX.png""
            }
        }
    ]
}";

        string json = jsonTemplate.Replace("{time}", currentTime);

        return json;
    }
    private static string DefaultRejoinWebhook()
    {
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string jsonTemplate = @"{
    ""username"": ""Roblox Auto-Restarter"",
    ""avatar_url"": ""https://i.imgur.com/cl7eVJX.png"",
    ""content"": ""<@userId>"",
    ""embeds"": [
        {
            ""title"": ""Reconnecting!"",
            ""description"": ""Emulator attempted to open roblox."",
            ""url"": ""https://example.com"",
            ""color"": 0,
            ""fields"": [
                {
                    ""name"": ""Field 1"",
                    ""value"": ""Value 1"",
                    ""inline"": true
                },
                {
                    ""name"": ""Field 2"",
                    ""value"": ""Value 2"",
                    ""inline"": true
                }
            ],
            ""footer"": {
                ""text"": ""{time}"",
                ""icon_url"": ""https://i.imgur.com/cl7eVJX.png""
            }
        }
    ]
}";

        string jsonWithTime = jsonTemplate.Replace("{time}", currentTime);

        return jsonWithTime;
    }
    private static string DefaultEmulatorWebhook()
    {
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string jsonTemplate = @"{
    ""username"": ""Roblox Auto-Restarter"",
    ""avatar_url"": ""https://i.imgur.com/cl7eVJX.png"",
    ""content"": ""<@userId>"",
    ""embeds"": [
        {
            ""title"": ""Emulator Crashed!"",
            ""description"": ""Attempted to reopen the emulator."",
            ""url"": ""https://example.com"",
            ""color"": 0,
            ""fields"": [
                {
                    ""name"": ""Field 1"",
                    ""value"": ""Value 1"",
                    ""inline"": true
                },
                {
                    ""name"": ""Field 2"",
                    ""value"": ""Value 2"",
                    ""inline"": true
                }
            ],
            ""footer"": {
                ""text"": ""{time}"",
                ""icon_url"": ""https://i.imgur.com/cl7eVJX.png""
            }
        }
    ]
}";

        string jsonWithTime = jsonTemplate.Replace("{time}", currentTime);

        return jsonWithTime;
    }
    public static string? GetDisconnectWebhook() => _disconnectWebhook;
    public static string? GetRejoinWebhook() => _rejoinWebhook;
    public static string? GetEmulatorWebhook() => _emulatorWebhook;

    public static void SetDisconnectWebhook(string jsonPayload)
    {
        _disconnectWebhook = jsonPayload;
        SaveJsonPayload(disconnectWebhookDataFilePath, jsonPayload);
    }

    public static void SetRejoinWebhook(string jsonPayload)
    {
        _rejoinWebhook = jsonPayload;
        SaveJsonPayload(rejoinWebhookDataFilePath, jsonPayload);
    }

    public static void SetEmulatorWebhook(string jsonPayload)
    {
        _emulatorWebhook = jsonPayload;
        SaveJsonPayload(emulatorWebhookDataFilePath, jsonPayload);
    }

    private static void SaveJsonPayload(string filePath, string jsonPayload)
    {
        if (!Directory.Exists(settingsFolderPath))
        {
            Directory.CreateDirectory(settingsFolderPath);
        }
        File.WriteAllText(filePath, jsonPayload);
    }

    public static async Task SendWebhook(int accountNumber, string? jsonPayload, CheckBox discordCheckBox)
    {
        if (string.IsNullOrEmpty(jsonPayload))
        {
            Logger.SendMessage("Invalid parameters. Webhook not sent.", ConsoleColor.Red);
            return;
        }

        if (!discordCheckBox.Checked)
        {
            return;
        }

        if (!FormManager.AccountWebhooks.TryGetValue(accountNumber, out string? webhookLink) || string.IsNullOrEmpty(webhookLink))
        {
            Logger.SendMessage($"Webhook URL for account {accountNumber} not found or invalid.", ConsoleColor.Red);
            return;
        }

        try
        {
            using var client = new HttpClient();
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(webhookLink, content);

            if (response.IsSuccessStatusCode)
            {
                Logger.SendMessage("Webhook sent successfully!");
            }
            else
            {
                Logger.SendMessage($"Failed to send webhook. Status code: {response.StatusCode}", ConsoleColor.Red);
            }
        }
        catch (Exception ex)
        {
            Logger.SendMessage($"An error occurred while sending the webhook: {ex.Message}", ConsoleColor.Red);
        }
    }
}
