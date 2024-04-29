using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {

    }

    private void OnPlayerStateChanged(PlayerState state)
    {
        // Todo: replace animation with DOTween

        Debug.Log("Player state: " + state);
        switch (state)
        {
            case PlayerState.Idle:
                // remove previous animation
                this.transform.DOKill();

                IdleAnimation();
                break;
            case PlayerState.Move:
                // remove previous animation
                this.transform.DOKill();

                MoveAnimation();
                break;
        }
    }

    // replace Animator.CrossFade with DOTween
    private void MoveAnimation()
    {
        // _spriteRenderer.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        // Lắc lư lặp lại vô hạn trái phải
        // Debug.Log("Move Animation");
        // this.transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void IdleAnimation()
    {
        var originalScale = new Vector3(1, 1, 1);
        var duration = 0.5f;

        transform.DOScale(new Vector2(0.9f, 1.15f), duration / 2).SetEase(Ease.OutCirc).OnComplete(
            () => transform.DOScale(originalScale, duration / 2).SetEase(Ease.OutCirc)).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        Player.OnPlayerStateChanged += OnPlayerStateChanged;
    }

    private void OnDisable()
    {
        Player.OnPlayerStateChanged -= OnPlayerStateChanged;
    }

}
