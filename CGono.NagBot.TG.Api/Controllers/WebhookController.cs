using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

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
        return Ok(new { message = testMessage });
    }

    [Route("update"), HttpPost]
    public async Task<IActionResult> ReceiveUpdate([FromBody] Update update)
    {
        if (update?.Message == null)
        {
            return BadRequest();
        }

        // Process your update
        return Ok(new { message = update.Message.Text });
    }
}
