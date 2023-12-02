using Dominoes.Core.Interfaces.Services;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Views.Gameplay
{
    internal class TableTilesView : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        private IGameplayService _gameplayService;
        private IGzLogger<TableTilesView> _logger;

        public void Initialize(IGameplayService gameplayService)
        {
            _gameplayService = gameplayService;
            _logger = ServiceProvider.GetRequiredService<IGzLogger<TableTilesView>>();
        }
    }
}
