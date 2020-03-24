using Serilog;
using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor.Factory
{
    internal class B3BotFactory : IB3BotFactory
    {
        private readonly ILogger _logger;

        public B3BotFactory(ILogger logger)
        {
            _logger = logger;
        }

        public B3Bot CreateInstance(B3BotConfig config)
        {
            var bot = new B3Bot(_logger);
            bot.Configure(config);

            return bot;
        }
    }
}