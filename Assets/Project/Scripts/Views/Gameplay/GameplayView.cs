using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Dominoes.Views.Gameplay
{
    internal class GameplayView : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private TileView _tilePrefab;
        [SerializeField] private GameObject _tableTiles;
        [SerializeField] private GameObject _cornerPointsObject;

        [Header("Texts")]
        [SerializeField] private LocalizeStringEvent _cornerPointsText;
        [SerializeField] private LocalizeStringEvent _stockTilesText;

        private IGameplayService _gameplayService;
        private IGzLogger<GameplayView> _logger;
        private int _cornerPoints;
        private int _stockTiles;

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<GameplayView>>();
        }

        #region Unity
        private void Awake()
        {
            switch (_gameState.GameType)
            {
                case GameType.Multiplayer:
                case GameType.PlayWithFriends:
                    _gameplayService = ServiceProvider.GetRequiredKeyedService<IGameplayService>(nameof(GameType.Multiplayer));
                    break;
                case GameType.SinglePlayer:
                    _gameplayService = ServiceProvider.GetRequiredKeyedService<IGameplayService>(nameof(GameType.SinglePlayer));
                    break;
            }
            _gameplayService.CornerPointsChanged += GameplayService_CornerPointsChanged;
            _gameplayService.StockTilesChanged += GameplayService_StockTilesChanged;
        }

        private void OnDestroy()
        {
            _gameplayService.CornerPointsChanged -= GameplayService_CornerPointsChanged;
            _gameplayService.StockTilesChanged -= GameplayService_StockTilesChanged;
        }

        private void Start()
        {
            if (_gameState.GameMode != GameMode.AllFives)
            {
                _cornerPointsObject.SetActive(false);
            }
        }
        #endregion

        #region Events
        private void GameplayService_CornerPointsChanged(int points)
        {
            _cornerPoints = points;
            (_cornerPointsText.StringReference["points"] as IntVariable).Value = _cornerPoints;
        }

        private void GameplayService_StockTilesChanged(int tiles)
        {
            _stockTiles = tiles;
            (_stockTilesText.StringReference["count"] as IntVariable).Value = _stockTiles;
        }
        #endregion
    }
}
