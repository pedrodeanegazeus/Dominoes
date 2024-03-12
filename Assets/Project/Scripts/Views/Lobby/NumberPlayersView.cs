using System;
using Dominoes.Animations;
using Dominoes.Core.Enums;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class NumberPlayersView : MonoBehaviour
    {
        public event Action<NumberPlayers> NumberPlayersSelected;

        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private LocalizeStringEvent _ribbonTitle;
        [SerializeField] private Button _2PlayersButton;
        [SerializeField] private Button _4PlayersButton;
        [SerializeField] private LocalizeStringEvent _infoPanel;

        private IGzLogger<NumberPlayersView> _logger;

        #region Unity
        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _2PlayersButton.onClick.AddListener(() => NumberPlayersSelected?.Invoke(NumberPlayers.Two));
            _4PlayersButton.onClick.AddListener(() => NumberPlayersSelected?.Invoke(NumberPlayers.Four));
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _2PlayersButton.onClick.RemoveAllListeners();
            _4PlayersButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void Initialize()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<NumberPlayersView>>();
        }

        public void Open(GameMode gameMode)
        {
            _logger.Debug("CALLED: {method} - {gameMode}",
                          nameof(Open),
                          gameMode);

            string key = gameMode switch
            {
                GameMode.Draw => "draw-game",
                GameMode.Block => "block-game",
                GameMode.AllFives => "all-fives-game",
                GameMode.Turbo => "turbo-game",
                _ => throw new NotImplementedException($"Game mode {gameMode} not implemented"),
            };

            _infoPanel.SetEntry(key);
            _ribbonTitle.SetEntry(key);

            _slideAnimation.SlideIn();
        }

        private void Close()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }
    }
}
