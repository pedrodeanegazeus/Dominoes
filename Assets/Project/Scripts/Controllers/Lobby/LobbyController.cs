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
        [SerializeField] private LobbyCanvasView _lobbyCanvasView;
        [SerializeField] private GameTypeCanvasView _gameTypeCanvasView;
        [SerializeField] private SideMenuCanvasView _sideMenuCanvasView;

        #region Unity
        private void Awake()
        {
            _lobbyCanvasView.Initialize();
            _gameTypeCanvasView.Initialize();
            _sideMenuCanvasView.Initialize();

            _backButton.onClick.AddListener(GoToLobby);
            _settingsButton.onClick.AddListener(OpenSideMenu);

            _lobbyCanvasView.GameTypeSelected += GoToGameType;
        }

        private void Start()
        {
            GoToLobby();
        }
        #endregion

        private void GoToGameType()
        {
            _lobbyCanvasView.Hide();
            _gameTypeCanvasView.Show();

            _backButton.gameObject.SetActive(true);
            _moreGamesButton.gameObject.SetActive(false);
        }

        private void GoToLobby()
        {
            _gameState.ResetState();

            _lobbyCanvasView.Show();
            _gameTypeCanvasView.Hide();

            _backButton.gameObject.SetActive(false);
            _moreGamesButton.gameObject.SetActive(true);
        }

        private void OpenSideMenu()
        {
            _sideMenuCanvasView.gameObject.SetActive(true);
            _sideMenuCanvasView.Open();
        }
    }
}
