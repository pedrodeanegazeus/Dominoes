using System;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Components;
using Gazeus.Mobile.Domino.Core.Enum;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class SettingsView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image _overlayImage;
        [SerializeField] private Button _closeButton;

        [Space]
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _loginButton;

        [Header("Views")]
        [SerializeField] private AvatarView _avatarView;
        [SerializeField] private TextMeshProUGUI _profileNameText;

        [Space]
        [SerializeField] private SlideComponent _slideComponent;
        [SerializeField] private float _overlayAlpha;
        [SerializeField] private float _overlayFadeDuration;

        public event Action SlideOutCompleted
        {
            add => _slideComponent.SlideOutCompleted += value;
            remove => _slideComponent.SlideOutCompleted -= value;
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
            _profileButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _profileButton.onClick.AddListener(OnProfileButtonClick);
        }
        #endregion

        public void SetAvatarSprite(Sprite sprite)
        {
            _logger.LogMethodCall(nameof(SetAvatarSprite));

            _avatarView.SetAvatarSprite(sprite);
        }

        public void SetAvatarVip(bool isVip)
        {
            _logger.LogMethodCall(nameof(SetAvatarVip),
                                  isVip);

            _avatarView.SetAvatarVip(isVip);
        }

        public void SetLoggedIn(bool isLoggedIn)
        {
            _logger.LogMethodCall(nameof(SetLoggedIn),
                                  isLoggedIn);

            _loginButton.gameObject.SetActive(!isLoggedIn);

            RectTransform profileButtonTransform = _profileButton.transform as RectTransform;
            if (isLoggedIn)
            {
                profileButtonTransform.anchorMax = Vector2.one;
                profileButtonTransform.offsetMax = new Vector2(-10f, -50f);
            }
            else
            {
                profileButtonTransform.anchorMax = Vector2.up;
                profileButtonTransform.offsetMax = new Vector2(97.5f, -50f);
            }
        }

        public void SetProfileName(string profileName)
        {
            _logger.LogMethodCall(nameof(SetProfileName),
                                  profileName);

            _profileNameText.text = profileName;
        }

        public void Show()
        {
            _logger.LogMethodCall(nameof(Show));

            Color imageOverlayColor = _overlayImage.color;
            imageOverlayColor.a = 0f;
            _overlayImage.color = imageOverlayColor;

            _overlayImage.DOFade(_overlayAlpha, _overlayFadeDuration);

            _slideComponent.SlideIn();
        }

        #region Events
        private void OnCloseButtonClick()
        {
            Hide();
        }

        private static void OnProfileButtonClick()
        {
            GameManager.GameSceneManager.LoadScene(GameScene.Profile);
        }
        #endregion

        private void Hide()
        {
            Color imageOverlayColor = _overlayImage.color;
            imageOverlayColor.a = _overlayAlpha;
            _overlayImage.color = imageOverlayColor;

            _overlayImage.DOFade(0, _overlayFadeDuration);

            _slideComponent.SlideOut();
        }
    }
}
