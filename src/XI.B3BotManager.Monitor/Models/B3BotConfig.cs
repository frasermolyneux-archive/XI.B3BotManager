namespace XI.B3BotManager.Monitor.Models
{
    internal class B3BotConfig
    {
        public B3BotConfig(string botTag, string botId)
        {
            BotTag = botTag;
            BotId = botId;
        }

        public string BotTag { get; }
        public string BotId { get; }

        public string ConfigName => $"{BotTag}_{BotId}.ini";

        public string LogName => $"{BotTag}_b3.log";
    }
}