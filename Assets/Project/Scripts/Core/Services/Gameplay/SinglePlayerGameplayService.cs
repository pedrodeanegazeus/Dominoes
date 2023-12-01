using System;
using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Core.Services.Gameplay
{
    internal class SinglePlayerGameplayService : IGameplayService
    {
#pragma warning disable CS0067
        public event Action ChatReceived;
#pragma warning restore CS0067

        private readonly IGzLogger<SinglePlayerGameplayService> _logger;

        public SinglePlayerGameplayService(IGzLogger<SinglePlayerGameplayService> logger)
        {
            _logger = logger;
        }

        public Task InitializeAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(InitializeAsync));

            return Task.CompletedTask;
        }
    }
}
