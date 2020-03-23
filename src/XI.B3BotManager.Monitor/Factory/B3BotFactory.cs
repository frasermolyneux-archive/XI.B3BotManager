using Serilog;
using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor.Factory
{
    internal interface IB3BotFactory
    {
        B3Bot CreateInstance(string configurationFile);
    }

    internal class B3BotFactory : IB3BotFactory
    {
        private readonly ILogger _logger;

        public B3BotFactory(ILogger logger)
        {
            _logger = logger;
        }

        public B3Bot CreateInstance(string configurationFile)
        {
            var bot = new B3Bot(_logger);
            bot.Configure(configurationFile);

            return bot;
        }
    }
}