using Dominoes.Core.Interfaces.Repositories;
using Dominoes.Core.Models;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace Dominoes.Infrastructure.Repositories
{
    internal class GameConfigRepository : IGameConfigRepository
    {
        private const string _gameConfigKey = "DOMINOES_GAME_CONFIG";

        private readonly IGzLogger<GameConfigRepository> _logger;
        private GameConfig _gameConfig;
        private bool _synced;

        public GameConfig GameConfig
        {
            get
            {
                if (!_synced)
                {
                    SyncGameConfigFromPlayerPrefs();
                }
                return _gameConfig;
            }
            set
            {
                _gameConfig = value;
                SyncGameConfigToPlayerPrefs(_gameConfig);
            }
        }

        public GameConfigRepository(IGzLogger<GameConfigRepository> logger)
        {
            _logger = logger;
        }

        public void Sync()
        {
            SyncGameConfigToPlayerPrefs(GameConfig);
        }

        private void SyncGameConfigFromPlayerPrefs()
        {
            string gameConfigJson = PlayerPrefs.GetString(_gameConfigKey, "{}");
            _gameConfig = JsonConvert.DeserializeObject<GameConfig>(gameConfigJson);
            _synced = true;

            _logger.Info("Game config synced from player prefs");
        }

        private void SyncGameConfigToPlayerPrefs(GameConfig gameConfig)
        {
            string gameConfigJson = JsonConvert.SerializeObject(gameConfig);
            PlayerPrefs.SetString(_gameConfigKey, gameConfigJson);

            _logger.Info("Game config synced to player prefs");
        }
    }
}
