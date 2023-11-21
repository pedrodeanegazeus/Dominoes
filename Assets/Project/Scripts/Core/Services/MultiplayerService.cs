using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GazeusServicesService;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Core.Services
{
    internal class MultiplayerService : IMultiplayerService
    {
        private readonly IGzLogger<MultiplayerService> _logger;

        public MultiplayerService(IGzLogger<MultiplayerService> logger)
        {
            _logger = logger;
        }

        public async Task<PlayersOnline> GetPlayersOnlineAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(GetPlayersOnlineAsync));

            await Task.Delay(Random.Range(0, 1000));
            PlayersOnline playersOnline = new()
            {
                AllFives = Random.Range(0, 1000),
                Block = Random.Range(0, 1000),
                Draw = Random.Range(0, 1000),
                Turbo = Random.Range(0, 1000),
            };
            return playersOnline;
        }

        public Task InitializeAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(InitializeAsync));

            return Task.CompletedTask;
        }
    }
}
