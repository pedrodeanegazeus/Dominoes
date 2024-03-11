using System.Threading.Tasks;
using DG.Tweening;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Views.Start
{
    internal class JogatinaView : MonoBehaviour
    {
        [SerializeField] private Image _hatShadow;
        [SerializeField] private RectTransform _hat;
        [SerializeField] private RectTransform _letterJ;
        [SerializeField] private RectTransform _letterO;
        [SerializeField] private RectTransform _letterG;
        [SerializeField] private RectTransform _letterA1;
        [SerializeField] private RectTransform _letterT;
        [SerializeField] private RectTransform _letterI;
        [SerializeField] private RectTransform _letterN;
        [SerializeField] private RectTransform _letterA2;

        private IGzLogger<JogatinaView> _logger;

        public void Initialize()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<JogatinaView>>();
        }

        public Task AnimateAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(AnimateAsync));

            TaskCompletionSource<bool> source = new();
            float hitDuration = 0.7f;

            // hat
            _hat.localPosition = new Vector3(-7f, 789f, 0f);
            _ = DOTween.Sequence()
                .Append(_hat.DOLocalMoveY(166f, hitDuration))
                .Append(_hat.DOLocalMoveY(124f, 0.3f))
                .Append(_hat.DOLocalMoveY(155f, 0.3f))
                .AppendInterval(0.5f)
                .OnComplete(() => source.SetResult(true));

            // hat shadow
            _hatShadow.color = new Color(1f, 1f, 1f, 0f);
            _ = DOTween.Sequence()
                .Append(_hatShadow.DOColor(Color.white, hitDuration));

            // letter J
            _letterJ.localPosition = new Vector3(-206f, 45f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterJ.DOLocalMoveY(21f, 0.3f))
                .Append(_letterJ.DOLocalMoveY(45f, 0.3f))
                .Append(_letterJ.DOLocalMoveY(38f, 0.1f));

            // letter O
            _letterO.localPosition = new Vector3(-155f, 55f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterO.DOLocalMoveY(12f, 0.3f))
                .Append(_letterO.DOLocalMoveY(27f, 0.3f));

            // letter G
            _letterG.localPosition = new Vector3(-86f, 38f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterG.DOLocalMoveY(5f, 0.3f))
                .Append(_letterG.DOLocalMoveY(36f, 0.3f));

            // letter A1
            _letterA1.localPosition = new Vector3(-17f, 58f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterA1.DOLocalMoveY(-3f, 0.3f))
                .Append(_letterA1.DOLocalMoveY(16f, 0.3f))
                .Append(_letterA1.DOLocalMoveY(14f, 0.1f));

            // letter T
            _letterT.localPosition = new Vector3(41f, 73f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterT.DOLocalMoveY(-5f, 0.3f))
                .Append(_letterT.DOLocalMoveY(43f, 0.3f))
                .Append(_letterT.DOLocalMoveY(36f, 0.1f));

            // letter I
            _letterI.localPosition = new Vector3(87f, 57f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterI.DOLocalMoveY(-2f, 0.3f))
                .Append(_letterI.DOLocalMoveY(48f, 0.3f))
                .Append(_letterI.DOLocalMoveY(46f, 0.1f));

            // letter N
            _letterN.localPosition = new Vector3(147f, 58f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterN.DOLocalMoveY(-20f, 0.3f))
                .Append(_letterN.DOLocalMoveY(34f, 0.1f));

            // letter A2
            _letterA2.localPosition = new Vector3(224f, 57f, 0f);
            _ = DOTween.Sequence()
                .AppendInterval(hitDuration)
                .Append(_letterA2.DOLocalMoveY(5f, 0.3f))
                .Append(_letterA2.DOLocalMoveY(51f, 0.3f))
                .Append(_letterA2.DOLocalMoveY(42f, 0.1f));

            return source.Task;
        }
    }
}
