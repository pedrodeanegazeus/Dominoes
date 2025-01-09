using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views
{
    public class ShadowsView : MonoBehaviour
    {
        public event Action AnimationCompleted;

        [SerializeField] private Image _hatShadowImage;
        [SerializeField] private float _hatShadowFadeDuration;

        public void Animate()
        {
            Color transparentColor = new(_hatShadowImage.color.r,
                                         _hatShadowImage.color.g,
                                         _hatShadowImage.color.b,
                                         0);

            _hatShadowImage.color = transparentColor;

            _ = DOTween.Sequence()
                .Append(_hatShadowImage.DOFade(1, _hatShadowFadeDuration))
                .OnComplete(() => AnimationCompleted?.Invoke());
        }
    }
}
