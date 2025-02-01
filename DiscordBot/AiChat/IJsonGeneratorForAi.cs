using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.AiChat
{
    public interface IJsonGeneratorForAi
    {
        string GenerateJson(List<(string, string)> conv);
    }
}
