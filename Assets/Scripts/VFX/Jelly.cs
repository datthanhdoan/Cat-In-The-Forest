using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
public class Jelly : MonoBehaviour, IEffect
{
    [NonSerialized] public float duration = 0.1f;
    [NonSerialized] public Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Effect()
    {
        transform.DOScale(new Vector2(0.9f, 1.15f), duration / 2).SetEase(Ease.OutCirc).OnComplete(
            () => transform.DOScale(originalScale, duration / 2).SetEase(Ease.OutCirc));
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }
}

