using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Gameplay;
using UnityEngine;
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
        [SerializeField] private SettingsMenuCanvasView _settingsMenuCanvasView;

        private IGameplayService _gameplayService;
        private ConcurrentQueue<Action> _actions;

        #region Unity
        private void Awake()
        {
            _actions = new ConcurrentQueue<Action>();

            _settingsMenuCanvasView.Initialize();

            _chatButton.onClick.AddListener(OpenChat);
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        private void OnDestroy()
        {
            _chatButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
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
            StartCoroutine(initializeTask.WaitForTaskCompleteRoutine());

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
        #endregion

        private void OpenChat()
        {
            _chatAlert.SetActive(false);
        }

        private void OpenSettingsMenu()
        {
            _settingsMenuCanvasView.gameObject.SetActive(true);
            _settingsMenuCanvasView.Open();
        }
    }
}
