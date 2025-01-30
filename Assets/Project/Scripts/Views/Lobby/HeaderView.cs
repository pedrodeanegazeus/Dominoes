#pragma warning disable UNT0006 // Incorrect message signature

using System;
using System.Diagnostics.CodeAnalysis;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Services;
using Gazeus.Mobile.Domino.Views.Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class HeaderView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _moreGamesButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _profileNameText;

        [Header("Localization")]
        [SerializeField] private LocalizedString _guestProfileKey;

        [Header("Views")]
        [SerializeField] private AvatarView _avatarView;

        public event Action SettingsButtonClicked;

        private const string _moreGamesUrl = "https://play.google.com/store/apps/dev?id=6748044026530676626";

        private ProfileService _profileService;
        private VipService _vipService;

        #region Unity
        private void Awake()
        {
            _profileService = GameManager.ServiceProviderManager.GetService<ProfileService>();
            _vipService = GameManager.ServiceProviderManager.GetService<VipService>();

            _guestProfileKey.StringChanged += GuestProfileKey_StringChanged;
        }

        private void OnDisable()
        {
            _moreGamesButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _moreGamesButton.onClick.AddListener(OnMoreGamesButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity Start method")]
        private async Awaitable Start()
        {
            if (_profileService.IsLoggedIn())
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _avatarView.SetAvatarSprite(avatarSprite);
            }

            _avatarView.SetAvatarVip(_vipService.IsVip);
        }
        #endregion

        #region Events
        private static void OnMoreGamesButtonClick()
        {
            Application.OpenURL(_moreGamesUrl);
        }

        private void OnSettingsButtonClick()
        {
            SettingsButtonClicked?.Invoke();
        }

        private void GuestProfileKey_StringChanged(string value)
        {
            _profileNameText.text = value;
        }
        #endregion
    }
}
