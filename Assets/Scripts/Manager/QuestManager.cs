using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestManager : GenericSingleton<QuestManager>
{
    public QuestInfoList _questInfoList;
    public void SetQuestInfoList(QuestInfoList questInfoList)
    {
        _questInfoList.questList = questInfoList.questList;
        Debug.Log(questInfoList.questList[0].nameRequester);
    }
}