using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Dominoes.Views.Gameplay
{
    internal class GameplayView : MonoBehaviour
    {
        [SerializeField] private TileView _tilePrefab;
        [SerializeField] private GameObject _tableTiles;
        [SerializeField] private GameObject _cornerPointsObject;
        [SerializeField] private GameObject _stockTilesObject;

        [Header("Texts")]
        [SerializeField] private LocalizeStringEvent _cornerPointsText;
        [SerializeField] private LocalizeStringEvent _stockTilesText;

        private IGameplayService _gameplayService;

        #region Unity
        private void OnDestroy()
        {
            _gameplayService.CornerPointsChanged -= GameplayService_CornerPointsChanged;
            _gameplayService.StockTilesChanged -= GameplayService_StockTilesChanged;
        }
        #endregion

        public void Initialize(GameType gameType, GameMode gameMode)
        {
            _gameplayService = gameType switch
            {
                GameType.Multiplayer or GameType.PlayWithFriends => GameManager.ServiceProvider.GetRequiredKeyedService<IGameplayService>(GameType.Multiplayer),
                GameType.SinglePlayer => GameManager.ServiceProvider.GetRequiredKeyedService<IGameplayService>(GameType.SinglePlayer),
                _ => throw new NotImplementedException($"Game type {gameType} not implemented"),
            };
            _gameplayService.CornerPointsChanged += GameplayService_CornerPointsChanged;
            _gameplayService.StockTilesChanged += GameplayService_StockTilesChanged;

            switch (gameMode)
            {
                case GameMode.Draw:
                    _cornerPointsObject.SetActive(false);
                    break;
                case GameMode.Block:
                    _cornerPointsObject.SetActive(false);
                    _stockTilesObject.SetActive(false);
                    break;
                case GameMode.Turbo:
                    _cornerPointsObject.SetActive(false);
                    break;
            }
        }

        #region Events
        private void GameplayService_CornerPointsChanged(int points)
        {
            (_cornerPointsText.StringReference["points"] as IntVariable).Value = points;
        }

        private void GameplayService_StockTilesChanged(int tiles)
        {
            (_stockTilesText.StringReference["count"] as IntVariable).Value = tiles;
        }
        #endregion
    }
}
