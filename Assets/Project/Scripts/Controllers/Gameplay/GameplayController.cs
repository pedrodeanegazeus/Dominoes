using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
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
        [SerializeField] private TableTilesView _tableTilesView;
        [SerializeField] private SettingsMenuView _settingsMenuView;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _usScoreTitle;
        [SerializeField] private TextMeshProUGUI _themScoreTitle;

        private IGameplayService _gameplayService;
        private ConcurrentQueue<Action> _actions;

        #region Unity
        private void Awake()
        {
            _actions = new ConcurrentQueue<Action>();

            _settingsMenuView.Initialize();

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);

            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();

            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            SetScoreTitle();
        }

        private void Start()
        {
            switch (_gameState.GameType)
            {
                case GameType.Multiplayer:
                case GameType.PlayWithFriends:
                    _chatButton.gameObject.SetActive(true);
                    _gameplayService = ServiceProvider.GetRequiredKeyedService<IGameplayService>(nameof(GameType.Multiplayer));
                    break;
                case GameType.SinglePlayer:
                    _infoButton.gameObject.SetActive(true);
                    _gameplayService = ServiceProvider.GetRequiredKeyedService<IGameplayService>(nameof(GameType.SinglePlayer));
                    break;
            }
            Task initializeTask = _gameplayService.InitializeAsync();
            StartCoroutine(initializeTask.WaitForTaskCompleteRoutine(() => _tableTilesView.Initialize(_gameplayService)));

            _gameplayService.ChatReceived += GameplayService_ChatReceived;
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
        private void GameplayService_ChatReceived()
        {
            _actions.Enqueue(() => _chatAlert.SetActive(true));
        }

        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            SetScoreTitle();
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

        private void SetScoreTitle()
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
