using DG.Tweening;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Views
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private RectTransform _loadingTransform;
        [SerializeField] private float _startDelayDuration;
        [SerializeField] private float _rotateDuration;

        public void Animate()
        {
            _loadingTransform.gameObject.SetActive(false);

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .AppendCallback(() => _loadingTransform.gameObject.SetActive(true))
                .AppendCallback(() =>
                {
                    _ = _loadingTransform
                        .DOLocalRotate(Vector3.back * 360f, _rotateDuration, RotateMode.FastBeyond360)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1);
                });
        }
    }
}
