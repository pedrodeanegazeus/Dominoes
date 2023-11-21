using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private LogConfiguration _logConfiguration;

        private IMultiplayerService _multiplayerService;
        private IProfileService _profileService;
        private IVipService _vipService;
        private List<Task> _loadingTasks;
        private TaskCompletionSource<bool> _loadingAnimationTask;

        #region Unity
        private void Awake()
        {
            GzLogger.Initialize(_logConfiguration);

            _animationController.Initialize();
            _audioManager.Initialize();
            _gameState.Initialize();

            _multiplayerService = ServiceProvider.GetRequiredService<IMultiplayerService>();
            _profileService = ServiceProvider.GetRequiredService<IProfileService>();
            _vipService = ServiceProvider.GetRequiredService<IVipService>();

            _loadingAnimationTask = new TaskCompletionSource<bool>();
            _loadingTasks = new List<Task>();

            _animationController.EventFired += AnimationController_EventFired;
        }

        private void Start()
        {
            AddLoadingTasks();
            _ = StartCoroutine(FinishedLoadingRoutine());
        }
        #endregion

        private void AddLoadingTasks()
        {
            _loadingTasks.Add(_loadingAnimationTask.Task);
            _loadingTasks.Add(_gameState.LoadAsync());
            _loadingTasks.Add(_multiplayerService.InitializeAsync());
            _loadingTasks.Add(_profileService.InitializeAsync());
            _loadingTasks.Add(_vipService.InitializeAsync());

            _loadingTasks.Add(Task.Run(() => DOTween.Init()));
        }

        private void AnimationController_EventFired(string @event)
        {
            _loadingAnimationTask.SetResult(true);
        }

        private IEnumerator FinishedLoadingRoutine()
        {
            while (_loadingTasks.Any(loadingTask => !loadingTask.IsCompleted))
            {
                yield return null;
            }
            _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Lobby));
        }
    }
}
