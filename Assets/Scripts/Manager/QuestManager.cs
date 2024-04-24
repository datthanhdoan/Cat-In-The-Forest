using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestManager : GenericSingleton<QuestManager>
{
    public QuestInfoList _questInfoList;
    public void SetQuestInfoList(QuestInfoList questInfoList)
    {
        Debug.Log("run SetQuestInfoList");
        _questInfoList.questList = questInfoList.questList;
        Debug.Log("[0]" + _questInfoList.questList[0].nameRequester);
    }
}