using System;
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

        private IProfileService _profileService;
        private IVipService _vipService;
        private Queue<Func<Task>> _asynchronousStartupTasks;
        private Queue<Func<Task>> _synchronousStartupTasks;
        private TaskCompletionSource<bool> _loadingAnimationTask;

        #region Unity
        private void Awake()
        {
            _asynchronousStartupTasks = new Queue<Func<Task>>();
            _synchronousStartupTasks = new Queue<Func<Task>>();
            _loadingAnimationTask = new TaskCompletionSource<bool>();

            _animationController.EventFired += AnimationController_EventFired;
        }

        private void OnDestroy()
        {
            _animationController.EventFired -= AnimationController_EventFired;
        }

        private void Start()
        {
            _synchronousStartupTasks.Enqueue(ActionToTaskFunc(GzLogger.Initialize, _logConfiguration));
            _synchronousStartupTasks.Enqueue(ActionToTaskFunc(_serviceProviderManager.Initialize));
            _synchronousStartupTasks.Enqueue(ActionToTaskFunc(Initialize));

            _asynchronousStartupTasks.Enqueue(() => _loadingAnimationTask.Task);
            _asynchronousStartupTasks.Enqueue(FuncToTaskFunc(DOTween.Init, (bool?)null, (bool?)null, (LogBehaviour?)null));
            _asynchronousStartupTasks.Enqueue(ActionToTaskFunc(_gameState.Load));
            _asynchronousStartupTasks.Enqueue(ActionToTaskFunc(_profileService.Initialize));
            _asynchronousStartupTasks.Enqueue(ActionToTaskFunc(_vipService.Initialize));

            _ = StartCoroutine(FinishedLoadingRoutine());
        }
        #endregion

        private Func<Task> ActionToTaskFunc(Action action)
        {
            action();
            return () => Task.CompletedTask;
        }

        private Func<Task> ActionToTaskFunc<TParam1>(Action<TParam1> action, TParam1 param1)
        {
            action(param1);
            return () => Task.CompletedTask;
        }

        private void AnimationController_EventFired(string @event)
        {
            _loadingAnimationTask.SetResult(true);
        }

        private IEnumerator FinishedLoadingRoutine()
        {
            while (_synchronousStartupTasks.Any())
            {
                Func<Task> func = _synchronousStartupTasks.Dequeue();
                Task task = func();
                yield return TaskWait(task);
            }
            List<Task> tasks = new();
            while (_asynchronousStartupTasks.Any())
            {
                Func<Task> func = _asynchronousStartupTasks.Dequeue();
                Task task = func();
                tasks.Add(task);
            }
            Task whenAllTask = Task.WhenAll(tasks);
            yield return TaskWait(whenAllTask);
            GameSceneManager.Instance.LoadScene(DominoesScene.Lobby, false);
        }

        private Func<Task> FuncToTaskFunc<TParam1, TParam2, TParam3, TResult>(Func<TParam1, TParam2, TParam3, TResult> func, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            _ = func(param1, param2, param3);
            return () => Task.CompletedTask;
        }

        private void Initialize()
        {
            _animationController.Initialize();
            _audioManager.Initialize();
            _gameSceneManager.Initialize();
            _gameState.Initialize();

            _profileService = ServiceProviderManager.Instance.GetRequiredService<IProfileService>();
            _vipService = ServiceProviderManager.Instance.GetRequiredService<IVipService>();
        }

        private IEnumerator TaskWait(Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
        }
    }
}
