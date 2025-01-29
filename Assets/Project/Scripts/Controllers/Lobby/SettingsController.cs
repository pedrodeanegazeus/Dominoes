using System.Threading.Tasks;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Services;
using Gazeus.Mobile.Domino.Views.Lobby;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Controllers.Lobby
{
    public class SettingsController
    {
        private readonly IGzLogger<SettingsController> _logger;
        private readonly ProfileService _profileService;
        private readonly VipService _vipService;

        private SettingsView _settingsView;

        public SettingsController(IGzLogger<SettingsController> logger,
                                  ProfileService profileService,
                                  VipService vipService)
        {
            _logger = logger;
            _vipService = vipService;
            _profileService = profileService;
        }

        public void Initialize(SettingsView settingsView)
        {
            _logger.LogMethodCall(nameof(Initialize));

            _settingsView = settingsView;
            _settingsView.gameObject.SetActive(false);

            _settingsView.SlideOutCompleted += SettingsView_SlideOutCompleted;
        }

        public async Task ShowAsync()
        {
            _logger.LogMethodCall(nameof(ShowAsync));

            if (_profileService.IsLoggedIn())
            {
                Sprite avatarSprite = await _profileService.GetAvatarSpriteAsync();
                _settingsView.SetAvatarSprite(avatarSprite);
            }

            _settingsView.SetAvatarVip(_vipService.IsVip);
            _settingsView.gameObject.SetActive(true);

            _settingsView.Show();
        }

        #region Events
        private void SettingsView_SlideOutCompleted()
        {
            _settingsView.gameObject.SetActive(false);
        }
        #endregion
    }
}
