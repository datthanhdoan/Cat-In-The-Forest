using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance { get; private set; }

    // resources
    public int coin { get; private set; } = 0;
    public int apple { get; private set; } = 0;
    public int grape { get; private set; } = 0;
    public int wood { get; private set; } = 0;

    // level
    [SerializeField] public GameObject region;
    public int level { get; private set; } = 1;
    public int maxLevel { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        maxLevel = region.transform.childCount;
    }
    #region Getters and Setters 
    // set resources
    public void UpdateLevel() => level++;
    public void UpdateCoin(int value) => coin += value;
    public void UpdateApple(int value) => apple += value;
    public void UpdateGrape(int value) => grape += value;
    public void UpdateWood(int value) => wood += value;

    public void SetCoin(int value) => coin += value;

    // get resources
    public bool CheckMaxLevel() => level >= maxLevel ? true : false;

    public int GetCoin() => coin;

    #endregion
}