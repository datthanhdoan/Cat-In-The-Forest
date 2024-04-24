using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class QuestInfo
{
    public string nameRequester;
    public string majorRequester;
    public string itemRequest;
    public int amountRequest;
    public int coinReward;
}
[System.Serializable]
public class QuestInfoList
{
    public List<QuestInfo> questList;
}