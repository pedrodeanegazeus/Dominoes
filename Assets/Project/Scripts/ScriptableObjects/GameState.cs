using Dominoes.Core.Enums;
using Dominoes.Core.Models;
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

        public GameMode GameMode { get => _gameMode; set => _gameMode = value; }
        public GameType GameType { get => _gameType; set => _gameType = value; }
        public NumberPlayers NumberPlayers { get => _numberPlayers; set => _numberPlayers = value; }

        public void ResetState()
        {
            GameMode = GameMode.None;
            GameType = GameType.None;
            NumberPlayers = NumberPlayers.None;
        }
    }
}
