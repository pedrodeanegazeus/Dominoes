using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Repositories;
using Dominoes.Core.Models;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Dominoes/GameState")]
    internal class GameState : ScriptableObject
    {
        [Header("Game")]
        [SerializeField] private GameType _gameType;
        [SerializeField] private GameMode _gameMode;
        [SerializeField] private NumberPlayers _numberPlayers;

        [Header("Config")]
        [SerializeField] private GameConfig _gameConfig;

        private IGameStateRepository _gameStateRepository;
        private IGzLogger<GameState> _logger;

        public GameMode GameMode { get => _gameMode; set => _gameMode = value; }
        public GameType GameType { get => _gameType; set => _gameType = value; }
        public NumberPlayers NumberPlayers { get => _numberPlayers; set => _numberPlayers = value; }

        public bool Audio
        {
            get => _gameConfig.Audio;
            set
            {
                _gameConfig.Audio = value;
                _gameStateRepository.SaveGameConfig(_gameConfig);
            }
        }

        public int BotDifficulty
        {
            get => _gameConfig.BotDifficulty;
            set
            {
                _gameConfig.BotDifficulty = value;
                _gameStateRepository.SaveGameConfig(_gameConfig);
            }
        }

        public void Initialize()
        {
            _gameStateRepository = GameManager.ServiceProvider.GetRequiredService<IGameStateRepository>();
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<GameState>>();
        }

        public void Load()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Load));

            _gameConfig = _gameStateRepository.LoadGameConfig();
        }

        public void ResetState()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(ResetState));

            GameMode = GameMode.None;
            GameType = GameType.None;
            NumberPlayers = NumberPlayers.None;
        }
    }
}
