using System;
using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GameService;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class GameTypeView : MonoBehaviour
    {
        public event Action<GameMode> GameModeSelected;

        [SerializeField] private GameObject _jogatinaLogo;

        [Header("Titles")]
        [SerializeField] private GameObject _singlePlayerTitle;
        [SerializeField] private GameObject _multiplayerTitle;
        [SerializeField] private GameObject _playWithFriendsTitle;

        [Header("Buttons")]
        [SerializeField] private Button _drawButton;
        [SerializeField] private Button _blockButton;
        [SerializeField] private Button _allFivesButton;
        [SerializeField] private Button _turboButton;
        [SerializeField] private GameObject _promotional;
        [SerializeField] private GameObject _vipButton;

        [Header("Texts")]
        [SerializeField] private LocalizeStringEvent _drawPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _blockPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _allFivesPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _turboPlayersOnlineText;

        private IGzLogger<GameTypeView> _logger;
        private IGameService _gameService;
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _drawButton.onClick.AddListener(() => GameModeSelected?.Invoke(GameMode.Draw));
            _blockButton.onClick.AddListener(() => GameModeSelected?.Invoke(GameMode.Block));
            _allFivesButton.onClick.AddListener(() => GameModeSelected?.Invoke(GameMode.AllFives));
            _turboButton.onClick.AddListener(() => GameModeSelected?.Invoke(GameMode.Turbo));
        }

        private void OnDestroy()
        {
            _drawButton.onClick.RemoveAllListeners();
            _blockButton.onClick.RemoveAllListeners();
            _allFivesButton.onClick.RemoveAllListeners();
            _turboButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            SetVip(_vipService.IsVip);
        }
        #endregion

        public void Hide()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Hide));

            gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<GameTypeView>>();
            _gameService = GameManager.ServiceProvider.GetRequiredService<IGameService>();
            _vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
        }

        public void Show(GameType gameType)
        {
            _logger.Debug("CALLED: {method} - {gameType}",
                          nameof(Show),
                          gameType);

            gameObject.SetActive(true);
            switch (gameType)
            {
                case GameType.Multiplayer:
                    SetMultiplayerGameType();
                    break;
                case GameType.PlayWithFriends:
                    SetPlayWithFriendsGameType();
                    break;
                case GameType.SinglePlayer:
                    SetSinglePlayerGameType();
                    break;
            }
        }

        private void SetMultiplayerGameType()
        {
            _multiplayerTitle.SetActive(true);
            _playWithFriendsTitle.SetActive(false);
            _singlePlayerTitle.SetActive(false);

            _allFivesPlayersOnlineText.gameObject.SetActive(true);
            _blockPlayersOnlineText.gameObject.SetActive(true);
            _drawPlayersOnlineText.gameObject.SetActive(true);
            _turboPlayersOnlineText.gameObject.SetActive(true);

            Task<PlayersOnline> playersOnlineTask = _gameService.GetPlayersOnlineAsync();
            _ = StartCoroutine(playersOnlineTask.WaitTask(result =>
            {
                (_allFivesPlayersOnlineText.StringReference["count"] as IntVariable).Value = result.AllFives;
                (_blockPlayersOnlineText.StringReference["count"] as IntVariable).Value = result.Block;
                (_drawPlayersOnlineText.StringReference["count"] as IntVariable).Value = result.Draw;
                (_turboPlayersOnlineText.StringReference["count"] as IntVariable).Value = result.Turbo;
            }));
        }

        private void SetPlayWithFriendsGameType()
        {
            _multiplayerTitle.SetActive(false);
            _playWithFriendsTitle.SetActive(true);
            _singlePlayerTitle.SetActive(false);

            _allFivesPlayersOnlineText.gameObject.SetActive(false);
            _blockPlayersOnlineText.gameObject.SetActive(false);
            _drawPlayersOnlineText.gameObject.SetActive(false);
            _turboPlayersOnlineText.gameObject.SetActive(false);
        }

        private void SetSinglePlayerGameType()
        {
            _multiplayerTitle.SetActive(false);
            _playWithFriendsTitle.SetActive(false);
            _singlePlayerTitle.SetActive(true);

            _allFivesPlayersOnlineText.gameObject.SetActive(false);
            _blockPlayersOnlineText.gameObject.SetActive(false);
            _drawPlayersOnlineText.gameObject.SetActive(false);
            _turboPlayersOnlineText.gameObject.SetActive(false);
        }

        private void SetVip(bool isVip)
        {
            _jogatinaLogo.SetActive(isVip);
            _promotional.SetActive(!isVip);
            _vipButton.SetActive(!isVip);
        }
    }
}
