using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TroungVFX : MonoBehaviour
{
    private float _gap = 2f;
    private float _animTime = 0.5f;
    [SerializeField] private RectTransform _canvas;

    private void Start()
    {
        _canvas.gameObject.SetActive(false);
    }

    enum State
    {
        Show,
        Hide
    }
    private void ShowVFX()
    {
        _canvas.gameObject.SetActive(true);
        var canvasGroup = _canvas.GetComponent<CanvasGroup>();

        // Set alpha from 0 -> 1
        canvasGroup.DOFade(1, _animTime);

        // Move from -_gap -> 0
        _canvas.localPosition = new Vector3(0, -_gap, 0);
        _canvas.DOAnchorPosY(0, _animTime).SetEase(Ease.OutExpo);

    }

    private void HideVFX()
    {
        var canvasGroup = _canvas.GetComponent<CanvasGroup>();

        // Set alpha from 1 -> 0
        canvasGroup.DOFade(0, _animTime).SetEase(Ease.OutCubic);

        // Move from 0 -> -_gap
        _canvas.DOAnchorPosY(-_gap, _animTime).SetEase(Ease.InExpo).OnComplete(() =>
        {
            _canvas.gameObject.SetActive(false);
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowVFX();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HideVFX();
        }
    }
}