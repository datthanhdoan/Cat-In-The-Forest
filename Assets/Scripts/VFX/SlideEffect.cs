using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlideEffect : MonoBehaviour, IShowHide
{
    public void Show(Transform transformParent, float distance, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            canvasGroup.alpha = 0;
            sequence.Append(canvasGroup.DOFade(1, duration));
        }
        sequence.Join(transform.DOLocalMoveY(transformParent.localPosition.y + distance, duration).SetEase(Ease.OutQuad));
    }
    public Tween Hide(Transform transformParent, float distance, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            sequence.Append(canvasGroup.DOFade(0, 0.5f));
        }
        // return transform.DOLocalMoveY(transformParent.localPosition.y - 1, 0.5f).SetEase(Ease.OutQuad);
        sequence.Join(transform.DOLocalMoveY(transformParent.localPosition.y - distance, duration).SetEase(Ease.OutQuad));
        return sequence;

    }
}
