using System;

namespace Dominoes.Core.Models
{
    [Serializable]
    internal class GameConfig
    {
        public bool Audio;
        public int BotDifficulty;

        public GameConfig()
        {
            Audio = true;
            BotDifficulty = 2;
        }
    }
}
