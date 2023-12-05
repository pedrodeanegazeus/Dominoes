using System;
using Dominoes.Animations;
using Dominoes.Core.Enums;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class NumberPlayersView : MonoBehaviour
    {
        public event Action NumberPlayersSelected;

        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private LocalizeStringEvent _ribbonTitle;
        [SerializeField] private Button _2PlayersButton;
        [SerializeField] private Button _4PlayersButton;
        [SerializeField] private LocalizeStringEvent _infoPanel;

        private IGzLogger<NumberPlayersView> _logger;

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<NumberPlayersView>>();
        }

        public void Open()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Open));

            _slideAnimation.SlideIn();
        }

        #region Unity
        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _2PlayersButton.onClick.AddListener(() => SetNumberPlayers(2));
            _4PlayersButton.onClick.AddListener(() => SetNumberPlayers(4));
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _2PlayersButton.onClick.RemoveAllListeners();
            _4PlayersButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            string key = _gameState.GameMode switch
            {
                GameMode.Draw => "draw-game",
                GameMode.Block => "block-game",
                GameMode.AllFives => "all-fives-game",
                GameMode.Turbo => "turbo-game",
                _ => throw new NotImplementedException($"Game mode {_gameState.GameMode} not implemented"),
            };
            _infoPanel.SetEntry(key);
            _ribbonTitle.SetEntry(key);
        }
        #endregion

        private void Close()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }

        private void SetNumberPlayers(int numberPlayers)
        {
            _gameState.NumberPlayers = (NumberPlayers)numberPlayers;
            NumberPlayersSelected?.Invoke();
        }
    }
}
