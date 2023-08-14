namespace CGono.NagBot.TG.Services.Settings
{
    public class BotApiSettings
    {
        public const string BotApi = "BotApi";

        /// <summary>
        /// Token used to authenticate to Telegram
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Webhook URL to register with Telegram
        /// </summary>
        public string WebhookUrl { get; set; } = string.Empty;
    }
}
