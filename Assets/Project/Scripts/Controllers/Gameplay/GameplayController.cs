using System;
using System.Collections.Concurrent;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        [SerializeField] private TextMeshProUGUI _usScoreTitle;
        [SerializeField] private TextMeshProUGUI _themScoreTitle;

        private IChatService _chatService;
        private ConcurrentQueue<Action> _actions;

        #region Unity
        private void Awake()
        {
            _actions = new ConcurrentQueue<Action>();
            _chatService = ServiceProvider.GetRequiredService<IChatService>();

            _gameplayView.Initialize();
            _settingsMenuView.Initialize();

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);

            _chatService.ChatReceived += ChatService_ChatReceived;
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();

            _chatService.ChatReceived -= ChatService_ChatReceived;
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            SetLocalizedTexts();
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

        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            SetLocalizedTexts();
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

        private void SetLocalizedTexts()
        {
            string key = _gameState.NumberPlayers switch
            {
                NumberPlayers.Two => "score-title-2",
                NumberPlayers.Four => "score-title-4",
                _ => throw new NotImplementedException("Number of players not implemented"),
            };
            string type = _gameState.GameType switch
            {
                GameType.Multiplayer or GameType.PlayWithFriends => "mp",
                GameType.SinglePlayer => "sp",
                _ => throw new NotImplementedException("Game type not implemented"),
            };

            AsyncOperationHandle<string> usScoreTitleTask = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Gameplay View Strings", $"us-{key}");
            _usScoreTitle.text = usScoreTitleTask.WaitForCompletion();

            AsyncOperationHandle<string> themScoreTitleTask = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Gameplay View Strings", $"them-{key}-{type}");
            _themScoreTitle.text = themScoreTitleTask.WaitForCompletion();
        }
    }
}
