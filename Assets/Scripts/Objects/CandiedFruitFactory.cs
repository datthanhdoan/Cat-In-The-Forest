using System;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CandiedFruitFactory : MonoBehaviour
{
    private ResourceManager _rM;
    private Player _player;
    // Select item type in Unity Editor`
    public ItemType itemTypeInput;
    public ItemType itemTypeResult;

    // set item-type to item
    private Item itemInput, wood, itemResult;




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

    public int fruitRequired = 1;
    public int woodRequired = 1;

    float _timeToCook = 12f;
    float _timer = 0;
    [NonSerialized] public bool _isCooking = false;
    [SerializeField] CanvasGroup _canvasGroup;




    private void Start()
    {
        _player = Player.Instance;
        _rM = ResourceManager.Instance;

        itemInput = _rM.GetItem(itemTypeInput);
        wood = _rM.GetItem(ItemType.Wood);
        itemResult = _rM.GetItem(itemTypeResult);

        _itemInputImage.sprite = itemInput.gameObject.GetComponent<SpriteRenderer>().sprite;
        _woodImage.sprite = wood.gameObject.GetComponent<SpriteRenderer>().sprite;
        _itemResultImage.sprite = itemResult.gameObject.GetComponent<SpriteRenderer>().sprite;

        _itemInputText.text = fruitRequired.ToString();
        _woodText.text = woodRequired.ToString();
        _itemResultText.text = "1";

        _slider.maxValue = _timeToCook;
        _slider.value = 0;
        _slider.gameObject.SetActive(false);

        _buttonImage.sprite = _imageForButton[0];
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
            _canvasGroup.interactable = false;

            _slider.gameObject.SetActive(true);
            _timer += Time.deltaTime;
            _slider.value = _timer;

            // if cooking is done
            if (_timer >= _timeToCook)
            {
                // Can interact with the factory again
                _canvasGroup.interactable = true;

                // update the amount of the item
                _rM.SetAmoutItem(itemTypeResult, itemResult.amount + 1);

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