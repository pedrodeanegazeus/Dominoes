using System.Threading.Tasks;
using Dominoes.Core.Models.Services.GazeusServicesService;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IMultiplayerService
    {
        Task<PlayersOnline> GetPlayersOnlineAsync();
        Task InitializeAsync();
    }
}
