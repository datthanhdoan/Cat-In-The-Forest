using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ShopVFX : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRect;

    private Player _player;
    private float _animTime = 0.5f;
    private float _gap = 1f;
    private void Start()
    {
        _player = Player.Instance;
        _canvasRect.gameObject.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hide();
        }
    }


}