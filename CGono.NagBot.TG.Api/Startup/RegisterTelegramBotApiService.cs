using CGono.NagBot.TG.Services;

namespace CGono.NagBot.TG.Api.Startup
{
    /// <summary>
    /// Registers the webhook with Telegram.
    /// </summary>
    public class RegisterTelegramBotApiService : BackgroundService
    {
        private readonly IBotApiService _botApiService;

        public RegisterTelegramBotApiService(IBotApiService botApiService)
        {
            _botApiService = botApiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._botApiService.RegisterWebhookAsync(stoppingToken);
        }
    }
}
