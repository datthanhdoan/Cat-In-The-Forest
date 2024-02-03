using UnityEngine;

public class T_Level : Task
{
    [SerializeField] int _coinRequire;
    [SerializeField] int unlockLevel;
    GameObject region;

    new void Start()
    {
        base.Start();
        string t1 = "Locked";
        string t2 = _coinRequire.ToString();
        UpdateTaskContent(t1, t2);
        region = _pm.region;
    }

    protected override void CheckTask()
    {
        Debug.Log("CheckTask");
        if (_pm.coin >= _coinRequire)
        {
            // _button.interactable = true;
            if (!_pm.CheckMaxLevel())
            {
                _pm.UpdateCoin(-_coinRequire);

                _pm.UpdateLevel();

                // update map
                region.transform.GetChild(unlockLevel - 1).gameObject.SetActive(true);
                _map.UpdateNavMesh();
                gameObject.SetActive(false);
            }
        }
    }

}