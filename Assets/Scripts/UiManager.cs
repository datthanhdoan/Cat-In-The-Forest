using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] TaskManagerment _taskManagerment;
    [SerializeField] GameObject _coinGO;
    [SerializeField] GameObject _fruitGO;
    void Start()
    {
        _taskManagerment = GameObject.Find("TaskManager").GetComponent<TaskManagerment>();
    }
    void Update()
    {
        UpdateCoin();
        UpdateFruit();
    }

    void UpdateCoin()
    {
        string coin = _taskManagerment.coin.ToString();
        _coinGO.GetComponent<Text>().text = "Coin : " + coin;
    }

    void UpdateFruit()
    {
        string fruit = _taskManagerment.quantityOfFruit.ToString();
        _fruitGO.GetComponent<Text>().text = "Fruit : " + fruit + "/" + _taskManagerment.quantityOfFruitRequire.ToString();
    }
}
