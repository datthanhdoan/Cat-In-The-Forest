using UnityEngine;
using DG.Tweening;

public class QuestVFX : MonoBehaviour
{
    public void OnShow()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosX(60, 0.5f).SetEase(Ease.OutBack);
    }

    public void OnHide()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosX(-10, 0.5f).SetEase(Ease.InBack);
    }
}