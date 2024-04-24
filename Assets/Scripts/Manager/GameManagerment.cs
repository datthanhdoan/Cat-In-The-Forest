using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
/// <summary>
/// Quản lý game
/// </summary>
using UnityEngine;

public class GameManagerment : MonoBehaviour
{

    public bool CheckFirstTimeInGame()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGame") == 1)
        {
            return true;
        }
        else
        {
            return true;
        }
    }


}
