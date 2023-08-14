using CGono.NagBot.TG.Services.Impl;
using CGono.NagBot.TG.Services.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace CGono.NagBot.TG.Tests.Services
{
    public class TelegramBotApiServiceTests
    {
        private readonly TelegramBotApiService _telegramBotApiService;
        private readonly Mock<IOptions<BotApiSettings>> _optionsMock;
        private readonly Mock<ITelegramBotClient> _botClientMock;
        private readonly Mock<ILogger<TelegramBotApiService>> _loggerMock;

        public TelegramBotApiServiceTests()
        {
            _optionsMock = new Mock<IOptions<BotApiSettings>>();
            _loggerMock = new Mock<ILogger<TelegramBotApiService>>();
            _optionsMock.SetupGet(x => x.Value).Returns(new BotApiSettings
            {
                Token = "validToken",
                WebhookUrl = "http://localhost:5000"
            });

            _botClientMock = new Mock<ITelegramBotClient>();
            _telegramBotApiService = new TelegramBotApiService(_botClientMock.Object, _optionsMock.Object, _loggerMock.Object);

            _botClientMock
                .Setup(x => x.MakeRequestAsync(new SetWebhookRequest(It.IsAny<string>())
                {
                    // Certificate = It.IsAny<InputFileStream>(),
                    // MaxConnections = It.IsAny<int>(),
                    // AllowedUpdates = It.IsAny<IEnumerable<UpdateType>>(),
                    // IpAddress = It.IsAny<string>(),
                    // DropPendingUpdates = It.IsAny<bool>(),
                    // SecretToken = It.IsAny<string>(),
                },
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            _botClientMock
                .Setup(x => x.MakeRequestAsync(new DeleteWebhookRequest()
                {
                    DropPendingUpdates = It.IsAny<bool>()
                },
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            _botClientMock
                .Setup(x => x.MakeRequestAsync(new SendMessageRequest(It.IsAny<long>(), It.IsAny<string>()),
                It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Message()));
        }

        [Fact]
        public async Task Test_RegisterWebhookAsync()
        {
            CancellationToken cancellationToken = new();
            await _telegramBotApiService.RegisterWebhookAsync(cancellationToken);

            _botClientMock
                .Verify(x => x.MakeRequestAsync(It.IsAny<SetWebhookRequest>(),
                It.IsAny<CancellationToken>()));

            _botClientMock
                .Verify(x => x.MakeRequestAsync(It.IsAny<DeleteWebhookRequest>(),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Test_SendMessageAsync()
        {
            CancellationToken cancellationToken = new();
            const long chatId = 123456789;
            const string message = "testMessage";

            await _telegramBotApiService.SendMessageAsync(chatId, message, cancellationToken);

            _botClientMock
                .Verify(x => x.MakeRequestAsync(It.IsAny<SendMessageRequest>(),
                It.IsAny<CancellationToken>()));
        }
    }
}
