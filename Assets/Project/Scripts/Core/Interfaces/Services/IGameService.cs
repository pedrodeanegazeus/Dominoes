using System.Threading.Tasks;
using Dominoes.Core.Models.Services.MultiplayerService;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameService
    {
        Task<PlayersOnline> GetPlayersOnlineAsync();
    }
}
