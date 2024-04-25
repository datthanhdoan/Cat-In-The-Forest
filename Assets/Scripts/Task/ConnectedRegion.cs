using UnityEngine;

/// <summary>
/// Dùng huy hiệu chân mèo để mở map mới
/// </summary>
public class ConnectedRegion : Task
{
    [SerializeField] private int _coinRequire;
    [Tooltip("Level to unlock - R_Region")]
    [SerializeField] private int unlockLevel;

    protected override void Start()
    {
        base.Start();
        string t1 = "Locked";
        string t2 = _coinRequire.ToString();
        UpdateTaskContent(t1, t2);
    }

    public int GetCoinRequire()
    {
        return _coinRequire;
    }
    protected override void CheckTask()
    {
        Debug.Log("Call Check Task");
        if (_rM.GetCoin() >= _coinRequire)
        {
            // _button.interactable = true;
            if (!_mapManager.CheckMaxLevel())
            {
                int coinAfter = _rM.GetCoin() - _coinRequire;
                _rM.SetCoin(coinAfter);

                // Update Map
                int currentLevel = _mapManager.GetLevel();
                _mapManager.Setlevel(currentLevel + 1);

                // update map
                _mapManager.region.transform.GetChild(unlockLevel - 1).gameObject.SetActive(true);
                _mapManager.UpdateNavMesh();
                gameObject.SetActive(false);
            }
        }
    }

}