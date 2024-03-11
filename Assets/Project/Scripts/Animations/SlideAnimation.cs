using System;
using DG.Tweening;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class SlideAnimation : MonoBehaviour
    {
        public enum Direction
        {
            Bottom,
            Left,
            Right,
            Top,
        }

        [SerializeField] private Direction _from;
        [SerializeField] private Ease _fromEase;

        [Space]
        [SerializeField] private Direction _to;
        [SerializeField] private Ease _toEase;

        [Space]
        [SerializeField] private float _duration;

        private IGzLogger<SlideAnimation> _logger;
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
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<SlideAnimation>>();
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.position;

            _outOfScreenIn = _from switch
            {
                Direction.Bottom => Vector3.down * Screen.height,
                Direction.Left => Vector3.left * Screen.width,
                Direction.Right => Vector3.right * Screen.width,
                Direction.Top => Vector3.up * Screen.height,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
            _outOfScreenOut = _to switch
            {
                Direction.Bottom => Vector3.down * Screen.height,
                Direction.Left => Vector3.left * Screen.width,
                Direction.Right => Vector3.right * Screen.width,
                Direction.Top => Vector3.up * Screen.height,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
        }
        #endregion
    }
}
