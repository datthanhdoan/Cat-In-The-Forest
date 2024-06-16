using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class BeeHive : MonoBehaviour, IObserver, IInteractive
{

    public int _floralHoneyRequired { get; private set; } = 10;
    public int _floralHoneyCollected { get; private set; } = 0;
    public float _timerToSpawnBee { get; private set; } = 4f;
    private HoneyState _honeyState = HoneyState.NotEnoughHoney;
    private ResourceManager _rM;
    [SerializeField] private ItemPopup _itemPopup;
    [SerializeField] private Transform _flower;
    [SerializeField] private Transform _bee;


    public enum HoneyState
    {
        NotEnoughHoney,
        EnoughHoney
    }
    private void Start()
    {
        _itemPopup.gameObject.SetActive(false);

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
            ActiveItemPopup();
        }
        else
        {
            _honeyState = HoneyState.NotEnoughHoney;
            StartCoroutine(ActiveBee(_timerToSpawnBee));
        }
    }

    private void ActiveItemPopup()
    {
        _itemPopup.gameObject.SetActive(true);
        _itemPopup.AddObserver(this);
        _itemPopup.SetTransformParentAndShow(this.transform);

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
            _floralHoneyCollected = 0;
            _honeyState = HoneyState.NotEnoughHoney;
            StartCoroutine(ActiveBee(_timerToSpawnBee));
        }
    }

    public void OnInteractive()
    {
        if (_honeyState == HoneyState.EnoughHoney)
        {
            _itemPopup.OnClick();
        }
    }

    public void OnButtonInteractive() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetInteractiveObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().GetInteractiveObject().Equals(this))
            {
                other.GetComponent<Player>().SetInteractiveObject(null);
            }
        }
    }
}