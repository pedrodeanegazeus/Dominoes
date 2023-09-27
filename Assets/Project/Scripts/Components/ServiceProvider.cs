using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine.EventSystems;

namespace Dominoes.Components
{
    public class ServiceProvider : UIBehaviour
    {
        public IGzServiceProvider GzServiceProvider { get; private set; }

        private GzServiceCollection ServiceCollection { get; set; }

        public void AddSingleton<TService, TImplementation>()
            where TImplementation : class, TService
        {
            ServiceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddTransient<TService, TImplementation>()
            where TImplementation : class, TService
        {
            ServiceCollection.AddTransient<TService, TImplementation>();
        }

        public void Build()
        {
            GzServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public TService GetRequiredService<TService>()
            where TService : class
        {
            TService service = GzServiceProvider.GetRequiredService<TService>();
            return service;
        }

        public void Initialize()
        {
            ServiceCollection = new GzServiceCollection();
        }
    }
}
