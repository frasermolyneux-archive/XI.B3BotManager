using System.Collections.Generic;
using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor.Configuration
{
    internal interface IB3BotConfiguration
    {
        IEnumerable<B3BotConfig> GetConfigurations();
    }
}