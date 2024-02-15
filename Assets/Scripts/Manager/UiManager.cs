using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] ProgressManager _pm;
    [SerializeField] GameObject _coinGO;
    [SerializeField] GameObject _appleGO;
    void Start()
    {
        _pm = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();
    }
    void Update()
    {
        UpdateCoin();
        UpdateApple();
    }

    void UpdateCoin()
    {
        string coin = _pm.coin.ToString();
        _coinGO.GetComponent<Text>().text = "Coin : " + coin;
    }

    void UpdateApple()
    {
        string apple = _pm.apple.ToString();
        _appleGO.GetComponent<Text>().text = "Apple: " + apple;
    }
}
