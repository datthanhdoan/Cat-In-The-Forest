using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class T_Apple : Task
{
    int _appleRequire = 0;
    int _coinReward = 0;
    void Start()
    {
        UpdateAppleRequire(0); // baseRequire = 0
        UpdateCoinReward(_pm.level, 10, 0.3f);

        string t1 = _appleRequire.ToString();
        string t2 = _coinReward.ToString();
        UpdateTaskContent(t1, t2);
    }

    public new void CheckTask()
    {
        if (_pm.apple >= _appleRequire)
        {
            _pm.UpdateApple(-_appleRequire); // minus apple
            _pm.SetCoin(_coinReward); // plus coin

            UpdateAppleRequire(10); // baseRequire
            UpdateCoinReward(_pm.level, 10, 0.3f);

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
        _appleRequire = baseRequire + _pm.level * 2 + Random.Range(0, 3);
    }

    public void UpdateCoinReward(int level, float a, float b)
    {
        _coinReward = (int)(a * Math.Exp(b * level));
    }
}
