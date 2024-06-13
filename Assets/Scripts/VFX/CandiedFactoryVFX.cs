using DG.Tweening;
using UnityEngine;

public class CandiedFactoryVFX : MonoBehaviour
{
    CandiedFruitFactory _candiedFruitFactory;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] RectTransform _resourceTrans;
    [SerializeField] RectTransform _buttonTrans;
    Vector3 _originalButtonTrans; // Changed here
    Vector3 OriginalButtonTrans { get => _originalButtonTrans; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Player _player;

    float _animTime = 0.5f;
    float _gap = 15f;
    private void Start()
    {
        _player = Player.Instance;
        _candiedFruitFactory = GetComponent<CandiedFruitFactory>();
        _resourceTrans.localPosition = new Vector3(0, -_gap, 0);
        _canvasGroup.alpha = 0;
        _originalButtonTrans = _buttonTrans.position;
    }
    enum State
    {
        Show,
        Hide
    }
    State _state = State.Hide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _state = State.Show;
        ShowResource();
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        HideResource();
        _state = State.Hide;
    }

    public void AnimButton()
    {
        if (_candiedFruitFactory._allConditions)
        {
            _buttonTrans.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 10, 1);
        }
        else
        {
            _canvasGroup.interactable = false;
            _buttonTrans.DOShakeAnchorPos(0.5f, 2, 90, 90, false, true).SetEase(Ease.InOutElastic).onComplete += () =>
            {
                _canvasGroup.interactable = true;
            };
        }
    }


    void ShowResource()
    {
        _canvasGroup.gameObject.SetActive(true);
        _resourceTrans.localPosition = new Vector3(0, -_gap, 0);
        _resourceTrans.DOAnchorPos(new Vector2(0, 0), _animTime, false).SetEase(Ease.OutExpo);
        _canvasGroup.DOFade(1, _animTime);
    }

    void HideResource()
    {
        _resourceTrans.localPosition = new Vector3(0, 0, 0);
        _resourceTrans.DOAnchorPos(new Vector2(0, -_gap), _animTime, false).SetEase(Ease.InExpo);
        _canvasGroup.DOFade(0, _animTime).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _canvasGroup.gameObject.SetActive(false);
        });
    }

}
