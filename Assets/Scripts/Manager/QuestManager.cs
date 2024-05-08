using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


/// <summary>
/// This file is still in testing phase
/// </summary>

public enum Status
{
    COMPLETED = 1,
    INCOMPLETE = 0
}

public enum VisualStatus
{
    Show,
    Hide
}
public class QuestManager : MonoBehaviour
{
    public static event Action<VisualStatus> NotifyStatusChanged;
    public static event Action<bool> OnHasBeenViewedChange;
    [field: SerializeField] public QuestData _questInfoList { get; private set; }
    private float _timerTesting = 0;
    private float _timeToNextQuest = 0;
    private bool _testIsSetQuest = false;
    private VisualStatus _visualStatus = VisualStatus.Show;
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



    private void LoadData()
    {
        int curIndex = _questInfoList.currentQuestIndex;
        Debug.Log("Current Quest Index: " + curIndex);
        Debug.Log("Quest List Count: " + _questInfoList.questList.Count);
        var curQuest = _questInfoList.questList[curIndex];
        SetQuestInfo(curQuest.nameRequester, curQuest.majorRequester, curQuest.itemRequest, curQuest.amountRequest, curQuest.coinReward);

        _visualStatus = VisualStatus.Show;
        NotifyStatusChanged?.Invoke(_visualStatus);
        OnHasBeenViewedChange?.Invoke(_questInfoList.hasBeenViewed);
    }


    IEnumerator UpdateQuest(float seconds)
    {
        // Cập nhật quest index
        if (_questInfoList.currentQuestIndex < _questInfoList.questList.Count - 1)
        {
            _questInfoList.currentQuestIndex++;
            // Chờ để nhận quest mới

            yield return new WaitForSeconds(seconds);
            int curIndex = _questInfoList.currentQuestIndex;
            var curQuest = _questInfoList.questList[curIndex];
            SetQuestInfo(curQuest.nameRequester, curQuest.majorRequester, curQuest.itemRequest, curQuest.amountRequest, curQuest.coinReward);

            // Thông báo tới VFX là quest cần show
            _visualStatus = VisualStatus.Show;
            NotifyStatusChanged?.Invoke(_visualStatus);
            OnHasBeenViewedChange?.Invoke(false);
        }

    }



    public void CheckAward()
    {
        var curIndex = _questInfoList.currentQuestIndex;

        var itemWanted = _questInfoList.questList[curIndex].itemRequest;
        var amountWanted = _questInfoList.questList[curIndex].amountRequest;
        var coinReward = _questInfoList.questList[curIndex].coinReward;

        Item item = _rM.GetItem(itemWanted);
        ItemType itemType = item.type;

        // Nếu số lượng quả đủ yêu cầu
        if (_rM.GetAmountOfItem(itemType) >= amountWanted)
        {
            int coinAfter = _rM.GetCoin() + coinReward;
            int amountAfter = _rM.GetAmountOfItem(itemType) - amountWanted;

            // thay đổi lại số lượng quả và thay đổi lại số vàng
            _rM.SetAmoutItem(itemType, amountAfter);
            _rM.SetCoin(coinAfter);

            // cập nhật status của quest đó 
            _questInfoList.questList[curIndex].status = (int)Status.COMPLETED;

            // thông báo tới VFX là quest cần hide
            _visualStatus = VisualStatus.Hide;
            NotifyStatusChanged?.Invoke(_visualStatus);
            // đếm ngược thời gian để cập nhật quest mới và cập nhật quest index

            float timeToNextQuest = 10f;
            StartCoroutine(UpdateQuest(timeToNextQuest));


        }
        else
        {
            Debug.Log("Not enough item");
        }

    }

    private void CheckResourceToShowQuest()
    {
        if (_visualStatus == VisualStatus.Hide)
        {
            var curIndex = _questInfoList.currentQuestIndex;
            var itemWanted = _questInfoList.questList[curIndex].itemRequest;
            var amountWanted = _questInfoList.questList[curIndex].amountRequest;
            var coinReward = _questInfoList.questList[curIndex].coinReward;

            Item item = _rM.GetItem(itemWanted);
            ItemType itemType = item.type;

            if (_rM.GetAmountOfItem(itemType) >= amountWanted)
            {
                _visualStatus = VisualStatus.Show;
                NotifyStatusChanged?.Invoke(_visualStatus);
                OnHasBeenViewedChange?.Invoke(false);
            }
        }
    }


    public void OnClickAvatar()
    {
        _questInfoList.hasBeenViewed = true;
        OnHasBeenViewedChange?.Invoke(true);
    }

    public void OnClickCloseButton()
    {
        _visualStatus = VisualStatus.Hide;
        NotifyStatusChanged?.Invoke(_visualStatus);
    }


    public void SetQuestData(QuestData questInfoList)
    {
        _questInfoList.currentQuestIndex = questInfoList.currentQuestIndex;
        _questInfoList.hasBeenViewed = questInfoList.hasBeenViewed;
        _questInfoList.questList = questInfoList.questList;
    }
    public void SetQuestInfo(string nameRequester, string majorRequester, string itemRequest, int amountRequest, int coinReward)
    {
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

    private void OnEnable()
    {
        DataManager.OnDataLoaded += LoadData;
        ResourceManager.OnResourceChanged += CheckResourceToShowQuest;
    }

    private void OnDisable()
    {
        DataManager.OnDataLoaded -= LoadData;
        ResourceManager.OnResourceChanged -= CheckResourceToShowQuest;
    }
}