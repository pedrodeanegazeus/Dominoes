using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Infrastructure.Repositories
{
    public class GameConfigRepository
    {
        private const string _gameConfigPlayerPrefsKey = "DOMINOES_GAME_CONFIG";

        private readonly IGzLogger<GameConfigRepository> _logger;

        private GameConfig _gameConfig;

        public GameConfigRepository(IGzLogger<GameConfigRepository> logger)
        {
            _logger = logger;
        }

        public GameConfig GetGameConfig()
        {
            _logger.LogMethodCall(nameof(GetGameConfig));

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
