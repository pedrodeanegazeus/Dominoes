using System.Threading.Tasks;
using Dominoes.Core.Models.Services.GazeusServicesService;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGazeusServicesService
    {
        Task<PlayersOnline> GetPlayersOnlineAsync();
        Task InitializeAsync();
    }
}