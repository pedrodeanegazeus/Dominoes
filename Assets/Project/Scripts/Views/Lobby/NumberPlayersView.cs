using System;
using Dominoes.Animations;
using Dominoes.Core.Enums;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        [SerializeField] private TextMeshProUGUI _ribbonTitle;
        [SerializeField] private Button _2PlayersButton;
        [SerializeField] private Button _4PlayersButton;
        [SerializeField] private TextMeshProUGUI _infoPanel;

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

            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _2PlayersButton.onClick.RemoveAllListeners();
            _4PlayersButton.onClick.RemoveAllListeners();

            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            SetLocalizedTexts();
        }
        #endregion

        #region Events
        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            SetLocalizedTexts();
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

        private void SetLocalizedTexts()
        {
            string key = _gameState.GameMode switch
            {
                GameMode.Draw => "draw-game",
                GameMode.Block => "block-game",
                GameMode.AllFives => "all-fives-game",
                GameMode.Turbo => "turbo-game",
                _ => throw new NotImplementedException($"Game mode {_gameState.GameMode} not implemented"),
            };

            AsyncOperationHandle<string> infoPanelTask = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Number Players View Strings", key);
            infoPanelTask.WaitForCompletion();
            _infoPanel.text = infoPanelTask.Result;

            AsyncOperationHandle<string> ribbonTitleTask = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Game Type View Strings", key);
            ribbonTitleTask.WaitForCompletion();
            _ribbonTitle.text = ribbonTitleTask.Result;
        }
    }
}
