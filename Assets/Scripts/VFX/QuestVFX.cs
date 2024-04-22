using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestVFX : MonoBehaviour
{
    [SerializeField] private RectTransform _tableDescription;
    private bool _isTableDesShow = false;
    private bool _isQuestShow = false;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnShow()
    {
        _rectTransform.DOAnchorPosX(35, 0.5f).SetEase(Ease.OutBack);

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

    public void ToggleQuest()
    {
        if (_isQuestShow)
        {
            OnHide();
        }
        else
        {
            OnShow();
        }
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

    public void ToggleTableDescription()
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
}