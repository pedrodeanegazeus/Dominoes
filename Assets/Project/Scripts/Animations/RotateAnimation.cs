using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _durationMin;
        [SerializeField] private float _durationMax;

        private RectTransform _rectTransform;

        #region Unity
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            StartCoroutine(StartDelayCoroutine());
        }
        #endregion

        private void Rotate()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            _rectTransform
                .DORotate(new Vector3(0, 0, _rectTransform.rotation.eulerAngles.z - 180), duration)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear)
                .OnComplete(Rotate);
        }

        private IEnumerator StartDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(_delay / 1000);
            Rotate();
        }
    }
}
