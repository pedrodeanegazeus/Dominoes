using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;

        private IGazeusServicesService _gazeusServicesService;
        private IVipService _vipService;
        private List<Task> _loadingTasks;
        private TaskCompletionSource<bool> _loadingAnimationTask;

        #region Unity
        private void Awake()
        {
            _loadingAnimationTask = new TaskCompletionSource<bool>();
            _loadingTasks = new List<Task>();

            _animationController.EventFired += AnimationController_EventFired;
        }

        private void Start()
        {
            _gazeusServicesService = ServiceProvider.GetRequiredService<IGazeusServicesService>();
            _vipService = ServiceProvider.GetRequiredService<IVipService>();
            AddLoadingTasks();
            _ = StartCoroutine(FinishedLoadingRoutine());
        }
        #endregion

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

        private void AddLoadingTasks()
        {
            _loadingTasks.Add(_loadingAnimationTask.Task);
            _loadingTasks.Add(_gazeusServicesService.InitializeAsync());
            _loadingTasks.Add(_vipService.InitializeAsync());
        }
    }
}
