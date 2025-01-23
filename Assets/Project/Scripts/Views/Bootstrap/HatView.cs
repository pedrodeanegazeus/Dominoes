using System;
using DG.Tweening;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Views.Bootstrap
{
    public class HatView : MonoBehaviour
    {
        public event Action AnimationCompleted;

        [SerializeField] private RectTransform _hatTransform;
        [SerializeField] private float _startYPosition;
        [SerializeField] private float _elasticYDelta;

        [Space]
        [SerializeField] private float _fallDuration;
        [SerializeField] private float _elasticDuration;

        public void Animate()
        {
            Vector2 endPosition = _hatTransform.localPosition;
            Vector2 elasticPosition = new(endPosition.x, endPosition.y - _elasticYDelta);

            _hatTransform.localPosition = new(endPosition.x, _startYPosition);

            _ = DOTween.Sequence()
                .Append(_hatTransform.DOLocalMove(endPosition, _fallDuration).SetEase(Ease.Linear))
                .Append(_hatTransform.DOLocalMove(elasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_hatTransform.DOLocalMove(endPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => AnimationCompleted?.Invoke());
        }
    }
}
