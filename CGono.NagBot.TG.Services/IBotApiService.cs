namespace CGono.NagBot.TG.Services
{
    /// <summary>
    /// Represents a service that manages connections to the Telegram Bot API.
    /// </summary>
    public interface IBotApiService
    {
        /// <summary>
        /// Registers this application with Telegram.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RegisterWebhookAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="chatId">The chat ID to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken);
    }
}
