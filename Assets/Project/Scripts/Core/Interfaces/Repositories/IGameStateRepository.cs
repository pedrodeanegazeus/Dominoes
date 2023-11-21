using Dominoes.Core.Models;

namespace Dominoes.Core.Interfaces.Repositories
{
    internal interface IGameStateRepository
    {
        GameConfig LoadGameConfig();
        void SaveGameConfig(GameConfig gameConfig);
    }
}
