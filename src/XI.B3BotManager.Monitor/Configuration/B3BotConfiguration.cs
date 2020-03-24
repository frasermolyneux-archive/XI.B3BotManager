using System.Collections.Generic;
using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor.Configuration
{
    internal class B3BotConfiguration : IB3BotConfiguration
    {
        public IEnumerable<B3BotConfig> GetConfigurations()
        {
            // ReSharper disable StringLiteralTypo
            yield return new B3BotConfig("cod2_dm", "9897446d-861e-e911-90f6-a4bf012a19d1");
            yield return new B3BotConfig("cod2_tdm", "7b4c4374-861e-e911-90f6-a4bf012a19d1");
            yield return new B3BotConfig("cod4_ftag1", "1ae21225-861e-e911-90f6-a4bf012a19d1");
            yield return new B3BotConfig("cod4_mw2_ftag", "a133de1c-861e-e911-90f6-a4bf012a19d1");
            yield return new B3BotConfig("cod5_dm2", "54a0a750-861e-e911-90f6-a4bf012a19d1");
            yield return new B3BotConfig("cod5_vietnam_ftag", "273e1c49-861e-e911-90f6-a4bf012a19d1");
            // ReSharper restore StringLiteralTypo
        }
    }
}