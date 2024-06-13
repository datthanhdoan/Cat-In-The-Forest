using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class QuestVFX : MonoBehaviour
{
    [SerializeField] private RectTransform _tableDescription;
    [SerializeField] private GameObject _exclamationMark;
    private bool _isTableDesShow = false;
    private bool _isQuestShow = false;
    private PlayerInputAction _inputActions;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _inputActions = new PlayerInputAction();
        _inputActions.UI.QuestTableDecription.performed += ToggleTableDescription;
        _inputActions.Enable();
    }


    private void ControlShowHideQuest(VisualStatus status)
    {
        if (status == VisualStatus.Show)
        {
            OnShow();
        }
        if (status == VisualStatus.Hide)
        {
            OnHide();
        }
    }
    public void OnShow()
    {
        _rectTransform.DOAnchorPosX(30, 0.5f).SetEase(Ease.OutBack);

        var questCanvasGroup = GetComponent<CanvasGroup>();
        questCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutBack);

        _isQuestShow = true;
    }

    public void OnHide()
    {
        _rectTransform.DOAnchorPosX(-30, 0.5f).SetEase(Ease.InBack);

        var questCanvasGroup = GetComponent<CanvasGroup>();
        questCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.InBack);

        // also hide table description
        if (_isTableDesShow)
        {
            HideTableDescription();
        }

        _isQuestShow = false;

    }



    public void ShowTableDescription()
    {
        // _tableDescription.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);
        var tableCanvasGroup = _tableDescription.GetComponent<CanvasGroup>();
        tableCanvasGroup.alpha = 0;

        _tableDescription.gameObject.SetActive(true);

        // trong lúc đang show thì không cho click vào bảng
        tableCanvasGroup.interactable = false;

        tableCanvasGroup.DOFade(1, 0.5f).OnComplete(() =>
        {
            // sau khi show xong thì cho click vào bảng
            tableCanvasGroup.interactable = true;
            _isTableDesShow = true;
        });

    }

    public void HideTableDescription()
    {
        var tableCanvasGroup = _tableDescription.GetComponent<CanvasGroup>();

        // trong lúc đang hide thì không cho click vào bảng
        tableCanvasGroup.interactable = false;

        tableCanvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            tableCanvasGroup.interactable = true;
            _tableDescription.gameObject.SetActive(false);
            _isTableDesShow = false;
        });
    }

    public void ToggleTableDescription(InputAction.CallbackContext context)
    {
        if (_isTableDesShow)
        {
            HideTableDescription();
        }
        else
        {
            ShowTableDescription();
        }
    }


    private void ExclamationMarkStatus(bool hasBeenViewed)
    {
        if (hasBeenViewed)
        {
            _exclamationMark.SetActive(false);
        }
        else
        {
            _exclamationMark.SetActive(true);
        }
    }
    private void OnEnable()
    {
        QuestManager.NotifyStatusChanged += ControlShowHideQuest;
        QuestManager.OnHasBeenViewedChange += ExclamationMarkStatus;
    }
    private void OnDisable()
    {
        QuestManager.NotifyStatusChanged -= ControlShowHideQuest;
        QuestManager.OnHasBeenViewedChange -= ExclamationMarkStatus;
    }
}