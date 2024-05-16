using UnityEngine;
using UnityEngine.Pool;
public class ItemPopupSpawner : MonoBehaviour
{
    public int defaultAmount;
    public int maxSize;
    public ObjectPool<ItemPopup> _pool { get; private set; }
    [SerializeField] private ItemPopup _itemPopupPrefab;
    private void Start()
    {
        _pool = new ObjectPool<ItemPopup>(CreateItemPopup, GetItemPopup, ReturnItemPopup, DestroyItem, false, defaultAmount, maxSize);
    }

    private ItemPopup CreateItemPopup()
    {
        ItemPopup itemPopup = Instantiate(_itemPopupPrefab, transform.position, Quaternion.identity, transform);
        itemPopup.SetPool(_pool);
        return itemPopup;
    }

    private void GetItemPopup(ItemPopup itemPopup)
    {
        itemPopup.gameObject.SetActive(true);
    }

    private void ReturnItemPopup(ItemPopup itemPopup)
    {
        itemPopup.gameObject.SetActive(false);
    }

    private void DestroyItem(ItemPopup itemPopup)
    {
        Destroy(itemPopup.gameObject);
    }
}