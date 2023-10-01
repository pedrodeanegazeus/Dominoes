using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominoes.Components;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService VipService { get; set; }
        private List<Task> LoadingTasks { get; set; }
        private TaskCompletionSource<bool> LoadingAnimationTask { get; set; }

        #region Unity
        private void Awake()
        {
            LoadingAnimationTask = new TaskCompletionSource<bool>();
            LoadingTasks = new List<Task>();

            _animationController.EventFired += AnimationController_EventFired;
        }

        private void Start()
        {
            InitializeServiceProvider();
            VipService = _serviceProvider.GetRequiredService<IVipService>();
            AddLoadingTasks();
            _ = StartCoroutine(FinishedLoadingRoutine());
        }
        #endregion

        private void AnimationController_EventFired(string @event)
        {
            switch (@event)
            {
                case "Jogatina_End":
                    LoadingAnimationTask.SetResult(true);
                    break;
            }
        }

        private IEnumerator FinishedLoadingRoutine()
        {
            while (LoadingTasks.Any(loadingTask => !loadingTask.IsCompleted))
            {
                yield return null;
            }
            _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Lobby));
        }

        private void InitializeServiceProvider()
        {
            _serviceProvider.Initialize();
            _serviceProvider.AddSingleton<IVipService, VipService>();
            _serviceProvider.Build();
        }

        private void AddLoadingTasks()
        {
            LoadingTasks.Add(LoadingAnimationTask.Task);
            LoadingTasks.Add(VipService.InitializeAsync());
        }
    }
}
