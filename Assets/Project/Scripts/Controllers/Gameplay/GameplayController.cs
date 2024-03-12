using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Gameplay;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class GameplayController : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] private GameState _gameState;

        [Header("Header")]
        [SerializeField] private Button _chatButton;
        [SerializeField] private GameObject _chatAlert;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _settingsButton;

        [Header("Canvas views")]
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private SettingsMenuView _settingsMenuView;

        [Header("Texts")]
        [SerializeField] private LocalizeStringEvent _usScoreTitle;
        [SerializeField] private LocalizeStringEvent _themScoreTitle;

        private IChatService _chatService;
        private GameMode _gameMode;
        private GameType _gameType;
        private NumberPlayers _numberPlayers;

        #region Unity
        private void Awake()
        {
            (_gameType, _gameMode, _numberPlayers) = GameManager.Scene.GetParameter<(GameType, GameMode, NumberPlayers)>();

            _chatService = GameManager.ServiceProvider.GetRequiredService<IChatService>();
            _chatService.ChatReceived += ChatService_ChatReceived;

            _gameplayView.Initialize(_gameType, _gameMode);
            _settingsMenuView.Initialize(_gameMode, _numberPlayers);

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();

            _chatService.ChatReceived -= ChatService_ChatReceived;
        }

        private void OnEnable()
        {
            string key = _numberPlayers switch
            {
                NumberPlayers.Two => "score-title-2",
                NumberPlayers.Four => "score-title-4",
                _ => throw new NotImplementedException($"Number of players {_numberPlayers} not implemented"),
            };
            string type = _gameType switch
            {
                GameType.Multiplayer or GameType.PlayWithFriends => "mp",
                GameType.SinglePlayer => "sp",
                _ => throw new NotImplementedException($"Game type {_gameType} not implemented"),
            };
            _usScoreTitle.SetEntry($"us-{key}");
            _themScoreTitle.SetEntry($"them-{key}-{type}");
        }

        private void Start()
        {
            switch (_gameType)
            {
                case GameType.Multiplayer:
                case GameType.PlayWithFriends:
                    _chatButton.gameObject.SetActive(true);
                    break;
                case GameType.SinglePlayer:
                    _infoButton.gameObject.SetActive(true);
                    break;
            }
        }
        #endregion

        #region Events
        private void ChatService_ChatReceived()
        {
            _chatAlert.SetActive(true);
        }
        #endregion

        private void OpenChat()
        {
            _chatAlert.SetActive(false);
        }

        private void OpenSettingsMenu()
        {
            _settingsMenuView.gameObject.SetActive(true);
            _settingsMenuView.Open();
        }
    }
}
