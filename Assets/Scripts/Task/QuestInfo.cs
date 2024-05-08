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
    public int status;
}

[System.Serializable]
public class QuestData
{
    // tạo một biến xem quest đã được người chơi seen chưa
    public bool hasBeenViewed;
    public int currentQuestIndex;
    public List<QuestInfo> questList;
}
