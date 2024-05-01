using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _trailEffect;
    private Sequence _currentSequence;

    private void OnPlayerStateChanged(PlayerState state)
    {
        // Todo: replace animation with DOTween

        switch (state)
        {
            case PlayerState.Idle:
                // remove previous animation
                IdleState();
                _trailEffect.Stop();
                break;
            case PlayerState.Move:
                // remove previous animation
                MoveState();
                _trailEffect.Play();
                break;
        }
    }

    // replace Animator.CrossFade with DOTween
    private void MoveState()
    {
        transform.DOKill();

        _currentSequence = DOTween.Sequence();

        // Add animations to the Sequence
        _currentSequence.Append(transform.DORotate(new Vector3(0, 0, 9.45f), 0.2f).SetEase(Ease.Linear));
        _currentSequence.Append(transform.DORotate(new Vector3(0, 0, -9.45f), 0.2f).SetEase(Ease.Linear));

        _currentSequence.SetLoops(-1, LoopType.Yoyo);

        // Start the Sequence
        _currentSequence.Play();
    }

    private void IdleState()
    {
        // remove previous animation and reset scale
        _currentSequence?.Kill();

        transform.DOKill();
        transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        transform.DORotate(new Vector3(0, 0, 0), 0.1f);

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
