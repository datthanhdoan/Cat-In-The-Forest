using UnityEngine;

public class T_Level : Task
{
    int _coinRequire = 0;
    GameObject region;

    new void Start()
    {
        base.Start();
        string t1 = "Unlock level " + (_pm.level + 1) + " : " + _coinRequire + " coin";
        string t2 = "Upgrade";
        UpdateTaskContent(t1, t2);
        UpdateCoinRequire();
        region = _pm.region;
    }

    public new void CheckTask()
    {
        if (_pm.coin >= _coinRequire)
        {
            // _button.interactable = true;
            if (!_pm.CheckMaxLevel())
            {
                _pm.UpdateLevel();
                UpdateCoinRequire();

                // Update task content
                string t1 = "Unlock level " + (_pm.level + 1) + " : " + _coinRequire + " coin";
                string t2 = "Upgrade";
                UpdateTaskContent(t1, t2);


                // update map
                region.transform.GetChild(_pm.level - 1).gameObject.SetActive(true);
                _map.UpdateNavMesh();

                // update coin
                _pm.UpdateCoin(-_coinRequire);
            }
            if (_pm.CheckMaxLevel())
            {
                UpdateTaskContent("Congratulations! You unlocked all level ", "");
                // change color of button when task is completed
                _bImage.color = new Color(64f / 255f, 64f / 255f, 64f / 255f, 0.5f);
            }
        }
    }

    void UpdateCoinRequire()
    {
        _coinRequire = _coinRequire + Random.Range(2, 3) * _pm.level;
    }
}