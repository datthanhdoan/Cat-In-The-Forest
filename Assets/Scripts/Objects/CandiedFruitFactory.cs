using System;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CandiedFruitFactory : MonoBehaviour, IInteractive
{
    public int fruitRequired = 1;
    public int woodRequired = 1;
    private readonly float _timeToCook = 12f; // default 12 seconds
    private float _timer = 0;
    public ItemType itemTypeInput; // select in Unity inspector
    public ItemType itemTypeResult; // select in Unity inspector
    private Item itemInput, wood, itemResult;
    private ResourceManager _rM;
    private Player _player;


    [NonSerialized] public bool _allConditions = false;
    [NonSerialized] public bool playerInRange = false;

    [NonSerialized] public bool _isCooking = false;


    [Header("Text")]
    [SerializeField] TextMeshProUGUI _itemInputText, _woodText, _itemResultText;
    [Header("Image")]
    [SerializeField] Image _itemInputImage, _woodImage, _itemResultImage;
    [Header("Button Image")]
    [SerializeField] Sprite[] _imageForButton;
    [SerializeField] Image _buttonImage;

    [Header("Slider")]
    [SerializeField] Slider _slider;


    [SerializeField] CanvasGroup _canvasGroup;

    [SerializeField] private ItemPopup _itemPopup;


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

        _itemPopup.gameObject.SetActive(false);
        // Set the button image
        _buttonImage.sprite = _imageForButton[0]; // 0 means X image
        Debug.Log("CandiedFruitFactory Start - Item Popup GO : " + _itemPopup);
    }

    private void Update()
    {
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
            if (_itemPopup.gameObject.activeSelf == false)
            {
                _itemPopup.gameObject.SetActive(true);
                _itemPopup.SetTransformParentAndShow(this.transform);
                _itemPopup.SetItem(itemTypeResult, 1);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            other.GetComponent<Player>().SetInteractiveObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (other.GetComponent<Player>().GetInteractiveObject().Equals(this))
            {
                other.GetComponent<Player>().SetInteractiveObject(null);
            }
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

            if (_itemPopup.gameObject.activeSelf == true)
            {
                _itemPopup.OnClick();
            }
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

    public void OnInteractive()
    {
        if (_itemPopup != null && _itemPopup.gameObject.activeSelf == true)
        {
            _itemPopup.OnClick();
        }
    }

    public void OnButtonInteractive()
    {
        OnClickedButton();
        if (TryGetComponent(out CandiedFactoryVFX candiedFactoryVFX))
        {
            candiedFactoryVFX.AnimButton();
        }
    }
}