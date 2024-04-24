using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResourceItems", menuName = "ScriptableObjects/Resource")]
public class ItemSO : ScriptableObject
{
    public List<GameObject> itemList = new List<GameObject>();

}