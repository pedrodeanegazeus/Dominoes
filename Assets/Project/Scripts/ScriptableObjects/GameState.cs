using Dominoes.Core.Enums;
using UnityEngine;

namespace Dominoes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameState", menuName = "Dominoes/GameState")]
    internal class GameState : ScriptableObject
    {
        [SerializeField] private bool _loggerInitialized;

        [Space]
        [SerializeField] private GameType _gameType;

        public GameType GameType { get => _gameType; set => _gameType = value; }
        public bool LoggerInitialized { get => _loggerInitialized; set => _loggerInitialized = value; }
    }
}
