using System.Collections;
using DG.Tweening;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private float _delay;
        [SerializeField] private float _durationMin;
        [SerializeField] private float _durationMax;

        private IGzLogger<RotateAnimation> _logger;
        private RectTransform _rectTransform;
        private bool _isStarted;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<RotateAnimation>>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnDisable()
        {
            StopAnimation();
        }

        private void OnEnable()
        {
            if (_startOnEnable)
            {
                StartAnimation();
            }
        }
        #endregion

        public void StartAnimation()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(StartAnimation));

            if (_isStarted)
            {
                _logger.Warn("Pulse animation has already been started");
            }
            _ = StartCoroutine(StartDelayCoroutine());
            _isStarted = true;
        }

        public void StopAnimation()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(StopAnimation));

            if (!_isStarted)
            {
                _logger.Error("Pulse animation has not been started yet");
                return;
            }
            _isStarted = false;
        }

        private void Rotate()
        {
            if (_isStarted)
            {
                float duration = Random.Range(_durationMin, _durationMax);
                _ = _rectTransform
                    .DORotate(new Vector3(0, 0, _rectTransform.rotation.eulerAngles.z - 180), duration)
                    .SetSpeedBased(true)
                    .SetEase(Ease.Linear)
                    .OnComplete(Rotate);
            }
        }

        private IEnumerator StartDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(_delay / 1000);
            Rotate();
        }
    }
}
