using System;
using Dominoes.Core.Enums;
using UnityEngine.UI;

namespace Dominoes.Core.Interfaces.Services
{
    internal interface IGameplayService
    {
        event Action<int> CornerPointsChanged;
        event Action<int> StockTilesChanged;

        Image GetAvatar(TablePosition position);
    }
}
