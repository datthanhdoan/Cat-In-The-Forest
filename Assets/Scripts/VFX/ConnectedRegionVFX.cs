using UnityEngine;

public class ConnectedRegionVFX : MonoBehaviour
{
    [SerializeField] private ConnectedRegion _connectedRegion;
    private ResourceManager _rM;
    private void Awake()
    {
        _rM = ResourceManager.Instance;
        if (_rM == null)
        {
            Debug.LogError("ResourceManager is null");
        }
        else
        {
            Debug.Log("ConnectedRegionVFX Start");
        }
    }

    private void CheckOpenConnections()
    {
        int coinRequire = _connectedRegion.GetCoinRequire();

        // if enough coin, show the connected region
        if (coinRequire <= _rM.GetCoin())
        {
            _connectedRegion.gameObject.SetActive(true);
        }
        else
        {
            _connectedRegion.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ResourceManager.OnResourceChanged += CheckOpenConnections;
    }

    private void OnDisable()
    {
        ResourceManager.OnResourceChanged -= CheckOpenConnections;
    }
}