using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Infrastructure.Repositories
{
    public class GameStateRepository
    {
        private const string _gameStatePlayerPrefsKey = "DOMINOES_GAME_STATE";

        private readonly IGzLogger<GameStateRepository> _logger;

        private GameState _gameState;

        public GameStateRepository(IGzLogger<GameStateRepository> logger)
        {
            _logger = logger;
        }

        public GameState GetGameState()
        {
            _logger.LogMethodCall(nameof(GetGameState));

            if (_gameState is null)
            {
                string json = PlayerPrefs.GetString(_gameStatePlayerPrefsKey, "{}");
                _gameState = JsonConvert.DeserializeObject<GameState>(json);
            }

            // debug
            _gameState.IsAudioOn = true;

            return _gameState;
        }
    }
}
