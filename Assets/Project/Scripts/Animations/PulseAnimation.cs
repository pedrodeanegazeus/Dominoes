using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class PulseAnimation : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _durationMin;
        [SerializeField] private float _durationMax;
        [SerializeField] private float _pulseMin;
        [SerializeField] private float _pulseMax;

        private RectTransform _rectTransform;

        #region Unity
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            StartCoroutine(StartDelayCoroutine());
        }
        #endregion

        private void PulseIn()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            _rectTransform
                .DOScale(Vector3.zero, duration)
                .OnComplete(PulseOut);
        }

        private void PulseOut()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            float pulse = Random.Range(_pulseMin, _pulseMax);
            _rectTransform
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
