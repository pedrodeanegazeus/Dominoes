using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GazeusServicesService;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class GameTypeCanvasController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
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

        private IGzLogger<GameTypeCanvasController> _logger;
        private IMultiplayerService _gazeusServicesService;
        private IVipService _vipService;

        public void Hide()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Hide));

            gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<GameTypeCanvasController>>();
            _gazeusServicesService = ServiceProvider.GetRequiredService<IMultiplayerService>();
            _vipService = ServiceProvider.GetRequiredService<IVipService>();
        }

        public void Show()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Show));

            gameObject.SetActive(true);
            switch (_gameState.GameType)
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

        #region Unity
        private void Awake()
        {
            _drawButton.onClick.AddListener(() => SetGameMode(GameMode.Draw));
            _blockButton.onClick.AddListener(() => SetGameMode(GameMode.Block));
            _allFivesButton.onClick.AddListener(() => SetGameMode(GameMode.AllFives));
            _turboButton.onClick.AddListener(() => SetGameMode(GameMode.Turbo));
        }

        private void Start()
        {
            SetVip(_vipService.IsVip);
        }
        #endregion

        private void SetGameMode(GameMode gameMode)
        {
            _gameState.GameMode = gameMode;
            GameSceneManager.Instance.LoadScene(DominoesScene.Gameplay);
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

            Task<PlayersOnline> playersOnlineTask = _gazeusServicesService.GetPlayersOnlineAsync();
            _ = StartCoroutine(playersOnlineTask.WaitForTaskCompleteRoutine(result =>
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
