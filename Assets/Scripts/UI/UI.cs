using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private ResourceManager _rM;
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _diamondText;

    private void Awake()
    {
        _rM = ResourceManager.Instance;
        if (_rM == null)
        {
            Debug.LogError("ResourceManager is null");
        }
        else
        {
            Debug.Log("UI Start");
        }

    }


    public void UpdateUI()
    {
        // if (_rM == null) { _rM = ResourceManager.Instance; }
        UpdateCoin();
        UpdateDiamond();
    }

    private void UpdateCoin()
    {
        string coin = _rM.GetCoin().ToString();
        _coinText.text = "Coin : " + coin;
    }

    private void UpdateDiamond()
    {
        string diamond = _rM.GetDiamond().ToString();
        _diamondText.text = "Diamond : " + diamond;
    }

    public void OnNotify()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        ResourceManager.OnResourceChanged += OnNotify;
    }

    private void OnDisable()
    {
        ResourceManager.OnResourceChanged -= OnNotify;
    }


}
