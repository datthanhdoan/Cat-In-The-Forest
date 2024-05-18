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
        if (HasEnoughCoins())
        {
            // Check if the level to unlock is not greater than the maximum level
            if (IsUnlockLevelValid())
            {
                DeductCoins();

                // Update level if the current level is less than the maximum level
                UpdateLevel();

                // Update map
                UnlockRegionInMap();

                // Update the navigation mesh
                _mapManager.UpdateNavMesh();

                // Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Map is still in progress");
            }
        }


    }
    private bool HasEnoughCoins()
    {
        return _rM.GetCoin() >= _coinRequire;
    }

    private bool IsUnlockLevelValid()
    {
        return unlockLevel <= _mapManager.GetMaxLevel();
    }

    private void DeductCoins()
    {
        int coinAfter = _rM.GetCoin() - _coinRequire;
        _rM.SetCoin(coinAfter);
    }

    private void UpdateLevel()
    {
        int currentLevel = _mapManager.GetLevel();
        if (currentLevel < _mapManager.GetMaxLevel())
        {
            _mapManager.Setlevel(currentLevel + 1);
        }
    }

    private void UnlockRegionInMap()
    {
        _mapManager.region.transform.GetChild(unlockLevel - 1).gameObject.SetActive(true);
    }
}