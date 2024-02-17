using UnityEngine;

public class T_Level : Task
{
    [SerializeField] int _coinRequire;
    [SerializeField] int unlockLevel;

    void Start()
    {
        string t1 = "Locked";
        string t2 = _coinRequire.ToString();
        UpdateTaskContent(t1, t2);
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
                _pm.region.transform.GetChild(unlockLevel - 1).gameObject.SetActive(true);
                _map.UpdateNavMesh();
                gameObject.SetActive(false);
            }
        }
    }

}