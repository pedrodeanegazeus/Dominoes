using Gazeus.Mobile.Domino.Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Infrastructure.Repositories
{
    public class GameConfigRepository
    {
        private const string _gameConfigPlayerPrefsKey = "DOMINOES_GAME_CONFIG";

        private GameConfig _gameConfig;

        public GameConfig GetGameConfig()
        {
            if (_gameConfig is null)
            {
                string json = PlayerPrefs.GetString(_gameConfigPlayerPrefsKey, "{}");
                _gameConfig = JsonConvert.DeserializeObject<GameConfig>(json);
            }

            // debug
            _gameConfig.IsAudioOn = true;

            return _gameConfig;
        }
    }
}
