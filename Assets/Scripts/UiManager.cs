using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] ResourceSO _resourceSO;
    [SerializeField] Text _coinText;
    // [SerializeField] Text _appleText;
    void Update()
    {
        UpdateCoin();
        // UpdateApple();
    }

    void UpdateCoin()
    {
        string coin = _resourceSO.coin.ToString();
        _coinText.text = "Coin : " + coin;
    }
}
