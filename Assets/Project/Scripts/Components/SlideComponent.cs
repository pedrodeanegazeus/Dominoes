using System;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Components
{
    [RequireComponent(typeof(RectTransform))]
    public class SlideComponent : MonoBehaviour
    {
        [SerializeField] private Direction _from;
        [SerializeField] private Direction _to;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;

        public enum Direction
        {
            Bottom,
            Left,
            Right,
            Top,
        }

        public event Action SlideInCompleted;
        public event Action SlideOutCompleted;

        private IGzLogger<SlideComponent> _logger;
        private RectTransform _rectTransform;
        private Vector3 _initialPosition;
        private Vector3 _outOfScreenIn;
        private Vector3 _outOfScreenOut;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<SlideComponent>>();
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.localPosition;

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

        public void SlideIn()
        {
            _logger.LogMethodCall(nameof(SlideIn));

            _rectTransform.localPosition = _initialPosition + _outOfScreenIn;

            _ = _rectTransform
                .DOLocalMove(_initialPosition, _duration)
                .SetEase(_ease)
                .OnComplete(() => SlideInCompleted?.Invoke());
        }

        public void SlideOut()
        {
            _logger.LogMethodCall(nameof(SlideOut));

            _rectTransform.localPosition = _initialPosition;

            _ = _rectTransform
                .DOLocalMove(_initialPosition + _outOfScreenOut, _duration)
                .SetEase(_ease)
                .OnComplete(() => SlideOutCompleted?.Invoke());
        }
    }
}
