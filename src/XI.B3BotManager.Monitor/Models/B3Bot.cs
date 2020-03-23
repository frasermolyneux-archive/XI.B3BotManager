using Serilog;

namespace XI.B3BotManager.Monitor.Models
{
    internal class B3Bot
    {
        private readonly ILogger _logger;

        public B3Bot(ILogger logger)
        {
            _logger = logger;
        }

        public string ConfigurationFile { get; private set; }

        public string Name
        {
            get
            {
                var parts = ConfigurationFile.Split("_");
                return $"{parts[0]}{parts[1]}";
            }
        }

        public void Configure(string configurationFile)
        {
            ConfigurationFile = configurationFile;
        }

        public void CheckStatus()
        {
            _logger.Debug($"[{Name}] Checking status of B3Bot");
        }

        public void Kill()
        {
            _logger.Information($"[{Name}] Killing B3Bot");
        }
    }
}