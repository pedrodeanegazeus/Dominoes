using Dominoes.Components;
using Dominoes.Controllers.Lobby;
using Dominoes.Core.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    internal class LobbyController : MonoBehaviour
    {
        [Header("Canvas controllers")]
        [SerializeField] private LobbyCanvasController _lobbyCanvasController;
        [SerializeField] private GameTypeCanvasController _gameTypeCanvasController;

        #region Unity
        private void Awake()
        {
            // debug helper
            if (!DominoesServiceProvider.IsBuilt)
            {
                _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Start));
                return;
            }

            _lobbyCanvasController.GameTypeSelected += LobbyCanvasController_SelectedGameType;
            _gameTypeCanvasController.BackButtonClicked += GameTypeCanvasController_BackButtonClicked;
        }

        private void Start()
        {
            GoToLobby();
        }
        #endregion

        #region Events
        private void GameTypeCanvasController_BackButtonClicked()
        {
            GoToLobby();
        }

        private void LobbyCanvasController_SelectedGameType(GameType gameType)
        {
            GoToGameTypeLobby(gameType);
        }
        #endregion

        private void GoToLobby()
        {
            _lobbyCanvasController.gameObject.SetActive(true);
            _gameTypeCanvasController.gameObject.SetActive(false);
        }

        private void GoToGameTypeLobby(GameType gameType)
        {
            _lobbyCanvasController.gameObject.SetActive(false);
            _gameTypeCanvasController.gameObject.SetActive(true);
            _gameTypeCanvasController.SetGameType(gameType);
        }
    }
}
