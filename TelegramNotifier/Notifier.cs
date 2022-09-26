using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramNotifier
{
    public class Notifier
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static TelegramBotClient BotClient = new("256930387:AAEopn2bJqrmLL9EWHxn8Jtuiw0vvIvy1Cg"); // nanobyte_bot
        private static long RentChatId = -1001452888696; // Apartments Crawler [RENT]
        private static long SaleChatId = -1001641272354; // Apartments Crawler [SALE]

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
        public int Notify(string messageBody, bool disableNotification = true, bool isRent = true)
        {
            try
            {
                long chatId = RentChatId;
                if(!isRent)
                {
                    chatId = SaleChatId;
                }
                var message = BotClient.SendTextMessageAsync(chatId, $"{messageBody}\n________________" +
                    $"\nInstance #{UniqueId}", parseMode: ParseMode.Html, disableNotification: disableNotification).Result;

                return message.MessageId;
            }
            catch (Exception e)
            {
                Logger.Error($"{e.Data}\n{e.Message}\n{e.StackTrace}");
                return -1;
            }
        }
        public int NotifyWithImages(List<string> urls, string messageBody, bool isRent)
        {
            if (urls == null || urls.Count == 0)
            {
                Logger.Error("url list was null or empty");
                return -1;
            }
            try
            {
                List<InputMediaPhoto> album = new();
                foreach (string url in urls)
                {
                    InputMediaPhoto photo = new(new InputMedia(url));
                    album.Add(photo);
                }
                if (messageBody.Length > 1023)
                {
                    messageBody = messageBody[..1020];
                    messageBody += "<<<";
                    Logger.Info("message body was trimmed!");
                }
                album.First().Caption = messageBody;
                album.First().ParseMode = ParseMode.Html;
                long chatId = RentChatId;
                if (!isRent)
                {
                    chatId = SaleChatId;
                }
                Message[]? msg = BotClient.SendMediaGroupAsync(chatId, album).Result;
                if (msg == null)
                {
                    Logger.Error("Telegram responded with 0 messages");
                }
                else
                {
                    string msgs = string.Empty;
                    foreach (var message in msg)
                    {
                        msgs += message.MessageId + ",";
                    }
                    Logger.Info("messages " + msgs + " were sent to chat");
                }
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
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