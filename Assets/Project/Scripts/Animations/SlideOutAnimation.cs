using System;
using DG.Tweening;
using UnityEngine;

namespace Dominoes.Animations
{
    public enum SlideTo
    {
        Left,
        Right,
    }

    internal class SlideOutAnimation : MonoBehaviour
    {
        [SerializeField] private Ease _ease;
        [SerializeField] private SlideTo _to;
        [SerializeField] private float _duration;

        private RectTransform _rectTransform;
        private Vector3 _initialPosition;
        private Vector3 _outOfScreen;

        public void SlideOut(TweenCallback callback = null)
        {
            _ = _rectTransform
                .DOMove(_initialPosition + _outOfScreen, _duration)
                .SetEase(_ease)
                .OnComplete(callback);
        }

        #region Unity
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.position;
            _outOfScreen = _to switch
            {
                SlideTo.Left => Vector3.left * Screen.width,
                SlideTo.Right => Vector3.right * Screen.width,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
        }
        #endregion
    }
}
