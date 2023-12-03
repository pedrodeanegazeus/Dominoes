using System;
using System.Threading.Tasks;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameplayService
    {
        event Action ChatReceived;
        event Action<int> CornerPointsChanged;
        event Action<int> StockTilesChanged;

        Task InitializeAsync();
    }
}
