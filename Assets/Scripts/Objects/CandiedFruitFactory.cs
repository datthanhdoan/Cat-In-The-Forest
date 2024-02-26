using System;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CandiedFruitFactory : MonoBehaviour
{
    Player _player;
    [SerializeField] ResourceSO _resourceSO;
    public ResourceName fruitName;
    public ResourceName fruitResultName;
    Resource wood;
    Resource fruit;
    Resource fruitResult;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI _fruitText, _woodText, _fruitResultText;
    [Header("Image")]
    [SerializeField] Image _fruitImage, _woodImage, _fruitResultImage;
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
        _player = Player.instance;

        wood = _resourceSO.resources.Find(x => x.name == ResourceName.Wood);
        fruit = _resourceSO.resources.Find(x => x.name == fruitName);
        fruitResult = _resourceSO.resources.Find(x => x.name == fruitResultName);

        _fruitImage.sprite = fruit.sprite; // 
        _woodImage.sprite = wood.sprite;
        _fruitResultImage.sprite = fruitResult.sprite;

        _fruitText.text = fruitRequired.ToString();
        _woodText.text = woodRequired.ToString();
        _fruitResultText.text = "1";

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
            Debug.Log(_slider.value);
            if (_timer >= _timeToCook)
            {
                _canvasGroup.interactable = true;

                fruitResult.SetQuantity(fruitResult.quantity + 1);
                _timer = 0;

                _isCooking = false;
                _allConditions = false;

                _buttonImage.sprite = _imageForButton[0];

                _slider.value = 0;
                _slider.gameObject.SetActive(false);
            }
        }
    }

    public void OnClick()
    {
        if (_allConditions)
        {
            wood.SetQuantity(wood.quantity - woodRequired);
            fruit.SetQuantity(fruit.quantity - fruitRequired);
            _isCooking = true;
        }
    }
    public void CheckConditions()
    {
        if (wood.quantity >= woodRequired && fruit.quantity >= fruitRequired)
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