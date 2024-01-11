using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    private bool hasBeenClicked = false;
    [SerializeField] GameObject fruit;
    void Update()
    {

    }

    public void OnMouseDown()
    {
        hasBeenClicked = fruit.activeSelf ? true : false;
    }

}
