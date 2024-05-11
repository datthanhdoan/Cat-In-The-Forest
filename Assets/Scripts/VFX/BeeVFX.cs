using UnityEngine;
using DG.Tweening;

public class BeeVFX : MonoBehaviour
{
    public float duration = 3.0f;
    public float strength = 0.04f;
    public int vibrato = 2;
    public float randomness = 10.0f;


    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // dung dotween de tao hieu ung di chuyen
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true);
        // dung dotween de tao hieu ung xoay

    }

}