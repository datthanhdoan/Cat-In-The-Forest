using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class BeeHive : MonoBehaviour, IObserver
{

    public int _floralHoneyRequired { get; private set; } = 10;
    public int _floralHoneyCollected { get; private set; } = 0;
    public float _timerToSpawnBee { get; private set; } = 4f;
    private HoneyState _honeyState = HoneyState.NotEnoughHoney;
    private ResourceManager _rM;
    private ItemPopup _itemPopup;
    [SerializeField] private Transform _flower;
    [SerializeField] private Transform _bee;
    [SerializeField] private ItemPopupSpawner _itemPopupSpawner;


    public enum HoneyState
    {
        NotEnoughHoney,
        EnoughHoney
    }
    private void Start()
    {
        _rM = ResourceManager.Instance;
        float randomTime = Random.Range(5, 10);
        if (!HasEnoughFloralHoney())
        {
            StartCoroutine(ActiveBee(randomTime));
        }
    }

    public void TakeFloralHoneyFromBee(int amount)
    {
        _floralHoneyCollected += amount;
        _bee.gameObject.SetActive(false);

        if (HasEnoughFloralHoney())
        {
            _honeyState = HoneyState.EnoughHoney;
            SpawnItemPopup();
        }
        else
        {
            _honeyState = HoneyState.NotEnoughHoney;
            StartCoroutine(ActiveBee(_timerToSpawnBee));
        }
    }

    private void SpawnItemPopup()
    {
        _itemPopup = _itemPopupSpawner._pool.Get();
        _itemPopup.AddObserver(this);
        _itemPopup.SetTransformParent(this.transform);

        int amount = 1;
        _itemPopup.SetItem(ItemType.Honey, amount);
        Debug.Log("Call SpawnItemPopup");
    }
    IEnumerator ActiveBee(float second)
    {
        yield return new WaitForSeconds(second);
        _bee.gameObject.SetActive(true);
        yield return null;
    }

    private bool HasEnoughFloralHoney()
    {
        return _floralHoneyCollected >= _floralHoneyRequired;
    }

    public void OnNotify()
    {
        // when the player clicks take honey
        if (_honeyState == HoneyState.EnoughHoney)
        {
            _rM.SetAmoutItem(ItemType.Honey, _rM.GetAmountOfItem(ItemType.Honey) + 1);
            _floralHoneyCollected = 0;
            _honeyState = HoneyState.NotEnoughHoney;
            StartCoroutine(ActiveBee(_timerToSpawnBee));
        }
    }


}