using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TraderVFX : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRect;

    private Player _player;
    private bool playerInRange = false;
    private float _animTime = 0.5f;
    private float _gap = 1f;
    private State _previousState = State.Hide;
    private State _currentState = State.Hide;
    enum State
    {
        Show,
        Hide
    }
    private void Start()
    {
        _player = Player.Instance;
        _canvasRect.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckDistance();
        if (playerInRange)
        {
            if (_currentState == State.Hide)
            {
                Show();
                _currentState = State.Show;
            }
        }
        else
        {
            if (_currentState == State.Show)
            {
                Hide();
                _currentState = State.Hide;
            }
        }
    }

    private void Show()
    {
        // Dotween animation
        _canvasRect.gameObject.SetActive(true);

        var canvasGroup = _canvasRect.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        _canvasRect.localPosition = new Vector3(0, -_gap, 0);
        _canvasRect.DOAnchorPos(new Vector2(0, 0), _animTime, false).SetEase(Ease.OutExpo);
        canvasGroup.DOFade(1, _animTime);
    }

    private void Hide()
    {
        // Dotween animation
        var canvasGroup = _canvasRect.GetComponent<CanvasGroup>();
        _canvasRect.DOAnchorPos(new Vector2(0, -_gap), _animTime, false).SetEase(Ease.OutExpo);
        canvasGroup.DOFade(0, _animTime).OnComplete(()
        => _canvasRect.gameObject.SetActive(false));

    }
    public void CheckDistance()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < 2)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

}