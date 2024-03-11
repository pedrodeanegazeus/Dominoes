using System;
using Dominoes.Animations;
using Dominoes.Controllers.HUDs;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Extensions;
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
        [SerializeField] private SettingsPlayerController _team1Player1;
        [SerializeField] private SettingsPlayerController _team1Player2;
        [SerializeField] private SettingsPlayerController _team2Player1;
        [SerializeField] private SettingsPlayerController _team2Player2;
        [SerializeField] private Button _leaveButton;

        private IGameplayService _gameplayService;
        private IGzLogger<SettingsMenuView> _logger;
        private IProfileService _profileService;

        public void Initialize(IGameplayService gameplayService)
        {
            _gameplayService = gameplayService;
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<SettingsMenuView>>();
            _profileService = GameManager.ServiceProvider.GetRequiredService<IProfileService>();
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

        private void Start()
        {
            switch (_gameState.NumberPlayers)
            {
                case NumberPlayers.Two:
                    _team1Player2.gameObject.SetActive(false);
                    _team2Player2.gameObject.SetActive(false);
                    _team2Player1.SetPosition(TablePosition.Top);
                    break;
                case NumberPlayers.Four:
                    _team1Player2.SetPosition(TablePosition.Top);
                    _team2Player1.SetPosition(TablePosition.Left);
                    break;
            }
        }
        #endregion

        private void Close()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }

        private void LeaveGame()
        {
            GameManager.Scene.LoadScene(DominoesScene.Lobby);
        }
    }
}
