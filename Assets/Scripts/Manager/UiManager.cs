using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] ResourceManager _resource;
    [SerializeField] Text _coinText;
    // [SerializeField] Text _appleText;
    void Start()
    {
        _resource = ResourceManager.instance;
    }
    void Update()
    {
        UpdateCoin();
        // UpdateApple();
    }

    void UpdateCoin()
    {
        string coin = _resource.coin.ToString();
        _coinText.text = "Coin : " + coin;
    }

    // void UpdateApple()
    // {
    //     var appleQuantity = _resource.GetResource(ResourceManager.ResourceName.Apple).quantity;
    //     _appleText.text = "Apple: " + appleQuantity.ToString();
    // }
}
