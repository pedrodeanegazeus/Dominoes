using DG.Tweening;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Views.Start
{
    internal class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image _loading;
        [SerializeField] private RectTransform _loadingRect;

        private IGzLogger<LoadingView> _logger;

        public void Initialize()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<LoadingView>>();
        }

        public void Animate()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Animate));

            // loading
            _loading.fillAmount = 0;
            _loading.fillClockwise = true;
            _ = DOTween.Sequence()
                .Append(_loading.DOFillAmount(1f, 1f))
                .AppendCallback(() => _loading.fillClockwise = false)
                .Append(_loading.DOFillAmount(0f, 1f))
                .AppendCallback(() => _loading.fillClockwise = true)
                .SetLoops(-1);

            // loadingRect
            _ = DOTween.Sequence()
                .Append(_loadingRect.DORotate(new Vector3(0f, 0f, -1440f), 6f, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .SetLoops(-1);
        }
    }
}
