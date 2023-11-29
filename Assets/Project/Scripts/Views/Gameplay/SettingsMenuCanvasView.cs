using Dominoes.Animations;
using Dominoes.Core.Enums;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Views.Gameplay
{
    internal class SettingsMenuCanvasView : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _leaveButton;

        private IGzLogger<SettingsMenuCanvasView> _logger;

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<SettingsMenuCanvasView>>();
        }

        public void Open()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Open));

            _slideAnimation.SlideIn();
        }

        #region Unity
        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseSideMenu);
            _leaveButton.onClick.AddListener(LeaveGame);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _leaveButton.onClick.RemoveAllListeners();
        }
        #endregion

        private void CloseSideMenu()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }

        private void LeaveGame()
        {
            GameSceneManager.Instance.LoadScene(DominoesScene.Lobby);
        }
    }
}
