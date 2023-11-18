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
        [SerializeField] private GameMode _gameMode;

        public GameMode GameMode { get => _gameMode; set => _gameMode = value; }
        public GameType GameType { get => _gameType; set => _gameType = value; }
        public bool LoggerInitialized { get => _loggerInitialized; set => _loggerInitialized = value; }

        internal void Reset()
        {
            GameMode = GameMode.None;
            GameType = GameType.None;
        }

        #region Unity
        private void Awake()
        {
            _loggerInitialized = false;
            _gameType = GameType.None;
            _gameMode = GameMode.None;
        }
        #endregion
    }
}
