using Dominoes.Controllers.Lobby;
using Dominoes.ScriptableObjects;
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

        [Header("Canvas controllers")]
        [SerializeField] private LobbyCanvasController _lobbyCanvasController;
        [SerializeField] private GameTypeCanvasController _gameTypeCanvasController;
        [SerializeField] private SideMenuCanvasController _sideMenuCanvasController;

        #region Unity
        private void Awake()
        {
            _lobbyCanvasController.Initialize();
            _gameTypeCanvasController.Initialize();
            _sideMenuCanvasController.Initialize();

            _backButton.onClick.AddListener(GoToLobby);
            _settingsButton.onClick.AddListener(OpenSideMenu);

            _lobbyCanvasController.GameTypeSelected += GoToGameType;
        }

        private void Start()
        {
            GoToLobby();
        }
        #endregion

        private void GoToGameType()
        {
            _lobbyCanvasController.Hide();
            _gameTypeCanvasController.Show();

            _backButton.gameObject.SetActive(true);
            _moreGamesButton.gameObject.SetActive(false);
        }

        private void GoToLobby()
        {
            _gameState.Reset();

            _lobbyCanvasController.Show();
            _gameTypeCanvasController.Hide();

            _backButton.gameObject.SetActive(false);
            _moreGamesButton.gameObject.SetActive(true);
        }

        private void OpenSideMenu()
        {
            _sideMenuCanvasController.gameObject.SetActive(true);
            _sideMenuCanvasController.Open();
        }
    }
}
