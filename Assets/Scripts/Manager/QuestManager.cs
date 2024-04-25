using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class QuestManager : GenericSingleton<QuestManager>
{
    [field: SerializeField] private QuestInfoList _questInfoList;
    private int _currentQuestIndex = 0;
    private float _timer = 0;
    [SerializeField] private QuestVFX _questVFX;
    private ResourceManager _rM;

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



    public override void Awake()
    {
        base.Awake();
        _rM = ResourceManager.Instance;


    }

    private void Update()
    {
        _timer += Time.deltaTime;

        var timeCheck = 5f;

        if (_timer >= timeCheck)
        {
            Debug.Log("TimeCheck!");
            _timer = 0;
            Debug.Log(_questInfoList.questList.Count);
            Debug.Log(_questInfoList.questList[0].nameRequester);

            SetQuestInfo(_questInfoList.questList[_currentQuestIndex].nameRequester,
                         _questInfoList.questList[_currentQuestIndex].majorRequester,
                         _questInfoList.questList[_currentQuestIndex].itemRequest,
                         _questInfoList.questList[_currentQuestIndex].amountRequest,
                         _questInfoList.questList[_currentQuestIndex].coinReward);
            _questVFX.OnShow();

            if (_currentQuestIndex < _questInfoList.questList.Count - 1)
            {
                _currentQuestIndex++;
            }

        }
    }
    public void ShowQuest()
    {

        _questVFX.ToggleQuest();

    }

    public void SetQuestInfoList(QuestInfoList questInfoList)
    {
        _questInfoList.questList = questInfoList.questList;
    }
    public void SetQuestInfo(string nameRequester, string majorRequester, string itemRequest, int amountRequest, int coinReward)
    {

        if (Enum.TryParse(itemRequest, out ItemType itemType))
        {
            _itemWantedImage.sprite = _rM.GetItem(itemType).gameObject.GetComponent<SpriteRenderer>().sprite;
        }


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

        var itemWantedType = (ItemType)Enum.Parse(typeof(ItemType), itemWanted);
        var item = _rM.GetItem(itemWantedType);
        Debug.Log("Item wanted type :" + itemWantedType);
        if (_rM.GetAmountOfItem(itemWantedType) >= amountWanted)
        {
            int coinAfter = _rM.GetCoin() + coinReward;
            int amountAfter = _rM.GetAmountOfItem(itemWantedType) - amountWanted;

            _rM.SetAmoutItem(itemWantedType, amountAfter);
            _rM.SetCoin(coinAfter);
            _questVFX.OnHide();
        }
        else
        {
            Debug.Log("Not enough item");
        }
    }

}