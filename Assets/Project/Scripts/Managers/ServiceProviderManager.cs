using System;
using Gazeus.CoreMobile.SDK.Core;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Infrastructure.Repositories;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Managers
{
    public class ServiceProviderManager : MonoBehaviour
    {
        private IGzLogger<ServiceProviderManager> _logger;
        private IGzServiceProvider _serviceProvider;

        public TService GetService<TService>()
            where TService : class
        {
            _logger.LogMethodCall(nameof(GetService),
                                  typeof(TService).Name);

            try
            {
                TService service = _serviceProvider.GetService<TService>();
                return service;
            }
            catch (InvalidOperationException ex)
            {
                _logger.Error(ex.Message);

                return null;
            }
        }

        public void Initialize()
        {
            GzServiceCollection services = new();
            services.AddLoggerServices();

            AddRepositories(services);

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<IGzLogger<ServiceProviderManager>>();
            _logger.Info("Initialized");
        }

        private static void AddRepositories(GzServiceCollection services)
        {
            services.AddSingleton<GameConfigRepository>();
        }
    }
}
