using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBot.AiChat
{
    public class GoogleAiResponseReader : IAiResponseReader
    {
        public string ExtractAIResponse(string jsonResponse)
        {
            var obj = JsonDocument.Parse(jsonResponse);
            string text = obj.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();
            return text;
        }
    }
}
