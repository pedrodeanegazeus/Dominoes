using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine.UI;

namespace Dominoes.Core.Services.Gameplay
{
    internal class SinglePlayerGameplayService : IGameplayService
    {
        public event Action<int> CornerPointsChanged;
        public event Action<int> StockTilesChanged;

        private readonly IGzLogger<SinglePlayerGameplayService> _logger;

        public SinglePlayerGameplayService(IGzLogger<SinglePlayerGameplayService> logger)
        {
            _logger = logger;
        }

        public Image GetAvatar(TablePosition position)
        {
            return null;
        }
    }
}
