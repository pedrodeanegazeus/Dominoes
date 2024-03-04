using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.ScriptableObjects;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameSceneManager _gameSceneManager;
        [SerializeField] private LogConfiguration _logConfiguration;
        [SerializeField] private ServiceProviderManager _serviceProviderManager;

        private TaskCompletionSource<bool> _loadingAnimationTask;

        #region Unity
        private void Awake()
        {
            _loadingAnimationTask = new TaskCompletionSource<bool>();

            _animationController.EventFired += AnimationController_EventFired;
        }

        private void OnDestroy()
        {
            _animationController.EventFired -= AnimationController_EventFired;
        }

        private void Start()
        {
            _ = StartCoroutine(StartGame());
        }
        #endregion

        #region Events
        private void AnimationController_EventFired(string @event)
        {
            _loadingAnimationTask.SetResult(true);
        }
        #endregion

        private IEnumerator StartGame()
        {
            // statics
            GzLogger.Initialize(_logConfiguration);
            DOTween.Init();

            // singleton managers
            _serviceProviderManager.Initialize();
            _audioManager.Initialize();
            _gameSceneManager.Initialize();

            _animationController.Initialize();
            _gameState.Initialize();
            _gameState.Load();

            // singleton services
            IProfileService profileService = ServiceProviderManager.Instance.GetRequiredService<IProfileService>();
            IVipService vipService = ServiceProviderManager.Instance.GetRequiredService<IVipService>();
            Task profileServiceInitializeTask = Task.Run(() => profileService.Initialize());
            Task vipServiceInitializeTask = Task.Run(() => vipService.Initialize());

            yield return Task
                .WhenAll(_loadingAnimationTask.Task,
                         profileServiceInitializeTask,
                         vipServiceInitializeTask)
                .WaitTask();

            GameSceneManager.Instance.LoadScene(DominoesScene.Lobby, false);
        }
    }
}
