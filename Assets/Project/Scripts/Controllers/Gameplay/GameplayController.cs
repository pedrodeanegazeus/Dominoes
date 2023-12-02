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
        [SerializeField] private TextMeshProUGUI _usText;
        [SerializeField] private TextMeshProUGUI _themText;

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
            AsyncOperationHandle<string> usScore = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Gameplay Strings", "us-score");
            AsyncOperationHandle<string> themScore = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Gameplay Strings", "them-score");
            _ = StartCoroutine(usScore.Task.WaitForTaskCompleteRoutine(usScore => _usText.text = usScore));
            _ = StartCoroutine(usScore.Task.WaitForTaskCompleteRoutine(themScore => _themText.text = themScore));
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
