#pragma warning disable UNT0006 // Incorrect message signature

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DG.Tweening;
using Gazeus.Mobile.Domino.Views;
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

        private TaskCompletionSource<bool> _hatViewAnimationTask;
        private TaskCompletionSource<bool> _logoViewAnimationTask;
        private TaskCompletionSource<bool> _shadowsViewAnimationTask;

        #region Unity
        private void Awake()
        {
            _hatView.AnimationCompleted += HatView_AnimationCompleted;
            _shadowsView.AnimationCompleted += ShadowsView_AnimationCompleted;

            _hatViewAnimationTask = new TaskCompletionSource<bool>();
            _logoViewAnimationTask = new TaskCompletionSource<bool>();
            _shadowsViewAnimationTask = new TaskCompletionSource<bool>();

            _ = DOTween.Init();
        }

        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity Start method")]
        private async Awaitable Start()
        {
            _hatView.Animate();
            _logoView.Animate();
            _shadowsView.Animate();
            _loadingView.Animate();

            await Task.WhenAll(_hatViewAnimationTask.Task,
                               _logoViewAnimationTask.Task,
                               _shadowsViewAnimationTask.Task);

            Debug.Log("FINISHED!");
        }
        #endregion

        #region Unity
        private void HatView_AnimationCompleted()
        {
            _hatViewAnimationTask.SetResult(true);
        }

        private void ShadowsView_AnimationCompleted()
        {
            _shadowsViewAnimationTask.SetResult(true);
        }
        #endregion
    }
}
