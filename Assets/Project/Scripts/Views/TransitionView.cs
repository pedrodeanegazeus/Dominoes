using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views
{
    public class TransitionView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Canvas _transitionCanvas;
        [SerializeField] private Image _transitionTiles;

        [Space]
        [Range(0f, 1f)]
        [SerializeField] private float _initialAlpha;

        [Range(0f, 1f)]
        [SerializeField] private float _finalAlpha;

        [SerializeField] private float _transitionStepDuration;

        public event Action InAnimationCompleted;
        public event Action OutAnimationCompleted;

        private const int _steps = 7;
        private const string _materialTilingAttribute = "_Tiling";

        public void AnimateIn()
        {
            Color transitionTilesColor = _transitionTiles.color;
            transitionTilesColor.a = _initialAlpha;
            _transitionTiles.color = transitionTilesColor;

            _transitionTiles.material.SetInt(_materialTilingAttribute, 0);

            _transitionCanvas.gameObject.SetActive(true);

            float transitionAlphaStep = (_finalAlpha - _initialAlpha) / _steps;
            int step = 0;

            _ = DOTween.Sequence()
                .AppendCallback(() => _transitionTiles.material.SetInt(_materialTilingAttribute, ++step))
                .AppendCallback(() => transitionTilesColor.a += transitionAlphaStep)
                .AppendCallback(() => _transitionTiles.color = transitionTilesColor)
                .AppendInterval(_transitionStepDuration)
                .SetLoops(_steps)
                .OnComplete(() => InAnimationCompleted?.Invoke());
        }

        public void AnimateOut()
        {
            Color transitionTilesColor = _transitionTiles.color;
            transitionTilesColor.a = _finalAlpha;
            _transitionTiles.color = transitionTilesColor;

            _transitionTiles.material.SetInt(_materialTilingAttribute, _steps);

            float transitionAlphaStep = (_finalAlpha - _initialAlpha) / _steps;
            int step = _steps;

            _ = DOTween.Sequence()
                .AppendCallback(() => _transitionTiles.material.SetInt(_materialTilingAttribute, --step))
                .AppendCallback(() => transitionTilesColor.a -= transitionAlphaStep)
                .AppendCallback(() => _transitionTiles.color = transitionTilesColor)
                .AppendInterval(_transitionStepDuration)
                .SetLoops(_steps)
                .OnComplete(() =>
                {
                    _transitionCanvas.gameObject.SetActive(false);
                    OutAnimationCompleted?.Invoke();
                });
        }
    }
}
