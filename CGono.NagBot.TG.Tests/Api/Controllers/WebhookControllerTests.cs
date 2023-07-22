using CGono.NagBot.TG.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

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
            Assert.IsType<OkResult>(result);
        }
    }
}