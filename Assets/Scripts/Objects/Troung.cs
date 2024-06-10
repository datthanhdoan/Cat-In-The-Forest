using System;
using TMPro;
using UnityEngine;

public class Troung : MonoBehaviour
{
    public static event Action<State> OnTroughStateChange;
    private int _troughCapacity = 100;
    private int _currentFill = 100;
    private int _amountToFull = 0;
    private ResourceManager _rM;
    private State _state = State.HasRice;
    [SerializeField] private TextMeshProUGUI _amountText;
    public enum State
    {
        Empty,
        HasRice
    }
    private void Start()
    {
        _rM = ResourceManager.Instance;
        UpdateAmountToFull();
    }


    private void UpdateAmountToFull()
    {
        int riceAmout = _rM.GetAmountOfItem(ItemType.Rice);
        _amountToFull = _troughCapacity - _currentFill;
        _amountText.text = (_troughCapacity - _amountToFull).ToString() + "/" + _troughCapacity;
    }

    // ON CLICK
    public void FillTrough()
    {
        int riceAmout = _rM.GetAmountOfItem(ItemType.Rice);

        if (riceAmout > 0)
        {
            if (riceAmout <= _amountToFull)
            {
                _currentFill = _currentFill + riceAmout;
                _rM.SetAmoutItem(ItemType.Rice, 0);
            }
            else
            {
                _currentFill = _currentFill + _amountToFull;
                _rM.SetAmoutItem(ItemType.Rice, riceAmout - _amountToFull);
            }
            UpdateAmountToFull();
            _state = State.HasRice;
        }
        OnTroughStateChange?.Invoke(_state);
    }

    public void TakeRice(int amount)
    {
        _currentFill -= amount;
        UpdateAmountToFull();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateAmountToFull();
        }
    }
}