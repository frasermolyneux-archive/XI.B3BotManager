using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor.Factory
{
    internal interface IB3BotFactory
    {
        B3Bot CreateInstance(B3BotConfig config);
    }
}