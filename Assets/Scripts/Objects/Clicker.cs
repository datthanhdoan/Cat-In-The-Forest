using UnityEngine;

public class Clicker : MonoBehaviour
{
    protected bool _hasBeenClicked = false;
    protected virtual void OnMouseUpAsButton()
    {
        _hasBeenClicked = true;
    }

    protected virtual void Update()
    {
        if (_hasBeenClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hasBeenClicked = false;
            }
            else
            {
                // method to be called when clicked to do something
                HandleClick();
            }
        }
    }
    protected virtual void HandleClick()
    {
    }

}