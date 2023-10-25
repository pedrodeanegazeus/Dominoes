using Dominoes.Core.Interfaces.Services;
using Dominoes.Services;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes
{
    internal static class ServiceProvider
    {
        private static readonly IGzServiceProvider _serviceProvider;

        static ServiceProvider()
        {
            GzServiceCollection serviceCollection = new();
            serviceCollection.AddSingleton<IMultiplayerService, MultiplayerService>();
            serviceCollection.AddSingleton<IProfileService, ProfileService>();
            serviceCollection.AddSingleton<IVipService, VipService>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static TService GetRequiredService<TService>()
            where TService : class
        {
            TService service = _serviceProvider.GetRequiredService<TService>();
            return service;
        }
    }
}
