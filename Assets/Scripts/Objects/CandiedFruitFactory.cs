using System;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CandiedFruitFactory : MonoBehaviour
{
    public int fruitRequired = 1;
    public int woodRequired = 1;
    private float _timeToCook = 12f;
    private float _timer = 0;
    public ItemType itemTypeInput; // select in Unity inspector
    public ItemType itemTypeResult; // select in Unity inspector
    private Item itemInput, wood, itemResult;
    private ItemPopup _itemPopup;
    private ResourceManager _rM;
    private Player _player;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI _itemInputText, _woodText, _itemResultText;
    [Header("Image")]
    [SerializeField] Image _itemInputImage, _woodImage, _itemResultImage;
    [Header("Button Image")]
    [SerializeField] Sprite[] _imageForButton;
    [SerializeField] Image _buttonImage;

    [Header("Slider")]
    [SerializeField] Slider _slider;

    [NonSerialized] public bool _allConditions = false;
    [NonSerialized] public bool playerInRange = false;

    [NonSerialized] public bool _isCooking = false;
    [SerializeField] CanvasGroup _canvasGroup;

    [SerializeField] private ItemPopupSpawner _itemPopupSpawner;


    private void Start()
    {
        _player = Player.Instance;
        _rM = ResourceManager.Instance;

        // Get the item from the resource manager by the item type
        itemInput = _rM.GetItem(itemTypeInput);
        wood = _rM.GetItem(ItemType.Wood);
        itemResult = _rM.GetItem(itemTypeResult);

        // Set the image
        _itemInputImage.sprite = itemInput.gameObject.GetComponent<SpriteRenderer>().sprite;
        _woodImage.sprite = wood.gameObject.GetComponent<SpriteRenderer>().sprite;
        _itemResultImage.sprite = itemResult.gameObject.GetComponent<SpriteRenderer>().sprite;

        // Set the text
        _itemInputText.text = fruitRequired.ToString();
        _woodText.text = woodRequired.ToString();
        _itemResultText.text = "1";

        // Set the slider
        _slider.maxValue = _timeToCook;
        _slider.value = 0;
        _slider.gameObject.SetActive(false);

        // Set the button image
        _buttonImage.sprite = _imageForButton[0]; // 0 means X image
    }

    private void Update()
    {
        CheckDistance();
        if (playerInRange)
        {
            CheckConditions();
        }
        if (_isCooking)
        {
            HandelCooking();
        }
    }

    private void HandelCooking()
    {

        _canvasGroup.interactable = false;

        _slider.gameObject.SetActive(true);
        _timer += Time.deltaTime;
        _slider.value = _timer;

        // if cooking is done
        if (_timer >= _timeToCook)
        {
            // Can interact with the factory again
            _canvasGroup.interactable = true;

            // spawn the item popup
            if (_itemPopup == null)
            {
                _itemPopup = _itemPopupSpawner._pool.Get();
                _itemPopup.SetItem(itemTypeResult, itemResult.amount + 1);
                _itemPopup.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            else
            {
                var amount = _itemPopup._amount;
                _itemPopup.SetItem(itemTypeResult, amount + 1);
            }

            // reset the timer
            _timer = 0;

            // reset the conditions
            _isCooking = false;
            _allConditions = false;

            // update the button image to the default
            _buttonImage.sprite = _imageForButton[0];

            // reset the slider
            _slider.value = 0;
            _slider.gameObject.SetActive(false);
        }
    }

    public void OnClickedButton()
    {
        // check conditions
        if (_allConditions)
        {
            _rM.SetAmoutItem(ItemType.Wood, wood.amount - woodRequired);
            _rM.SetAmoutItem(itemTypeInput, itemInput.amount - fruitRequired);

            // start cooking
            _isCooking = true;
        }
    }
    public void CheckConditions()
    {
        // check if the player has enough resources
        if (wood.amount >= woodRequired && itemInput.amount >= fruitRequired)
        {
            _allConditions = true;
            _buttonImage.sprite = _imageForButton[1];
        }
        else
        {
            _allConditions = false;
            _buttonImage.sprite = _imageForButton[0];
        }

    }
    public void CheckDistance()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < 2)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }


}