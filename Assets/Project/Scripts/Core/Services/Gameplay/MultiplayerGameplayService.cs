using System;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Core.Services.Gameplay
{
    internal class MultiplayerGameplayService : IGameplayService
    {
        public event Action<int> CornerPointsChanged;
        public event Action<int> StockTilesChanged;

        private readonly IGzLogger<MultiplayerGameplayService> _logger;

        public MultiplayerGameplayService(IGzLogger<MultiplayerGameplayService> logger)
        {
            _logger = logger;
        }
    }
}
