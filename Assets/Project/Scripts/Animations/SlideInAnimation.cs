using System;
using DG.Tweening;
using UnityEngine;

namespace Dominoes.Animations
{
    public enum SlideFrom
    {
        Left,
        Right,
    }

    internal class SlideInAnimation : MonoBehaviour
    {
        [SerializeField] private Ease _ease;
        [SerializeField] private SlideFrom _from;
        [SerializeField] private float _duration;

        private RectTransform _rectTransform;
        private Vector3 _initialPosition;
        private Vector3 _outOfScreen;

        public void SlideIn(TweenCallback callback = null)
        {
            _rectTransform.position = _initialPosition + _outOfScreen;
            _ = _rectTransform
                .DOMove(_initialPosition, _duration)
                .SetEase(_ease)
                .OnComplete(callback);
        }

        #region Unity
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.position;
            _outOfScreen = _from switch
            {
                SlideFrom.Left => Vector3.left * Screen.width,
                SlideFrom.Right => Vector3.right * Screen.width,
                _ => throw new NotImplementedException("Direction not implemented"),
            };
        }
        #endregion
    }
}
