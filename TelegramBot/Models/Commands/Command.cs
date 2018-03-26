using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string name { get; }

        public abstract void Execute(Message message,TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(this.name) && command.Contains(AppSettings.name);
        }
    }
}