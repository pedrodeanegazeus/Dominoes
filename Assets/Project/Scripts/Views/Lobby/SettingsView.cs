using System;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Controllers.Components;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private SlideController _slideController;

        [Header("UI")]
        [SerializeField] private Button _closeButton;

        public event Action CloseButtonClicked;

        public event Action SlideOutCompleted
        {
            add => _slideController.SlideOutCompleted += value;
            remove => _slideController.SlideOutCompleted -= value;
        }

        private IGzLogger<SettingsView> _logger;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<SettingsView>>();
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }
        #endregion

        public void Hide()
        {
            _logger.LogMethodCall(nameof(Hide));

            _slideController.SlideOut();
        }

        public void Show()
        {
            _logger.LogMethodCall(nameof(Show));

            _slideController.SlideIn();
        }

        #region Events
        private void OnCloseButtonClick()
        {
            CloseButtonClicked?.Invoke();
        }
        #endregion
    }
}
