using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.AiChat
{
    public class JsonGeneratorForAi : IJsonGeneratorForAi
    {
        public string GenerateJson(List<(string, string)> conv)
        {
            var contents = new StringBuilder("{ \"contents\": [");
            foreach (var message in conv)
            {
                contents.Append($"{{\"role\":\"{message.Item1}\",\"parts\":[{{\"text\":\"{message.Item2}\"}}]}},");
            }
            contents.Append("]}");
            return contents.ToString();
        }
    }
}
