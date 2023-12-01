using System;
using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Core.Services.Gameplay
{
    internal class MultiplayerGameplayService : IGameplayService
    {
        public event Action ChatReceived;

        private readonly IGzLogger<MultiplayerGameplayService> _logger;

        public MultiplayerGameplayService(IGzLogger<MultiplayerGameplayService> logger)
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
