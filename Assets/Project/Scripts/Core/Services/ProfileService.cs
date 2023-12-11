﻿using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Dominoes.Core.Services
{
    internal class ProfileService : IProfileService
    {
        private readonly IGzLogger<ProfileService> _logger;

        public bool IsGuest { get; private set; }
        public bool IsLoggedIn { get; private set; }

        public ProfileService(IGzLogger<ProfileService> logger)
        {
            _logger = logger;
        }

        public Task<string> GetProfileNameAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(GetProfileNameAsync));

            AsyncOperationHandle<string> handle = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Profile Strings", "guest");
            return handle.Task;
        }

        public void Initialize()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Initialize));

            IsGuest = true;
        }
    }
}
