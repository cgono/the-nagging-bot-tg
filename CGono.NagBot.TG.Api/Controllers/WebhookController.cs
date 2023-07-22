using Microsoft.AspNetCore.Mvc;

namespace CGono.NagBot.TG.Api.Controllers;

/// <summary>
/// Controller for handling webhook requests from Telegram.
/// </summary>
[ApiController]
[Route("hook")]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(ILogger<WebhookController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Test endpoint for webhook.
    /// </summary>
    /// <param name="testMessage"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetWebhook")]
    public IActionResult TestReply(string testMessage)
    {
        this._logger.LogInformation("Received test message: {testMessage}", testMessage);
        return Ok();
    }
}
