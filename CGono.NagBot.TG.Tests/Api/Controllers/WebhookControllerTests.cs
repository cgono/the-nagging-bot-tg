using CGono.NagBot.TG.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Telegram.Bot.Types;

namespace CGono.NagBot.TG.Tests.Api.Controllers
{
    public class WebhookControllerTests
    {
        private readonly WebhookController _controller;
        private readonly Mock<ILogger<WebhookController>> _loggerMock;

        public WebhookControllerTests()
        {
            _loggerMock = new Mock<ILogger<WebhookController>>();
            _controller = new WebhookController(_loggerMock.Object);
        }

        [Fact]
        public void TestReply_ReturnsOk()
        {
            // Arrange
            var testMessage = "test message";

            // Act
            var result = _controller.TestReply(testMessage);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Contains(testMessage, ((OkObjectResult)result).Value.ToString());
        }

        [Fact]
        public async Task ReceiveUpdate_ReturnsBadRequest()
        {
            // Arrange -- empty message
            var update = new Update();

            // Act
            var result = await _controller.ReceiveUpdate(update);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ReceiveUpdate_ReturnsOk()
        {
            // Arrange
            var update = new Update
            {
                Message = new Message
                {
                    Text = "test message"
                }
            };

            // Act
            var result = await _controller.ReceiveUpdate(update);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Contains(update.Message.Text, ((OkObjectResult)result).Value.ToString());
        }
    }
}