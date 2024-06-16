using System.Collections;
using UnityEngine;
using DG.Tweening;
using TreeEditor;
public class SurfingEffect : MonoBehaviour, IShowHide
{

    public void Show(Transform transformParent, float distance, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            canvasGroup.alpha = 0;
            sequence.Append(canvasGroup.DOFade(1, duration));
        }
        Debug.Log("transformParent.localPosition.y: " + transformParent.localPosition.y);
        float targetYPosition = transformParent.localPosition.y + distance;
        Debug.Log("targetYPosition: " + targetYPosition);
        sequence.Join(transform.DOLocalMoveY(targetYPosition, duration).SetEase(Ease.OutQuad));
    }
    public Tween Hide(Transform transformParent, float distance, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            sequence.Append(canvasGroup.DOFade(0, duration));
        }
        float targetYPosition = transformParent.localPosition.y - distance;
        sequence.Join(transform.DOLocalMoveY(targetYPosition, duration).SetEase(Ease.OutQuad));
        return sequence;

    }

}