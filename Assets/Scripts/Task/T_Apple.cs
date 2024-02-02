using System;
using UnityEngine;

public class T_Apple : Task
{
    int _appleRequire = 0;
    int _coinReward = 0;
    GameManagerment _gm;
    new void Start()
    {
        base.Start();
        _gm = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        UpdateAppleRequire();
        string t1 = "Unlock level " + (_gm.currentLevel + 1);
        string t2 = _appleRequire.ToString() + " apple ";
        UpdateTaskContent(t1, t2);
    }

    void Update()
    {

    }

    public new void CheckTask()
    {
        if (_pm.apple >= _appleRequire)
        {
            // _button.interactable = true;

        }
        else
        {
            Debug.Log("Not enough apple");
        }
    }
    void UpdateAppleRequire()
    {
        _appleRequire = _pm.apple + _gm.currentLevel * 2 + 1;
    }

    public void UpdateCoinReward(int level, float a, float b)
    {
        _coinReward = (int)(a * Math.Exp(b * level));
    }
}