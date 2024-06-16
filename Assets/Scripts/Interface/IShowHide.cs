using DG.Tweening;
using UnityEngine;
public interface IShowHide
{
    void Show(Transform transformParent, float distance, float duration);
    Tween Hide(Transform transformParent, float distance, float duration);
}