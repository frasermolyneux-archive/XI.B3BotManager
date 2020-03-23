using System.Collections.Generic;

namespace XI.B3BotManager.Monitor.Configuration
{
    internal interface IB3BotConfiguration
    {
        IEnumerable<string> GetConfigurations();
    }
}