using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] slotList;
    private ResourceManager _rM;


    private void Start()
    {
        _rM = ResourceManager.Instance;
        VisualItemInInventory();
    }

    private void VisualItemInInventory()
    {
        // if (_rM == null) _rM = ResourceManager.Instance;


        // Nếu không có item nào thì thoát
        if (_rM.GetGameObjectList().Count <= 0) return;

        // foreach (GameObject itemGo in _rM.GetGameObjectList())
        // {
        //     var item = itemGo.GetComponent<Item>();

        //     var slotIndexImage = slotList[index].transform.GetChild(0).GetComponent<Image>();
        //     var slotIndexText = slotList[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>();


        //     if (item.amount > 0)
        //     {
        //         // Nếu số lượng item > 0 thì hiển thị item đó
        //         index += 1;
        //         slotIndexImage.sprite = itemGo.GetComponent<SpriteRenderer>().sprite;
        //         slotIndexText.text = item.amount.ToString();

        //         slotIndexImage.gameObject.SetActive(true);
        //         slotIndexText.gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         slotIndexImage.gameObject.SetActive(false);
        //         slotIndexText.gameObject.SetActive(false);
        //     }
        // }
        UpdateInventory();
    }
    void UpdateSlot(int slotIndex, Sprite sprite, string amount, bool isActive)
    {
        var slot = slotList[slotIndex];
        var slotImage = slot.transform.GetChild(0).GetComponent<Image>();
        var slotText = slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        slotImage.sprite = sprite;
        slotText.text = amount;

        slotImage.gameObject.SetActive(isActive);
        slotText.gameObject.SetActive(isActive);
    }

    void UpdateInventory()
    {
        int slotIndex = 0;
        var gameObjects = _rM.GetGameObjectList();

        for (int i = 0; i < slotList.Length; i++)
        {
            if (i < gameObjects.Count)
            {
                var item = gameObjects[i].GetComponent<Item>();
                if (item.amount > 0)
                {
                    var sprite = gameObjects[i].GetComponent<SpriteRenderer>().sprite;
                    UpdateSlot(slotIndex, sprite, item.amount.ToString(), true);
                    slotIndex += 1;
                }
                else
                {
                    UpdateSlot(slotIndex, null, null, false);
                }
            }
            else
            {
                UpdateSlot(slotIndex, null, null, false);
            }
        }
    }
    private void OnEnable()
    {
        ResourceManager.OnResourceChanged += OnNotify;
    }

    private void OnDisable()
    {
        ResourceManager.OnResourceChanged -= OnNotify;
    }

    public void OnNotify()
    {
        // Khi có sự thay đổi từ ResourceManager thì cập nhật lại giao diện
        VisualItemInInventory();
    }
}