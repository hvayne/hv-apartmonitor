using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace TelegramNotifier
{
    public class Handlers
    {
        public static DateTime date = DateTime.UtcNow;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Dictionary<string, Func<string[], ICommand>> Commands = new();

        public static void RegisterCommand(string name, Func<string[], ICommand> command)
        {
            if (Commands.ContainsKey(name))
            {
                return;
            }

            Commands.TryAdd(name, command);
        }


        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Logger.Error(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {               
                UpdateType.ChannelPost => BotOnMessageReceived(botClient, update.ChannelPost),
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage),
               _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {
            Logger.Info($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text && message.Type != MessageType.Document || date > message.Date)
                return;

            var args = new List<string>(message?.Text?.Split(' ') ?? message.Caption.Split(' '));

            Commands.TryGetValue(args[0], out var action);

            if (action == null)
            {
                Logger.Warn($"Command '{args[0]}' is not recognized");
                return;
            }

            try
            {
                var edited = (message.Text ?? message.Caption) + " - 🆗";

                Notifier.Instance.EditMessage(message.Chat.Id, message.MessageId, edited);
            }
            catch (Exception)
            {
            }

            args.Remove(args[0]);
            await action?.Invoke(args.ToArray()).Execute();
        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Logger.Error($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }
    }
}
