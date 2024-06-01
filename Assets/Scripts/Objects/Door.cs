using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

}
