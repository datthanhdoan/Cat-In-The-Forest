using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] ResourceManager _resource;
    [SerializeField] GameObject _coinGO;
    [SerializeField] GameObject _appleGO;
    void Start()
    {
        _resource = ResourceManager.instance;
    }
    void Update()
    {
        UpdateCoin();
        UpdateApple();
    }

    void UpdateCoin()
    {
        string coin = _resource.coin.ToString();
        _coinGO.GetComponent<Text>().text = "Coin : " + coin;
    }

    void UpdateApple()
    {
        var appleQuantity = _resource.GetResource(ResourceManager.ResourceName.Apple).quantity;
        _appleGO.GetComponent<Text>().text = "Apple: " + appleQuantity.ToString();
    }
}
