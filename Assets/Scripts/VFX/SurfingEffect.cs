using System.Collections;
using UnityEngine;
using DG.Tweening;
public class SurfingEffect : MonoBehaviour, IShowHide
{
    private float _duration = 0.4f;

    public void Show(Transform transformParent)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            canvasGroup.alpha = 0;
            sequence.Append(canvasGroup.DOFade(1, _duration));
        }
        sequence.Join(transform.DOLocalMoveY(transformParent.localPosition.y + 1, _duration).SetEase(Ease.OutQuad));
    }
    public Tween Hide(Transform transformParent)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            sequence.Append(canvasGroup.DOFade(0, _duration));
        }
        sequence.Join(transform.DOLocalMoveY(transformParent.localPosition.y + 2, _duration).SetEase(Ease.OutQuad));
        return sequence;

    }

}