using System;
using System.Collections.Concurrent;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Gameplay;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class GameplayController : MonoBehaviour
    {
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
        private ConcurrentQueue<Action> _actions;

        #region Unity
        private void Awake()
        {
            _actions = new ConcurrentQueue<Action>();
            _chatService = ServiceProviderManager.Instance.GetRequiredService<IChatService>();

            IGameplayService gameplayService = _gameState.GameType switch
            {
                GameType.Multiplayer or GameType.PlayWithFriends => ServiceProviderManager.Instance.GetRequiredKeyedService<IGameplayService>(GameType.Multiplayer),
                GameType.SinglePlayer => ServiceProviderManager.Instance.GetRequiredKeyedService<IGameplayService>(GameType.SinglePlayer),
                _ => throw new NotImplementedException($"Game type {_gameState.GameType} not implemented"),
            };

            _gameplayView.Initialize(gameplayService);
            _settingsMenuView.Initialize(gameplayService);

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);

            _chatService.ChatReceived += ChatService_ChatReceived;
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();

            _chatService.ChatReceived -= ChatService_ChatReceived;
        }

        private void OnEnable()
        {
            string key = _gameState.NumberPlayers switch
            {
                NumberPlayers.Two => "score-title-2",
                NumberPlayers.Four => "score-title-4",
                _ => throw new NotImplementedException($"Number of players {_gameState.NumberPlayers} not implemented"),
            };
            string type = _gameState.GameType switch
            {
                GameType.Multiplayer or GameType.PlayWithFriends => "mp",
                GameType.SinglePlayer => "sp",
                _ => throw new NotImplementedException($"Game type {_gameState.GameType} not implemented"),
            };
            _usScoreTitle.SetEntry($"us-{key}");
            _themScoreTitle.SetEntry($"them-{key}-{type}");
        }

        private void Start()
        {
            switch (_gameState.GameType)
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

        private void Update()
        {
            if (!_actions.IsEmpty && _actions.TryDequeue(out Action action))
            {
                action.Invoke();
            }
        }
        #endregion

        #region Events
        private void ChatService_ChatReceived()
        {
            _actions.Enqueue(() => _chatAlert.SetActive(true));
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
