using UnityEngine;
using DG.Tweening;
public class ExclamationMark : MonoBehaviour
{
    // Exclamation mark in Canvas
    public float duration = 0.3f;
    public float delay = 0.5f;

    // Tạo hiệu ứng lắc lư , xoay theo trục Z
    public void Shake()
    {
        transform.DORotate(new Vector3(0, 0, 10), 0);
        Sequence mySequence = DOTween.Sequence();
        mySequence.SetLoops(-1, LoopType.Yoyo);
        mySequence.Append(transform.DORotate(new Vector3(0, 0, -10), duration));
        mySequence.Append(transform.DORotate(new Vector3(0, 0, 10), duration));


    }


    private void Start()
    {
        Shake();
    }
}