using CGono.NagBot.TG.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace CGono.NagBot.TG.Services.Impl
{
    public class TelegramBotApiService : IBotApiService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly BotApiSettings _botApiSettings;
        private readonly ILogger<TelegramBotApiService> _logger;

        public TelegramBotApiService(ITelegramBotClient botClient, IOptions<BotApiSettings> options, ILogger<TelegramBotApiService> logger)
        {
            _botClient = botClient;
            _botApiSettings = options.Value;
            _logger = logger;
        }

        public async Task RegisterWebhookAsync(CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Registering webhook URL: {webhookUrl}", _botApiSettings.WebhookUrl);
            await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
            await _botClient.SetWebhookAsync(_botApiSettings.WebhookUrl, cancellationToken: cancellationToken);
        }

        public async Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken)
        {
            await _botClient.SendTextMessageAsync(chatId, message, cancellationToken: cancellationToken);
        }
    }
}