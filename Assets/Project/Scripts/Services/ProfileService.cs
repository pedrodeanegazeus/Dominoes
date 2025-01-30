using System;
using System.Threading.Tasks;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Services.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Services.Core.Models;
using Gazeus.Mobile.Domino.Infrastructure.Clients;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Services
{
    public class ProfileService
    {
        public event Action ProfileUpdated;

        private readonly IGzLogger<ProfileService> _logger;
        private readonly IGzServices _gzServices;
        private readonly GenericClient _genericClient;

        private Sprite _avatarSprite;
        private AuthenticationData _authenticationData;

        public bool IsLoggedIn => _authenticationData.IsLoggedIn;
        public string Name => _authenticationData.Name;

        public ProfileService(IGzLogger<ProfileService> logger,
                              IGzServices gzServices,
                              GenericClient genericClient)
        {
            _logger = logger;
            _gzServices = gzServices;
            _genericClient = genericClient;

            _gzServices.AuthenticationService.AuthenticationDataChanged += AuthenticationService_AuthenticationDataChanged;
        }

        public async Task<Sprite> GetAvatarSpriteAsync()
        {
            _logger.LogMethodCall(nameof(GetAvatarSpriteAsync));

            AuthenticationData authenticationData = _gzServices.AuthenticationService.GetAuthenticationData();

            if (_avatarSprite == null)
            {
                _avatarSprite = await CreateAvatarSpriteAsync(authenticationData.AvatarUrl);
            }

            return _avatarSprite;
        }

        #region Events
        private async void AuthenticationService_AuthenticationDataChanged(AuthenticationData authenticationData)
        {
            Sprite avatarSprite = await CreateAvatarSpriteAsync(authenticationData.AvatarUrl);

            _avatarSprite = avatarSprite;
            _authenticationData = authenticationData;

            ProfileUpdated?.Invoke();
        }
        #endregion

        private async Task<Sprite> CreateAvatarSpriteAsync(string avatarUrl)
        {
            Texture2D avatarTexture = await _genericClient.GetTextureAsync(avatarUrl);

            Rect avatarRect = new(0f, 0f, avatarTexture.width, avatarTexture.height);
            Vector2 avatarPivot = new(0.5f, 0.5f);
            Sprite avatarSprite = Sprite.Create(avatarTexture, avatarRect, avatarPivot);

            return avatarSprite;
        }
    }
}
