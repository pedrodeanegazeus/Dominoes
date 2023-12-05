using System;
using Dominoes.Animations;
using Dominoes.Core.Enums;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Views.Gameplay
{
    internal class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private LocalizeStringEvent _gameModeText;
        [SerializeField] private Button _leaveButton;

        private IGzLogger<SettingsMenuView> _logger;

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<SettingsMenuView>>();
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
            _leaveButton.onClick.AddListener(LeaveGame);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _leaveButton.onClick.RemoveAllListeners();
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
            _gameModeText.SetEntry(key);
        }
        #endregion

        private void Close()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }

        private void LeaveGame()
        {
            GameSceneManager.Instance.LoadScene(DominoesScene.Lobby);
        }
    }
}
