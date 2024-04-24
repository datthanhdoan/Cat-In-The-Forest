using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IObserver
{
    [SerializeField] private Subject _subject;
    [SerializeField] private GameObject[] slotList;
    private ResourceManager _rM;


    private void Start()
    {
        _rM = ResourceManager.Instance;
        VisualItemInInventory();
    }

    private void VisualItemInInventory()
    {
        int index = 0;

        // Nếu không có item nào thì thoát
        if (_rM.GetItemList().Count <= 0) return;

        foreach (GameObject itemGo in _rM.GetGameObjectList())
        {
            var item = itemGo.GetComponent<Item>();

            var slotIndexImage = slotList[index].transform.GetChild(0).GetComponent<Image>();
            var slotIndexText = slotList[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>();


            if (item.amount > 0)
            {
                // Nếu số lượng item > 0 thì hiển thị item đó
                index += 1;
                slotIndexImage.sprite = itemGo.GetComponent<SpriteRenderer>().sprite;
                slotIndexText.text = item.amount.ToString();

                slotIndexImage.gameObject.SetActive(true);
                slotIndexText.gameObject.SetActive(true);
            }
            else
            {
                slotIndexImage.gameObject.SetActive(false);
                slotIndexText.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        _subject.AddObserver(this);
    }

    private void OnDisable()
    {
        _subject.RemoveObserver(this);
    }

    public void OnNotify()
    {
        // Khi có sự thay đổi từ ResourceManager thì cập nhật lại giao diện
        VisualItemInInventory();
    }
}