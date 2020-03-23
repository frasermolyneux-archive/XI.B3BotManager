using System.Collections.Generic;

namespace XI.B3BotManager.Monitor.Configuration
{
    internal class B3BotConfiguration : IB3BotConfiguration
    {
        public IEnumerable<string> GetConfigurations()
        {
            // ReSharper disable StringLiteralTypo
            yield return "cod2_dm_9897446d-861e-e911-90f6-a4bf012a19d1.ini";
            yield return "cod2_tdm_7b4c4374-861e-e911-90f6-a4bf012a19d1.ini";
            yield return "cod4_ftag1_1ae21225-861e-e911-90f6-a4bf012a19d1.ini";
            yield return "cod4_mw2_ftag_a133de1c-861e-e911-90f6-a4bf012a19d1.ini";
            yield return "cod5_dm2_54a0a750-861e-e911-90f6-a4bf012a19d1.ini";
            yield return "cod5_vietnam_ftag_273e1c49-861e-e911-90f6-a4bf012a19d1.ini";
            // ReSharper restore StringLiteralTypo
        }
    }
}