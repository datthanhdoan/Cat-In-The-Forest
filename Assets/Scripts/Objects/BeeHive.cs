using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class BeeHive : MonoBehaviour
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
        var honeyAmount = _rM.GetAmountOfItem(ItemType.Honey);
        if (_itemPopup == null)
        {
            _itemPopup = _itemPopupSpawner._pool.Get();
            _itemPopup.SetTransformParent(this.transform);
            _itemPopup.SetItem(ItemType.Honey, honeyAmount + 1);
        }
        else if (_itemPopup != null)
        {
            _itemPopup.OnClick();
            _itemPopup = null;

            _itemPopup = _itemPopupSpawner._pool.Get();
            _itemPopup.SetTransformParent(transform);
            _itemPopup.SetItem(ItemType.Honey, honeyAmount + 1);
        }
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


}