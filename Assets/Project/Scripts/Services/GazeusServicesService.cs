using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GazeusServicesService;
using UnityEngine;

namespace Dominoes.Services
{
    internal class GazeusServicesService : IGazeusServicesService
    {
        public async Task<PlayersOnline> GetPlayersOnlineAsync()
        {
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
            return Task.CompletedTask;
        }
    }
}
