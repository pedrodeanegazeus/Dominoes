using System;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameplayService
    {
        event Action<int> CornerPointsChanged;
        event Action<int> StockTilesChanged;
    }
}
