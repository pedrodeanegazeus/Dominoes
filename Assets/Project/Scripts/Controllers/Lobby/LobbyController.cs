using Dominoes.Components;
using Dominoes.Controllers.Lobby;
using Dominoes.Core.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class LobbyController : MonoBehaviour
    {
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
            // debug helper
            if (!DominoesServiceProvider.IsBuilt)
            {
                _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Start));
                return;
            }

            _backButton.onClick.AddListener(GoToLobby);
            _settingsButton.onClick.AddListener(_sideMenuCanvasController.OpenSideMenu);

            _lobbyCanvasController.GameTypeSelected += LobbyCanvasController_SelectedGameType;
        }

        private void Start()
        {
            GoToLobby();
        }
        #endregion

        #region Events
        private void LobbyCanvasController_SelectedGameType(GameType gameType)
        {
            GoToGameTypeLobby(gameType);
        }
        #endregion

        private void GoToLobby()
        {
            _backButton.gameObject.SetActive(false);
            _lobbyCanvasController.gameObject.SetActive(true);
            _gameTypeCanvasController.gameObject.SetActive(false);
            _moreGamesButton.gameObject.SetActive(true);
        }

        private void GoToGameTypeLobby(GameType gameType)
        {
            _backButton.gameObject.SetActive(true);
            _lobbyCanvasController.gameObject.SetActive(false);
            _gameTypeCanvasController.gameObject.SetActive(true);
            _moreGamesButton.gameObject.SetActive(false);

            _gameTypeCanvasController.SetGameType(gameType);
        }
    }
}
