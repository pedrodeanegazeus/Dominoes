using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Repositories;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Services;
using Dominoes.Core.Services.Gameplay;
using Dominoes.Infrastructure.Repositories;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Managers
{
    internal class ServiceProviderManager : MonoBehaviour
    {
        public static ServiceProviderManager Instance { get; private set; }

        private IGzServiceProvider _serviceProvider;

        public void Initialize()
        {
            GzServiceCollection serviceCollection = new();

            // Repositories
            serviceCollection.AddSingleton<IGameStateRepository, GameStateRepository>();

            // Services
            serviceCollection.AddKeyedTransient<IGameplayService, MultiplayerGameplayService>(GameType.Multiplayer);
            serviceCollection.AddKeyedTransient<IGameplayService, SinglePlayerGameplayService>(GameType.SinglePlayer);

            serviceCollection.AddSingleton<IGameService, GameService>();
            serviceCollection.AddSingleton<IProfileService, ProfileService>();
            serviceCollection.AddSingleton<IVipService, VipService>();

            serviceCollection.AddTransient<IChatService, ChatService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public TService GetRequiredKeyedService<TService>(object serviceKey)
            where TService : class
        {
            TService service = _serviceProvider.GetRequiredKeyedService<TService>(serviceKey);
            return service;
        }

        public TService GetRequiredService<TService>()
            where TService : class
        {
            TService service = _serviceProvider.GetRequiredService<TService>();
            return service;
        }

        #region Unity
        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
