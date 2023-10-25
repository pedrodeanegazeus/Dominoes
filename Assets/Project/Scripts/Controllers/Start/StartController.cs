using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private LogConfiguration _logConfiguration;

        private IMultiplayerService _multiplayerService = ServiceProvider.GetRequiredService<IMultiplayerService>();
        private IProfileService _profileService = ServiceProvider.GetRequiredService<IProfileService>();
        private IVipService _vipService = ServiceProvider.GetRequiredService<IVipService>();
        private List<Task> _loadingTasks;
        private TaskCompletionSource<bool> _loadingAnimationTask;

        #region Unity
        private void Awake()
        {
            GzLogger.Initialize(_logConfiguration);

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
            _loadingTasks.Add(_multiplayerService.InitializeAsync());
            _loadingTasks.Add(_profileService.InitializeAsync());
            _loadingTasks.Add(_vipService.InitializeAsync());
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
