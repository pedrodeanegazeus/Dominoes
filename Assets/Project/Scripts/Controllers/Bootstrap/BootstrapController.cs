#pragma warning disable UNT0006 // Incorrect message signature

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core;
using Gazeus.CoreMobile.SDK.Core.ScriptableObjects;
using Gazeus.Mobile.Domino.Core.Enum;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Bootstrap;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Controllers.Bootstrap
{
    public class BootstrapController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private HatView _hatView;
        [SerializeField] private LogoView _logoView;
        [SerializeField] private ShadowsView _shadowsView;
        [SerializeField] private LoadingView _loadingView;

        [Header("Managers")]
        [SerializeField] private GameManager _gameManager;

        [Header("Scriptable Objects")]
        [SerializeField] private LogConfiguration _logConfiguration;

        private TaskCompletionSource<bool> _hatViewAnimationTask;
        private TaskCompletionSource<bool> _logoViewAnimationTask;
        private TaskCompletionSource<bool> _shadowsViewAnimationTask;

        #region Unity
        private void Awake()
        {
            _hatViewAnimationTask = new TaskCompletionSource<bool>();
            _logoViewAnimationTask = new TaskCompletionSource<bool>();
            _shadowsViewAnimationTask = new TaskCompletionSource<bool>();

            _ = DOTween.Init();
        }

        private void OnDisable()
        {
            _hatView.AnimationCompleted -= HatView_AnimationCompleted;
            _logoView.AnimationCompleted -= LogoView_AnimationCompleted;
            _shadowsView.AnimationCompleted -= ShadowsView_AnimationCompleted;
        }

        private void OnEnable()
        {
            _hatView.AnimationCompleted += HatView_AnimationCompleted;
            _logoView.AnimationCompleted += LogoView_AnimationCompleted;
            _shadowsView.AnimationCompleted += ShadowsView_AnimationCompleted;
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity Start method")]
        private async Awaitable Start()
        {
            _hatView.gameObject.SetActive(true);
            _logoView.gameObject.SetActive(true);
            _shadowsView.gameObject.SetActive(true);
            _loadingView.gameObject.SetActive(true);

            StartAnimations();

            GzLogger.Initialize(_logConfiguration);

            _gameManager.Initialize();

            await Task.WhenAll(_hatViewAnimationTask.Task,
                               _logoViewAnimationTask.Task,
                               _shadowsViewAnimationTask.Task);

            GameManager.GameSceneManager.LoadScene(GameScene.Lobby, false);
        }
        #endregion

        #region Events
        private void HatView_AnimationCompleted()
        {
            _hatViewAnimationTask.SetResult(true);
        }

        private void LogoView_AnimationCompleted()
        {
            _logoViewAnimationTask?.SetResult(true);
        }

        private void ShadowsView_AnimationCompleted()
        {
            _shadowsViewAnimationTask.SetResult(true);
        }
        #endregion

        private void StartAnimations()
        {
            _hatView.Animate();
            _logoView.Animate();
            _shadowsView.Animate();
            _loadingView.Animate();
        }
    }
}
