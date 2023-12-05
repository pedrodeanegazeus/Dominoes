using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine.UI;

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

        public Image GetAvatar(TablePosition position)
        {
            return null;
        }
    }
}
