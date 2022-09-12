using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramNotifier;

namespace ApartmentCrawler.Commands
{
    public class BanCommand : ICommand
    {
        string idToBan = string.Empty;
        public BanCommand(string[] args)
        {
            idToBan = args[0];
        }
        public Task Execute()
        {
            Notifier notifyer = new();
            notifyer.Notify("ban command received for userid = " + idToBan);
            return Task.CompletedTask;
        }
    }
}
