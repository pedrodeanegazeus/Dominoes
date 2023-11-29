using Dominoes.ScriptableObjects;
using Dominoes.Views.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Header("Header")]
        [SerializeField] private Button _chatButton;
        [SerializeField] private Button _settingsButton;

        [Header("Canvas views")]
        [SerializeField] private SettingsMenuCanvasView _settingsMenuCanvasView;

        #region Unity
        private void Awake()
        {
            _settingsMenuCanvasView.Initialize();

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }
        #endregion

        private void OpenChat()
        {
        }

        private void OpenSettingsMenu()
        {
            _settingsMenuCanvasView.gameObject.SetActive(true);
            _settingsMenuCanvasView.Open();
        }
    }
}
