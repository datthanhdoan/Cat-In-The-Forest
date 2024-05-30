using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Bee : MonoBehaviour
{


    private float _speed = 0.5f;
    private float _timeToTakeHoney = 3f;
    private float _TIMER = 0f;
    [SerializeField] private Flower _targetFlower;
    private bool _isTakingHoney = false;
    private bool _isAtFlowerPosition = false;
    public int _honeyCollected { get; private set; } = 0;
    private BeeState _beeState = BeeState.MovingToFlower;
    [SerializeField] private Transform _beeHive;
    public void SetHoneyCollected(int amount)
    {
        _honeyCollected = amount;
    }


    enum BeeState
    {
        MovingToFlower,
        WaitToTakeHoney,
        GoingBack,
        Done
    }
    private void OnEnable()
    {
        _beeState = BeeState.MovingToFlower;
    }

    private void FixedUpdate()
    {
        HandelMoving();
        HandelTakingHoney();
        TransferHoneyToHive();
    }

    private void HandelTakingHoney()
    {
        if (_beeState == BeeState.WaitToTakeHoney && !_isTakingHoney)
        {
            _TIMER += Time.deltaTime;
            if (_TIMER >= _timeToTakeHoney)
            {
                _isTakingHoney = true;
                _TIMER = 0;
                TakeingHoney();
                _beeState = BeeState.GoingBack;
            }
        }

    }

    private void TakeingHoney()
    {
        _honeyCollected = _targetFlower._flowerHoney;
    }

    private void HandelMoving()
    {
        Vector3 pos = transform.position;
        switch (_beeState)
        {
            case BeeState.MovingToFlower:
                // nếu không ở vị trí hoa thì di chuyển đến vị trí hoa
                _isAtFlowerPosition = Vector2.Distance(pos, _targetFlower.transform.position) < 0.1f;
                if (!_isAtFlowerPosition)
                {
                    transform.position = Vector2.Lerp(pos, _targetFlower.transform.position, _speed * Time.fixedDeltaTime);
                }
                else
                {
                    // nếu ở vị trí hoa thì chuyển sang trạng thái chờ lấy mật ong
                    _beeState = BeeState.WaitToTakeHoney;
                }
                break;
            case BeeState.GoingBack:
                // nếu không ở vị trí ban đầu thì di chuyển về vị trí của tổ
                transform.position = Vector2.Lerp(pos, _beeHive.position, _speed * Time.fixedDeltaTime);
                if (Vector2.Distance(pos, _beeHive.position) < 0.1f)
                {
                    // nếu ở vị trí tổ thì chuyển sang trạng thái Done
                    _beeState = BeeState.Done;
                }

                break;

        }

    }

    public void TransferHoneyToHive()
    {
        if (_beeState == BeeState.Done)
        {
            _beeHive.GetComponent<BeeHive>().TakeFloralHoneyFromBee(_honeyCollected);
            // reset lại mật ong đã lấy
            _honeyCollected = 0;
            _beeState = BeeState.MovingToFlower;
            _isTakingHoney = false;
            _isAtFlowerPosition = false;
        }
    }

}