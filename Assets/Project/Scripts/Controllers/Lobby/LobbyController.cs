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
        private void GameTypeView_GameModeSelected()
        {
            _numberPlayersView.gameObject.SetActive(true);
            _numberPlayersView.Open();
        }

        private void LobbyView_GameTypeSelected()
        {
            _lobbyView.Hide();
            _gameTypeView.Show();

            _backButton.gameObject.SetActive(true);
            _moreGamesButton.gameObject.SetActive(false);
        }

        private void NumberPlayersView_NumberPlayersSelected()
        {
            GameSceneManager.Instance.LoadScene(DominoesScene.Gameplay);
        }
        #endregion

        private void GoToLobby()
        {
            _gameState.ResetState();

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
