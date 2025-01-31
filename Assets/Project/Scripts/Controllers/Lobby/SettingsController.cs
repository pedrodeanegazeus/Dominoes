using System.Threading.Tasks;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Services;
using Gazeus.Mobile.Domino.Views.Lobby;
using UnityEngine;
using UnityEngine.Localization;

namespace Gazeus.Mobile.Domino.Controllers.Lobby
{
    public class SettingsController
    {
        private readonly IGzLogger<SettingsController> _logger;
        private readonly ProfileService _profileService;
        private readonly VipService _vipService;

        private LocalizedString _guestProfileKey;
        private SettingsView _settingsView;

        public SettingsController(IGzLogger<SettingsController> logger,
                                  ProfileService profileService,
                                  VipService vipService)
        {
            _logger = logger;
            _vipService = vipService;
            _profileService = profileService;

            _profileService.ProfileUpdated += ProfileService_ProfileUpdated;
        }

        public void Initialize(LocalizedString guestProfileKey, SettingsView settingsView)
        {
            _logger.LogMethodCall(nameof(Initialize));

            _guestProfileKey = guestProfileKey;
            _guestProfileKey.StringChanged += GuestProfileKey_StringChanged;

            _settingsView = settingsView;
            _settingsView.gameObject.SetActive(false);

            _settingsView.SlideOutCompleted += SettingsView_SlideOutCompleted;
        }

        public async Task ShowAsync()
        {
            _logger.LogMethodCall(nameof(ShowAsync));

            if (_profileService.IsLoggedIn)
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _settingsView.SetAvatarSprite(avatarSprite);
                _settingsView.SetProfileName(_profileService.Name);
            }

            _settingsView.SetAvatarVip(_vipService.IsVip);
            _settingsView.SetLoggedIn(_profileService.IsLoggedIn);
            _settingsView.gameObject.SetActive(true);

            _settingsView.Show();
        }

        #region Events
        private void GuestProfileKey_StringChanged(string value)
        {
            if (!_profileService.IsLoggedIn)
            {
                _settingsView.SetProfileName(value);
            }
        }

        private async void ProfileService_ProfileUpdated()
        {
            if (_profileService.IsLoggedIn)
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _settingsView.SetAvatarSprite(avatarSprite);
                _settingsView.SetProfileName(_profileService.Name);
            }
            else
            {
                string guestText = await _guestProfileKey.GetLocalizedStringAsync().Task;
                _settingsView.SetAvatarSprite(null);
                _settingsView.SetProfileName(guestText);
            }

            _settingsView.SetLoggedIn(_profileService.IsLoggedIn);
        }

        private void SettingsView_SlideOutCompleted()
        {
            _settingsView.gameObject.SetActive(false);
        }
        #endregion
    }
}
