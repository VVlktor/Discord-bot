using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.AiChat
{
    public class UserConversation
    {
        public string Username { get; set; }
        public List<(string, string)> Conversation { get; set; }

        public UserConversation(string username)
        {
            Conversation = new List<(string, string)>();
            Username = username;
        }
    }
}
