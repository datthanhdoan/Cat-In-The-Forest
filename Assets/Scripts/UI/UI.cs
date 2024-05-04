using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private ResourceManager _rM;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _diamondText;


    public void UpdateUI()
    {
        UpdateCoin();
        UpdateDiamond();
    }

    private void UpdateCoin()
    {
        string coin = _rM.GetCoin().ToString();
        _coinText.text = coin;
    }

    private void UpdateDiamond()
    {
        string diamond = _rM.GetDiamond().ToString();
        _diamondText.text = diamond;
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
