using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;


/// <summary>
/// This file is still in testing phase
/// </summary>
public class QuestManager : MonoBehaviour
{
    public enum EachQuestStatus
    {
        NOTCCOMPLETED = 0,
        COMPLETED = 1
    }
    public enum VisualStatus
    {
        Show,
        Hide
    }
    public event Action<VisualStatus> OnVisualQuestChanged;
    private PlayerInputAction _inputActions;
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

    [field: SerializeField] public QuestData _questInfoList { get; private set; }

    private void LoadData()
    {
        int curIndex = _questInfoList.currentQuestIndex;
        // Debug.Log("Current Quest Index: " + curIndex);
        var curQuest = _questInfoList.questList[curIndex];
        SetQuestInfo(curQuest.nameRequester, curQuest.majorRequester, curQuest.itemRequest, curQuest.amountRequest, curQuest.coinReward);

        // wait seconds to show quest
        float seconds = 3f;
        StartCoroutine(ShowQuest(seconds));
    }

    IEnumerator ShowQuest(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnVisualQuestChanged?.Invoke(VisualStatus.Show);
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
            OnVisualQuestChanged?.Invoke(VisualStatus.Show);
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
            _questInfoList.questList[curIndex].status = (int)EachQuestStatus.COMPLETED;


            // thông báo tới VFX là quest cần hide
            OnVisualQuestChanged?.Invoke(VisualStatus.Hide);
            // đếm ngược thời gian để cập nhật quest mới và cập nhật quest index

            float timeToNextQuest = 10f;
            StartCoroutine(UpdateQuest(timeToNextQuest));


        }
        else
        {
            Debug.Log("Not enough item");
        }

    }

    public void OnClickAvatar()
    {
        _questInfoList.hasBeenViewed = true;
    }

    public void OnClickCloseButton()
    {
        OnVisualQuestChanged?.Invoke(VisualStatus.Hide);
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
        _inputActions = new PlayerInputAction();
        _inputActions.UI.ClaimAward.performed += OnClaimAwardGamePad;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        DataManager.OnDataLoaded -= LoadData;
        _inputActions.UI.ClaimAward.performed -= OnClaimAwardGamePad;
        _inputActions.Dispose();
    }

    #region Gamepad

    public void OnClaimAwardGamePad(InputAction.CallbackContext context)
    {
        CheckAward();
    }
    #endregion
}