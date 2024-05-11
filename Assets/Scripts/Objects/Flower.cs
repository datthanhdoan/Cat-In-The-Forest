using UnityEngine;

public class Flower : MonoBehaviour
{
    public int _flowerHoney { get; private set; } = 4;

    public void SetFlowerHoney(int amount)
    {
        _flowerHoney = amount;
    }


}