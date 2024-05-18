using DG.Tweening;
using UnityEngine;
public interface IShowHide
{
    void Show(Transform transformParent);
    Tween Hide(Transform transformParent);
}