using Dominoes.Core.Models;

namespace Dominoes.Core.Interfaces.Repositories
{
    internal interface IGameConfigRepository
    {
        GameConfig GameConfig { get; set; }

        void Sync();
    }
}
