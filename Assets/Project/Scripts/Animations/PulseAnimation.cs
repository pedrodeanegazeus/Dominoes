using System.Collections;
using DG.Tweening;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class PulseAnimation : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private float _delay;
        [SerializeField] private float _durationMin;
        [SerializeField] private float _durationMax;
        [SerializeField] private float _pulseMin;
        [SerializeField] private float _pulseMax;

        private IGzLogger<PulseAnimation> _logger;
        private Coroutine _coroutine;
        private RectTransform _rectTransform;
        private bool _isStarted;

        public void StartAnimation()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(StartAnimation));

            if (_isStarted)
            {
                _logger.Warn("Pulse animation has already been started");
            }
            _coroutine = StartCoroutine(StartDelayCoroutine());
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
            StopCoroutine(_coroutine);
            _isStarted = false;
        }

        #region Unity
        private void Awake()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<PulseAnimation>>();
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localScale = Vector3.zero;
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

        private void PulseIn()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            _ = _rectTransform
                .DOScale(Vector3.zero, duration)
                .OnComplete(PulseOut);
        }

        private void PulseOut()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            float pulse = Random.Range(_pulseMin, _pulseMax);
            _ = _rectTransform
                .DOScale(new Vector3(1, 1, 1) * pulse, duration)
                .SetEase(Ease.OutBack)
                .SetDelay(2)
                .OnComplete(PulseIn);
        }

        private IEnumerator StartDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(_delay / 1000);
            PulseIn();
        }
    }
}
