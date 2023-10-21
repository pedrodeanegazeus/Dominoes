using System;
using DG.Tweening;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class SlideAnimation : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right,
        }

        [SerializeField] private Direction _from;
        [SerializeField] private Ease _fromEase;

        [Space]
        [SerializeField] private Direction _to;
        [SerializeField] private Ease _toEase;

        [Space]
        [SerializeField] private float _duration;

        private readonly IGzLogger<SlideAnimation> _logger = ServiceProvider.GetRequiredService<IGzLogger<SlideAnimation>>();

        private RectTransform _rectTransform;
        private Vector3 _initialPosition;
        private Vector3 _outOfScreenIn;
        private Vector3 _outOfScreenOut;

        public void SlideIn(TweenCallback callback = null)
        {
            _logger.Debug("CALLED: {method}",
                          nameof(SlideIn));

            _rectTransform.position = _initialPosition + _outOfScreenIn;
            _ = _rectTransform
                .DOMove(_initialPosition, _duration)
                .SetEase(_fromEase)
                .OnComplete(callback);
        }

        public void SlideOut(TweenCallback callback = null)
        {
            _logger.Debug("CALLED: {method}",
                          nameof(SlideOut));

            _ = _rectTransform
                .DOMove(_initialPosition + _outOfScreenOut, _duration)
                .SetEase(_toEase)
                .OnComplete(callback);
        }

        #region Unity
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.position;

            _outOfScreenIn = _from switch
            {
                Direction.Left => Vector3.left * Screen.width,
                Direction.Right => Vector3.right * Screen.width,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
            _outOfScreenOut = _to switch
            {
                Direction.Left => Vector3.left * Screen.width,
                Direction.Right => Vector3.right * Screen.width,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
        }
        #endregion
    }
}
