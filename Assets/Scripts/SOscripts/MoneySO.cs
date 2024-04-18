using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MoneySO", menuName = "ScriptableObjects/Money")]


public class MoneySO : ScriptableObject
{

    public int coin { get; private set; } = 0;
    public void SetCoin(int value) => coin = value;
}