using Dominoes.Core.Interfaces.Repositories;
using Dominoes.Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Dominoes.Infrastructure.Repositories
{
    internal class GameStateRepository : IGameStateRepository
    {
        private const string _gameConfigKey = "DOMINOES_GAME_CONFIG";

        public GameConfig LoadGameConfig()
        {
            string gameConfigJson = PlayerPrefs.GetString(_gameConfigKey, "{}");
            GameConfig gameConfig = JsonConvert.DeserializeObject<GameConfig>(gameConfigJson);
            return gameConfig;
        }

        public void SaveGameConfig(GameConfig gameConfig)
        {
            string gameConfigJson = JsonConvert.SerializeObject(gameConfig);
            PlayerPrefs.SetString(_gameConfigKey, gameConfigJson);
        }
    }
}
