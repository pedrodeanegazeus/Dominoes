using System;
using Gazeus.CoreMobile.SDK.Core;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Services;
using Gazeus.CoreMobile.SDK.Services.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Services.ScriptableObjects;
using Gazeus.Mobile.Domino.Controllers.Lobby;
using Gazeus.Mobile.Domino.Infrastructure.Clients;
using Gazeus.Mobile.Domino.Infrastructure.Repositories;
using Gazeus.Mobile.Domino.Services;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Managers
{
    public class ServiceProviderManager : MonoBehaviour
    {
        [SerializeField] private AppConfiguration _appConfiguration;
        [SerializeField] private AppEnvironment _appEnvironment;

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

            AddClients(services);
            AddControllers(services);
            AddRepositories(services);
            AddServices(services);

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetRequiredService<IGzLogger<ServiceProviderManager>>();
            _logger.Info("Initialized");
        }

        private static void AddClients(GzServiceCollection services)
        {
            services.AddTransient<GenericClient>();
        }

        private static void AddControllers(GzServiceCollection services)
        {
            services.AddTransient<SettingsController>();
        }

        private static void AddRepositories(GzServiceCollection services)
        {
            services.AddSingleton<GameStateRepository>();
        }

        private void AddServices(GzServiceCollection services)
        {
            services.AddSingleton<IGzServices>(() =>
            {
                GzServices gzServices = new();
                gzServices.Initialize(_appConfiguration, _appEnvironment);

                return gzServices;
            });

            services.AddSingleton<ProfileService>();
            services.AddSingleton<VipService>();
        }
    }
}
