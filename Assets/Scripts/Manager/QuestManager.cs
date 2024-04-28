using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;

/// <summary>
/// This file is still in testing phase
/// </summary>

public class QuestManager : GenericSingleton<QuestManager>
{
    [field: SerializeField] private QuestInfoList _questInfoList;
    private int _currentQuestIndex = 0;
    private float _timerTesting = 0;
    private bool _testIsSetQuest = false;
    [SerializeField] private QuestVFX _questVFX; // Import in Unity Editor
    [SerializeField] private ResourceManager _rM; // Import in Unity Editor

    // Item Wanted
    [Header("Item Wanted")]
    [SerializeField] private Image _itemWantedImage;
    [SerializeField] private TextMeshProUGUI _itemWantedAmountText;

    // Info Requester
    [Header("Info Requester")]
    [SerializeField] private TextMeshProUGUI _nameRequesterText;
    [SerializeField] private TextMeshProUGUI _majorRequesterText;

    [Header("Quest Info")]
    // Reward
    [SerializeField] private TextMeshProUGUI _coinRewardText;

    private void Update()
    {
        if (_testIsSetQuest) return;
        _timerTesting += Time.deltaTime;
        if (_timerTesting >= 2)
        {
            SetQuestInfo(_questInfoList.questList[_currentQuestIndex].nameRequester,
                         _questInfoList.questList[_currentQuestIndex].majorRequester,
                         _questInfoList.questList[_currentQuestIndex].itemRequest,
                         _questInfoList.questList[_currentQuestIndex].amountRequest,
                         _questInfoList.questList[_currentQuestIndex].coinReward);
            _questVFX.OnShow();
            _testIsSetQuest = true;
        }
    }


    public void SetQuestInfoList(QuestInfoList questInfoList)
    {
        _questInfoList.questList = questInfoList.questList;
    }
    public void SetQuestInfo(string nameRequester, string majorRequester, string itemRequest, int amountRequest, int coinReward)
    {

        // if (Enum.TryParse(itemRequest, out ItemType itemType))
        // {
        //     _itemWantedImage.sprite = _rM.GetItem(itemType).gameObject.GetComponent<SpriteRenderer>().sprite;
        // }
        var item = _rM.GetItem(itemRequest);
        _itemWantedImage.sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;

        // nameRequester
        _nameRequesterText.text = nameRequester;
        // majorRequester
        _majorRequesterText.text = majorRequester;
        // itemWanted
        _itemWantedAmountText.text = amountRequest.ToString();
        // coinReward
        _coinRewardText.text = coinReward.ToString();
    }

    public void CheckAward()
    {
        var itemWanted = _questInfoList.questList[_currentQuestIndex].itemRequest;
        var amountWanted = _questInfoList.questList[_currentQuestIndex].amountRequest;
        var coinReward = _questInfoList.questList[_currentQuestIndex].coinReward;

        // ItemType itemWantedType = (ItemType)Enum.Parse(typeof(ItemType), itemWanted);

        Item item = _rM.GetItem(itemWanted);
        ItemType itemType = item.type;
        Debug.Log(itemType);

        if (_rM.GetAmountOfItem(itemType) >= amountWanted)
        {
            int coinAfter = _rM.GetCoin() + coinReward;
            int amountAfter = _rM.GetAmountOfItem(itemType) - amountWanted;

            _rM.SetAmoutItem(itemType, amountAfter);
            _rM.SetCoin(coinAfter);
            _questVFX.OnHide();
        }
        else
        {
            Debug.Log("Not enough item");
        }
    }

}