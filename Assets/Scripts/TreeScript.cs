using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameManagerment gameManagerment;
    private bool hasBeenClicked = false;
    private bool hasFruit = false;
    private float timeToSpawn = 5f;
    private float timeToSpawnTimer = 0f;
    public GameObject treeImage;
    [SerializeField] GameObject fruit;

    void Start()
    {
        fruit.SetActive(true);
        hasFruit = fruit.activeSelf;
    }
    void Update()
    {
        // Player dung lai va nguoi choi khong an chuot thi moi cho an
        if (hasBeenClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasBeenClicked = false;
            }
            else if (!gameManagerment.CheckPlayerMoving())
            {
                hasBeenClicked = false;
                TakeFruit();
                TreeStretchAnim();
            }
        }
        timeToSpawnTimer = hasFruit ? 0f : timeToSpawnTimer + Time.deltaTime;
        if (timeToSpawnTimer >= timeToSpawn)
        {
            SpawnFruit();
        }

    }
    public void OnMouseDown()
    {
        TreeStretchAnim();
        hasBeenClicked = true;
        // return true;
    }

    public void TakeFruit()
    {
        if (hasFruit)
        {
            hasFruit = false;
            fruit.SetActive(false);
            timeToSpawnTimer = 0f;
            gameManagerment.fruitCount++;
        }
    }

    public void SpawnFruit()
    {
        if (!hasFruit)
        {
            TreeStretchAnim();
            hasFruit = true;
            fruit.SetActive(true);
        }
    }


    private void TreeStretchAnim()
    {

        treeImage.GetComponent<Animator>().CrossFade(TreeStretch, 0f);
    }
    private static readonly int TreeStretch = Animator.StringToHash("Tree-Stretch");

}
