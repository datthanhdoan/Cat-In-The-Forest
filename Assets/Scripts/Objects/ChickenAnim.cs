using UnityEditor.Tilemaps;
using UnityEngine;

public class ChickenAnim : MonoBehaviour, IObserver
{
    private Animator _anim;
    private bool _faceRight = true;
    [SerializeField] private ChickenMoverment _chickenMoverment;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _chickenMoverment.AddObserver(this);
    }


    private void OnDisable()
    {
        _chickenMoverment.RemoveObserver(this);
    }
    public void OnNotify()
    {
        UpdateAnimation(_chickenMoverment.CurrentState);
    }

    private readonly int _idle = Animator.StringToHash("Chicken_Idle");
    private readonly int _walk = Animator.StringToHash("Chicken_Move");
    private void UpdateAnimation(ChickenMoverment.ChickenState state)
    {
        switch (state)
        {
            case ChickenMoverment.ChickenState.Idle:
                _anim.CrossFade(_idle, 0);
                break;
            case ChickenMoverment.ChickenState.Walk:
                Flip();
                _anim.CrossFade(_walk, 0);
                break;
            case ChickenMoverment.ChickenState.Run:

                break;
            case ChickenMoverment.ChickenState.Eat:

                break;
            case ChickenMoverment.ChickenState.Sleep:

                break;
        }
    }

    private void Flip()
    {
        Debug.Log(transform.parent.name + "Flip");
        if (_chickenMoverment.TargetPos.x > transform.position.x && !_faceRight)
        {
            _faceRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0); // Xoay về phía bên phải
        }
        else if (_chickenMoverment.TargetPos.x < transform.position.x && _faceRight)
        {
            _faceRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0); // Xoay về phía bên trái
        }


    }
}