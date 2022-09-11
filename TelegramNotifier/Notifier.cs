using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace TelegramNotifier
{
    public class Notifier
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static TelegramBotClient BotClient = new("256930387:AAEopn2bJqrmLL9EWHxn8Jtuiw0vvIvy1Cg"); // nanobyte_bot
        private static long ChatId = -1001452888696; // Apartments Crawler

        private static int UniqueId = 0;

        private static Notifier telegramNotifier;
        public static Notifier Instance => telegramNotifier ??= new Notifier();

        static Notifier()
        {
            Random random = new();
            UniqueId = random.Next(0, 100000);
        }

        /// <summary>
        /// Send Message to Telegram Chat
        /// </summary>
        /// <param name="messageBody">Message body</param>
        /// <param name="disableNotification"></param>
        /// <returns>Message id</returns>
        public int Notify(string messageBody, bool disableNotification = true)
        {
            try
            {
                var message = BotClient.SendTextMessageAsync(ChatId, $"{messageBody}\n________________" +
                    $"\nInstance #{UniqueId}", parseMode: ParseMode.Html, disableNotification: disableNotification).Result;

                return message.MessageId;
            }
            catch (Exception e)
            {
                Logger.Error($"{e.Data}\n{e.Message}\n{e.StackTrace}");
                return -1;
            }
        }

        public void EditMessage(long chatId, int messageId, string message)
        {
            _ = BotClient.EditMessageTextAsync(chatId, messageId, text: message, parseMode: ParseMode.Html).Result;
        }

        public void RemoveMessage(long chatId, int messageId)
        {
            _ = BotClient.DeleteMessageAsync(chatId, messageId);
        }
        public void RemoveMessage(int messageId)
        {
            _ = BotClient.DeleteMessageAsync(ChatId, messageId);
        }

        public void WaitForCommand()
        {
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            BotClient.StartReceiving(
                Handlers.HandleUpdateAsync,
                Handlers.HandleErrorAsync,
                receiverOptions,
                cts.Token);
        }
    }
}