using System.Threading.Tasks;
using Dominoes.Core.Models.Services.GameService;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameService
    {
        Task<PlayersOnline> GetPlayersOnlineAsync();
    }
}
