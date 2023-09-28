using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine.EventSystems;

namespace Dominoes.Components
{
    public class ServiceProvider : UIBehaviour
    {
        private GzServiceCollection _serviceCollection;

        public IGzServiceProvider GzServiceProvider { get; private set; }

        public void AddSingleton<TService, TImplementation>()
            where TImplementation : class, TService
        {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public void AddTransient<TService, TImplementation>()
            where TImplementation : class, TService
        {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public void Build()
        {
            GzServiceProvider = _serviceCollection.BuildServiceProvider();
        }

        public TService GetRequiredService<TService>()
            where TService : class
        {
            TService service = GzServiceProvider.GetRequiredService<TService>();
            return service;
        }

        public void Initialize()
        {
            _serviceCollection = new GzServiceCollection();
        }
    }
}
