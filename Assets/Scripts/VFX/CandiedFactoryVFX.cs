using DG.Tweening;
using UnityEngine;

public class CandiedFactoryVFX : MonoBehaviour
{
    CandiedFruitFactory _candiedFruitFactory;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] RectTransform _resourceTrans;
    [SerializeField] RectTransform _buttonTrans;
    Vector3 _originalButtonTrans => _buttonTrans.position; // Changed here

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Player _player;

    float _animTime = 0.5f;
    float _gap = 15f;
    private void Start()
    {
        _player = Player.Instance;
        _candiedFruitFactory = GetComponent<CandiedFruitFactory>();
        _resourceTrans.localPosition = new Vector3(0, -_gap, 0);
        _buttonTrans.position = _originalButtonTrans; // Changed here
        _canvasGroup.alpha = 0;
    }
    enum State
    {
        Show,
        Hide
    }
    State _state = State.Hide;
    void Update()
    {
        ChangeOrderLayer();
        if (_candiedFruitFactory.playerInRange)
        {
            if (_state == State.Hide)
            {
                ShowResource();
                _state = State.Show;
            }
        }
        else
        {
            if (_state == State.Show)
            {
                HideResource();
                _state = State.Hide;
            }
        }
    }


    private void ChangeOrderLayer()
    {

        float cheatDistance = 0.5f;
        if (_player.transform.position.y + cheatDistance > transform.position.y)
        {
            _spriteRenderer.sortingOrder = 10;
        }
        else
        {
            _spriteRenderer.sortingOrder = 3;
        }
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
            _buttonTrans.position = _originalButtonTrans; // Changed here
            _buttonTrans.DOShakeAnchorPos(0.5f, 2, 90, 90, false, true).onComplete += () =>
            {
                _canvasGroup.interactable = true;
            };
        }
    }


    void ShowResource()
    {
        _canvasGroup.alpha = 0;
        _resourceTrans.localPosition = new Vector3(0, -_gap, 0);
        _resourceTrans.DOAnchorPos(new Vector2(0, 0), _animTime, false).SetEase(Ease.OutExpo);
        _canvasGroup.DOFade(1, _animTime);
    }

    void HideResource()
    {
        _canvasGroup.alpha = 1;
        _resourceTrans.localPosition = new Vector3(0, 0, 0);
        _resourceTrans.DOAnchorPos(new Vector2(0, -_gap), _animTime, false).SetEase(Ease.InExpo);
        _canvasGroup.DOFade(0, _animTime);
    }

}
