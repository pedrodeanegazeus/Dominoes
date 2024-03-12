using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GameService;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.CoreMobile.SDK.Core.Models.PlayersOnline;

namespace Dominoes.Core.Services
{
    internal class GameService : IGameService
    {
        private readonly IGazeusSDK _gazeusSDK;
        private readonly IGzLogger<GameService> _logger;

        public GameService(IGazeusSDK gazeusSDK,
                           IGzLogger<GameService> logger)
        {
            _logger = logger;
            _gazeusSDK = gazeusSDK;
        }

        public async Task<PlayersOnline> GetPlayersOnlineAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(GetPlayersOnlineAsync));

            Task<TotalPlayersResult> allFivesOnlineTask = _gazeusSDK.PlayersOnlineService.GetTotalPlayersAsync("all_fives");
            Task<TotalPlayersResult> blockOnlineTask = _gazeusSDK.PlayersOnlineService.GetTotalPlayersAsync("block");
            Task<TotalPlayersResult> drawOnlineTask = _gazeusSDK.PlayersOnlineService.GetTotalPlayersAsync("draw");
            Task<TotalPlayersResult> turboOnlineTask = _gazeusSDK.PlayersOnlineService.GetTotalPlayersAsync("turbo");

            await Task.WhenAll(allFivesOnlineTask,
                               blockOnlineTask,
                               drawOnlineTask,
                               turboOnlineTask);

            PlayersOnline playersOnline = new()
            {
                AllFives = allFivesOnlineTask.Result.Response,
                Block = blockOnlineTask.Result.Response,
                Draw = drawOnlineTask.Result.Response,
                Turbo = turboOnlineTask.Result.Response,
            };
            return playersOnline;
        }
    }
}
