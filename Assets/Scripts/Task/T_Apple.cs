using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class T_Apple : Task
{
    int _appleRequire = 0;
    int _coinReward = 0;
    Resource apple;
    protected new void Start()
    {
        base.Start();
        UpdateAppleRequire(0); // baseRequire = 0
        UpdateCoinReward(_map.level, 10, 0.3f);

        string t1 = _appleRequire.ToString();
        string t2 = _coinReward.ToString();
        UpdateTaskContent(t1, t2);
        apple = _resource.resources.Find(x => x.name == ResourceName.Apple);
    }

    public new void CheckTask()
    {
        if (apple.quantity >= _appleRequire)
        {

            apple.SetQuantity(apple.quantity - _appleRequire); // minus apple
            _resource.SetCoin(_resource.coin + _coinReward); // plus coin

            UpdateAppleRequire(10); // baseRequire
            UpdateCoinReward(_map.level, 10, 0.3f);

            string t1 = _appleRequire.ToString();
            string t2 = _coinReward.ToString();
            UpdateTaskContent(t1, t2);
        }
        else
        {
            Debug.Log("Not enough apple");
        }
    }

    void UpdateAppleRequire(int baseRequire)
    {
        _appleRequire = baseRequire + _map.level * 2 + Random.Range(0, 3);
    }

    public void UpdateCoinReward(int level, float a, float b)
    {
        _coinReward = (int)(a * Math.Exp(b * level));
    }
}
