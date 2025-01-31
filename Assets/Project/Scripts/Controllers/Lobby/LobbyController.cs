#pragma warning disable UNT0006 // Incorrect message signature

using System.Diagnostics.CodeAnalysis;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Services;
using Gazeus.Mobile.Domino.Views.Lobby;
using UnityEngine;
using UnityEngine.Localization;

namespace Gazeus.Mobile.Domino.Controllers.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private HeaderView _headerView;
        [SerializeField] private SettingsView _settingsView;

        [Header("Localization")]
        [SerializeField] private LocalizedString _guestProfileKey;

        private ProfileService _profileService;
        private SettingsController _settingsController;
        private VipService _vipService;

        #region Unity
        private void Awake()
        {
            _profileService = GameManager.ServiceProviderManager.GetService<ProfileService>();
            _settingsController = GameManager.ServiceProviderManager.GetService<SettingsController>();
            _vipService = GameManager.ServiceProviderManager.GetService<VipService>();

            _settingsController.Awake(_guestProfileKey, _settingsView);
        }

        private void OnDisable()
        {
            _headerView.SettingsButtonClicked -= HeaderView_SettingsButtonClicked;
            _guestProfileKey.StringChanged -= GuestProfileKey_StringChanged;
            _profileService.ProfileUpdated -= ProfileService_ProfileUpdated;

            _settingsController.OnDisable();
        }

        private void OnEnable()
        {
            _headerView.SettingsButtonClicked += HeaderView_SettingsButtonClicked;
            _guestProfileKey.StringChanged += GuestProfileKey_StringChanged;
            _profileService.ProfileUpdated += ProfileService_ProfileUpdated;

            _settingsController.OnEnable();
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity Start method")]
        private async Awaitable Start()
        {
            if (_profileService.IsLoggedIn)
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _headerView.SetAvatarSprite(avatarSprite);
                _headerView.SetProfileName(_profileService.Name);
            }

            _headerView.SetAvatarVip(_vipService.IsVip);

            _settingsController.Start();
        }
        #endregion

        #region Events
        private void GuestProfileKey_StringChanged(string value)
        {
            if (!_profileService.IsLoggedIn)
            {
                _headerView.SetProfileName(value);
            }
        }

        private async void HeaderView_SettingsButtonClicked()
        {
            await _settingsController.ShowAsync();
        }

        private async void ProfileService_ProfileUpdated()
        {
            if (_profileService.IsLoggedIn)
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _headerView.SetAvatarSprite(avatarSprite);
                _headerView.SetProfileName(_profileService.Name);
            }
            else
            {
                string guestText = await _guestProfileKey.GetLocalizedStringAsync().Task;
                _headerView.SetAvatarSprite(null);
                _headerView.SetProfileName(guestText);
            }
        }
        #endregion
    }
}
