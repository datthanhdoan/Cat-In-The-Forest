using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private ResourceManager _rM;
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _diamondText;


    public void UpdateUI()
    {
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
