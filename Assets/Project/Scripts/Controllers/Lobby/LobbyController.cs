using Dominoes.Core.Enums;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Lobby;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class LobbyController : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] private GameState _gameState;

        [Header("Header")]
        [SerializeField] private Button _moreGamesButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _settingsButton;

        [Header("Canvas views")]
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private GameTypeView _gameTypeView;
        [SerializeField] private NumberPlayersView _numberPlayersView;
        [SerializeField] private SideMenuView _sideMenuView;

        private GameMode _gameMode;
        private GameType _gameType;

        #region Unity
        private void Awake()
        {
            _lobbyView.Initialize();
            _gameTypeView.Initialize();
            _numberPlayersView.Initialize();
            _sideMenuView.Initialize();

            _backButton.onClick.AddListener(GoToLobby);
            _settingsButton.onClick.AddListener(OpenSideMenu);

            _lobbyView.GameTypeSelected += LobbyView_GameTypeSelected;
            _gameTypeView.GameModeSelected += GameTypeView_GameModeSelected;
            _numberPlayersView.NumberPlayersSelected += NumberPlayersView_NumberPlayersSelected;
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();

            _lobbyView.GameTypeSelected -= LobbyView_GameTypeSelected;
            _gameTypeView.GameModeSelected -= GameTypeView_GameModeSelected;
            _numberPlayersView.NumberPlayersSelected -= NumberPlayersView_NumberPlayersSelected;
        }

        private void Start()
        {
            GoToLobby();
        }
        #endregion

        #region Events
        private void GameTypeView_GameModeSelected(GameMode gameMode)
        {
            _gameMode = gameMode;
            _gameState.GameMode = gameMode;

            _numberPlayersView.gameObject.SetActive(true);
            _numberPlayersView.Open(gameMode);
        }

        private void LobbyView_GameTypeSelected(GameType gameType)
        {
            _gameType = gameType;
            _gameState.GameType = gameType;

            _lobbyView.Hide();
            _gameTypeView.Show(gameType);

            _backButton.gameObject.SetActive(true);
            _moreGamesButton.gameObject.SetActive(false);
        }

        private void NumberPlayersView_NumberPlayersSelected(NumberPlayers numberPlayers)
        {
            _gameState.NumberPlayers = numberPlayers;

            GameManager.Scene.LoadSceneWithParameter(DominoesScene.Gameplay, (_gameType, _gameMode, numberPlayers));
        }
        #endregion

        private void GoToLobby()
        {
            _gameState.ResetState();
            _gameType = GameType.None;
            _gameMode = GameMode.None;

            _lobbyView.Show();
            _gameTypeView.Hide();

            _backButton.gameObject.SetActive(false);
            _moreGamesButton.gameObject.SetActive(true);
        }

        private void OpenSideMenu()
        {
            _sideMenuView.gameObject.SetActive(true);
            _sideMenuView.Open();
        }
    }
}
