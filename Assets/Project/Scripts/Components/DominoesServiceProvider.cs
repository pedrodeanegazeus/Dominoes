using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dominoes.Components
{
    internal class DominoesServiceProvider : UIBehaviour
    {
        private GzServiceCollection _serviceCollection;
        private static bool _isInitialized;

        public static IGzServiceProvider GzServiceProvider { get; private set; }
        public static bool IsBuilt { get; private set; }

        public void AddSingleton<TService, TImplementation>()
            where TImplementation : class, TService
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddTransient<TService, TImplementation>()
            where TImplementation : class, TService
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public void Build()
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            GzServiceProvider = _serviceCollection.BuildServiceProvider();
            IsBuilt = true;
        }

        public TService GetRequiredService<TService>()
            where TService : class
        {
            TService service = GzServiceProvider.GetRequiredService<TService>();
            return service;
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                Debug.LogWarning("Service provider is already initialized, you are probably doing something wrong");
            }
            _serviceCollection = new GzServiceCollection();
            _isInitialized = true;
        }
    }
}
