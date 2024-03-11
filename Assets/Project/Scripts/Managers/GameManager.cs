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
    internal class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameSceneManager _gameSceneManager;

        public static IGzServiceProvider ServiceProvider { get; private set; }
        public static AudioManager Audio { get; private set; }
        public static GameSceneManager Scene { get; private set; }

        #region Unity
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        public void Initialize()
        {
            // service provider always first
            InitializeServiceProvider();

            InitializeAudioManager();
            InitializeGameSceneManager();
        }

        private void InitializeAudioManager()
        {
            _audioManager.Initialize(ServiceProvider);
            Audio = _audioManager;
        }

        private void InitializeGameSceneManager()
        {
            _gameSceneManager.Initialize(ServiceProvider);
            Scene = _gameSceneManager;
        }

        private static void InitializeServiceProvider()
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

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
