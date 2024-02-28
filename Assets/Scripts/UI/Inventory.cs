using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] ResourceSO _resourceSO;
    [SerializeField] Transform _slotParent;
    PoolingObject _poolingObject;
    // [SerializeField] List<GameObject> _slots = new List<GameObject>();
    Dictionary<ResourceName, GameObject> _slotDict = new Dictionary<ResourceName, GameObject>();
    private void Start()
    {
        _poolingObject = GetComponent<PoolingObject>();
        foreach (var resource in _resourceSO.resources)
        {
            if (resource.quantity > 0)
            {
                GameObject slot = SpawnSlot();
                var slot_transform = slot.transform;
                slot_transform.localScale = Vector3.one;

                var slot_image = slot_transform.GetChild(0).GetComponent<Image>();
                var slot_text = slot_transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                slot_transform.SetParent(_slotParent);
                slot_transform.position = Vector3.zero;
                slot_image.sprite = resource.sprite;
                slot_text.text = resource.quantity.ToString();

                _slotDict.Add(resource.name, slot);
            }
        }
        // UpdateResource();
    }

    public void UpdateResource()
    {
        foreach (var resource in _resourceSO.resources)
        {


            if (_slotDict.ContainsKey(resource.name))
            {
                /// local variable to store the slot
                var slot = _slotDict[resource.name];
                var slot_transform = slot.transform;

                var slot_image = slot_transform.GetChild(0).GetComponent<Image>();
                var slot_text = slot_transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                /// ------------------------------

                slot_text.text = resource.quantity.ToString();
                if (resource.quantity == 0)
                {
                    // set the slot to inactive
                    slot.SetActive(false);
                    _poolingObject.ReturnToPool(_slotDict[resource.name]);
                    // remove the slot from the dictionary
                    _slotDict.Remove(resource.name);
                }
            }
            else
            {
                if (resource.quantity > 0)
                {
                    /// spawn a new slot
                    GameObject slotSpawn = SpawnSlot();
                    var slotSpawn_transform = slotSpawn.transform;
                    var slotSpawn_image = slotSpawn_transform.GetChild(0).GetComponent<Image>();
                    var slotSpawn_text = slotSpawn_transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    ///

                    slotSpawn_transform.SetParent(_slotParent);
                    slotSpawn_transform.position = Vector3.zero;
                    slotSpawn_transform.localScale = Vector3.one;

                    slotSpawn_image.sprite = resource.sprite;
                    slotSpawn_text.text = resource.quantity.ToString();
                    _slotDict.Add(resource.name, slotSpawn);

                }

            }
        }
    }

    GameObject SpawnSlot()
    {
        GameObject slot = _poolingObject.GetPooledObject();
        slot.SetActive(true);
        slot.transform.SetParent(transform);
        // slot.transform.localScale = Vector3.one;
        return slot;
    }


    private void OnEnable()
    {
        Tree.OnResourceChanged += UpdateResource;
        Task.OnResourceChanged += UpdateResource;
    }

    private void OnDisable()
    {
        Tree.OnResourceChanged -= UpdateResource;
        Task.OnResourceChanged -= UpdateResource;
    }
}