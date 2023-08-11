using Telegram.Bot;

namespace CGono.NagBot.TG.Services
{
    public class NagBot : TelegramBotClient
    {
        public NagBot(TelegramBotClientOptions options, HttpClient? httpClient = null) : base(options, httpClient)
        {
        }

        public NagBot(string token, HttpClient? httpClient = null) : base(token, httpClient)
        {
        }
    }
}