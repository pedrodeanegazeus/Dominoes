using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dominoes.Components
{
    public class DominoesServiceProvider : UIBehaviour
    {
        public static IGzServiceProvider GzServiceProvider { get; private set; }
        public static bool IsBuilt { get; private set; }

        private GzServiceCollection ServiceCollection { get; set; }
        private static bool IsInitialized { get; set; }

        public void AddSingleton<TService, TImplementation>()
            where TImplementation : class, TService
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            ServiceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddTransient<TService, TImplementation>()
            where TImplementation : class, TService
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            ServiceCollection.AddTransient<TService, TImplementation>();
        }

        public void Build()
        {
            if (IsBuilt)
            {
                Debug.LogWarning("Service provider is already built, you are probably doing something wrong");
            }
            GzServiceProvider = ServiceCollection.BuildServiceProvider();
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
            if (IsInitialized)
            {
                Debug.LogWarning("Service provider is already initialized, you are probably doing something wrong");
            }
            ServiceCollection = new GzServiceCollection();
            IsInitialized = true;
        }
    }
}
