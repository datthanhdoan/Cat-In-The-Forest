using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class BeeHive : MonoBehaviour
{
    [SerializeField] private Transform _flower;
    [SerializeField] private Transform _bee;
    public int _floralHoneyRequired { get; private set; } = 10;
    public int _floralHoneyCollected { get; private set; } = 0;
    public float _timerToSpawnBee { get; private set; } = 4f;


    private void Start()
    {
        float randomTime = Random.Range(5, 10);
        StartCoroutine(ActiveBee(randomTime));
    }

    public void TakeFloralHoneyFromBee(int amount)
    {
        _floralHoneyCollected += amount;
        Debug.Log("Floral Honey Collected: " + _floralHoneyCollected);
        _bee.gameObject.SetActive(false);
        StartCoroutine(ActiveBee(_timerToSpawnBee));
    }

    IEnumerator ActiveBee(float second)
    {
        yield return new WaitForSeconds(second);
        if (_floralHoneyCollected <= _floralHoneyRequired)
        {
            _bee.gameObject.SetActive(true);
        }
        yield return null;
    }


}