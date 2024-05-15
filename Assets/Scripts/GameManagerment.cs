using UnityEngine;
/// <summary>
/// Quản lý game
/// </summary>

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
