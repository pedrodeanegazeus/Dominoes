using System;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Controllers.Components;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Prefabs;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class SettingsView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image _overlayImage;
        [SerializeField] private Button _closeButton;

        [Header("Views")]
        [SerializeField] private AvatarView _avatarView;

        [Space]
        [SerializeField] private SlideController _slideController;
        [SerializeField] private float _overlayAlpha;
        [SerializeField] private float _overlayFadeDuration;

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

        public void SetAvatarSprite(Sprite sprite)
        {
            _logger.LogMethodCall(nameof(SetAvatarSprite),
                                  sprite);

            _avatarView.SetAvatarSprite(sprite);
        }

        public void SetAvatarVip(bool isVip)
        {
            _logger.LogMethodCall(nameof(SetAvatarVip),
                                  isVip);

            _avatarView.SetAvatarVip(isVip);
        }

        public void Show()
        {
            _logger.LogMethodCall(nameof(Show));

            Color imageOverlayColor = _overlayImage.color;
            imageOverlayColor.a = 0f;
            _overlayImage.color = imageOverlayColor;

            _overlayImage.DOFade(_overlayAlpha, _overlayFadeDuration);

            _slideController.SlideIn();
        }

        #region Events
        private void OnCloseButtonClick()
        {
            Hide();
        }
        #endregion

        private void Hide()
        {
            Color imageOverlayColor = _overlayImage.color;
            imageOverlayColor.a = _overlayAlpha;
            _overlayImage.color = imageOverlayColor;

            _overlayImage.DOFade(0, _overlayFadeDuration);

            _slideController.SlideOut();
        }
    }
}
