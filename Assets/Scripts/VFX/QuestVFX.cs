using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class QuestVFX : MonoBehaviour
{
    private bool _isTableDesShow = false;
    private bool _isTableTransition = false;
    private bool _isQuestShow = false;
    private bool _isQuestTransition = false;
    private bool _isQuestHasBeenViewed = false;
    private PlayerInputAction _inputActions;
    private RectTransform _rectTransform;
    [SerializeField] private QuestManager _questManager;

    [SerializeField] private RectTransform _tableDescription;
    [SerializeField] private GameObject _exclamationMark;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    #region Logic for VFX
    private void HandelVisual(QuestManager.VisualStatus status)
    {
        if (!_isQuestTransition)
        {
            switch (status)
            {
                case QuestManager.VisualStatus.Show:
                    ShowQuest();
                    _isQuestHasBeenViewed = true;
                    break;
                case QuestManager.VisualStatus.Hide:
                    HideQuest();
                    _isQuestHasBeenViewed = false;
                    break;
            }

            ExclamationMark__HandelStatus(_isQuestHasBeenViewed);
        }
    }


    public void ToggleTableDescriptionGamePad(InputAction.CallbackContext context)
    {
        ToggleTableDescription();
    }

    public void ToggleTableDescription()
    {
        if (_isTableTransition) return;
        if (!_isQuestShow) return;

        if (!_isQuestHasBeenViewed)
        {
            _isQuestHasBeenViewed = true;
            ExclamationMark__HandelStatus(_isQuestHasBeenViewed);
        }

        if (_isTableDesShow)
        {
            HideTableDescription();
        }
        else
        {
            ShowTableDescription();
        }
    }




    private void ExclamationMark__HandelStatus(bool hasBeenViewed)
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
        _questManager.OnVisualQuestChanged += HandelVisual;

        // input
        _inputActions = new PlayerInputAction();
        _inputActions.UI.QuestTableDecription.performed += ToggleTableDescriptionGamePad;
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _questManager.OnVisualQuestChanged -= HandelVisual;

        // input
        _inputActions.UI.QuestTableDecription.performed -= ToggleTableDescriptionGamePad;
        _inputActions.Disable();
    }
    #endregion

    #region VFX

    public void ShowQuest()
    {
        // set transition to true
        _isQuestTransition = true;

        _rectTransform.DOAnchorPosX(30, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            // after show quest, set transition to false
            _isQuestTransition = false;
            _isQuestShow = true;
        });

        var questCanvasGroup = GetComponent<CanvasGroup>();
        questCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutBack);

    }

    public void HideQuest()
    {
        // set transition to true
        _isQuestTransition = true;

        _rectTransform.DOAnchorPosX(-30, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            // after hide quest, set transition to false
            _isQuestTransition = false;
            _isQuestShow = false;
        });

        var questCanvasGroup = GetComponent<CanvasGroup>();
        questCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.InBack);

        // also hide table description
        if (_isTableDesShow)
        {
            HideTableDescription();
        }
    }



    public void ShowTableDescription()
    {
        // set transition to true
        _isTableTransition = true;

        // control alpha of table description
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

            // set transition to false
            _isTableTransition = false;
        });

    }

    public void HideTableDescription()
    {
        // set transition to true
        _isTableTransition = true;

        var tableCanvasGroup = _tableDescription.GetComponent<CanvasGroup>();

        // trong lúc đang hide thì không cho click vào bảng
        tableCanvasGroup.interactable = false;

        tableCanvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            tableCanvasGroup.interactable = true;
            _tableDescription.gameObject.SetActive(false);
            _isTableDesShow = false;

            // set transition to false
            _isTableTransition = false;
        });
    }

    #endregion
}