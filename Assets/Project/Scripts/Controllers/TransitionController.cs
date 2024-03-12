using System;
using System.Linq;
using DG.Tweening;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class TransitionController : MonoBehaviour
    {
        public event Action Completed;

        [SerializeField] private Image _transitionTilesAlpha;
        [SerializeField] private Image _transitionTilesTopAlpha;
        [SerializeField] private RectTransform _transitionTilesTransform;

        private IGzLogger<TransitionController> _logger;

        public void FinishTransition()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(FinishTransition));

            Color transparent = new(1f, 1f, 1f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(_transitionTilesAlpha.DOColor(transparent, 0.7f));
            _ = DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(_transitionTilesTopAlpha.DOColor(transparent, 0.7f));
            _ = DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.96f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.80f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.64f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.48f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.32f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.16f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0f), 0f));
        }

        public void Initialize(IGzServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<IGzLogger<TransitionController>>();
        }

        public void StartTransition()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(StartTransition));

            _ = _transitionTilesAlpha.DOColor(Color.white, 0.7f);
            _ = _transitionTilesTopAlpha.DOColor(Color.white, 0.7f);
            _ = DOTween.Sequence()
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.16f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.32f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.48f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.64f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.80f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.96f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 1f), 0f))
                .OnComplete(() => Completed?.Invoke());
        }
    }
}
