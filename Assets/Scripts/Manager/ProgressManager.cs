using UnityEngine;

public class ProgressManager : MonoBehaviour
{



    // resources
    public int coin = 0;
    public int apple = 0;


    // level
    [SerializeField] public GameObject region;
    public int level { get; private set; } = 1;
    public int maxLevel { get; private set; } = 0;

    private void Start()
    {
        maxLevel = region.transform.childCount;
    }
    // set resources
    public void UpdateLevel() => level++;

    public void UpdateCoin(int value) => coin += value;
    public void UpdateApple(int value) => apple += value;


    // get resources
    public bool CheckMaxLevel() => level >= maxLevel ? true : false;

}