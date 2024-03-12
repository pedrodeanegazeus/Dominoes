using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GameService;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Core.Services
{
    internal class GameService : IGameService
    {
        private readonly IGzLogger<GameService> _logger;

        public GameService(IGzLogger<GameService> logger)
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
    }
}
