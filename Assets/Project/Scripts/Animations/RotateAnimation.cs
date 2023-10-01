﻿using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Dominoes.Animations
{
    internal class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _durationMin;
        [SerializeField] private float _durationMax;

        private RectTransform RectTransform { get; set; }

        #region Unity
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            StartCoroutine(StartDelayCoroutine());
        }
        #endregion

        private void Rotate()
        {
            float duration = Random.Range(_durationMin, _durationMax);
            RectTransform
                .DORotate(new Vector3(0, 0, RectTransform.rotation.eulerAngles.z - 180), duration)
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
