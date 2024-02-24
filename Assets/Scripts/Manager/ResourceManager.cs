using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Quản lý tài nguyên trong game (ví dụ: quản lý số lượng quả táo, nho, gỗ)
/// </summary>
public class ResourceManager : MonoBehaviour
{

    public static ResourceManager instance { get; private set; }
    // resources
    public int coin { get; private set; } = 0;
    public enum ResourceName
    {
        Apple,
        Grape,
        Wood
    }
    [System.Serializable]
    public class Resource
    {
        public ResourceName name;
        public Sprite sprite;
        public int quantity;//{ get; private set; }

        public void SetQuantity(int value) => quantity = value;

    }
    // Show the list of fruits in the inspector
    [SerializeField] private List<Resource> ResourcesList = new List<Resource>();
    private Dictionary<ResourceName, Resource> resourcesDict = new Dictionary<ResourceName, Resource>();


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

    #region Setters and Getters
    public void SetCoin(int value) => coin = value;
    private void Start()
    {
        // convert the list to a dictionary
        foreach (var fruit in ResourcesList)
        {
            resourcesDict.Add(fruit.name, fruit);
        }
    }
    #endregion

    // public void UpdateFruit(ResourceName name, int value)
    // {
    //     if (resourcesDict.ContainsKey(name))
    //     {
    //     }
    //     else
    //     {
    //         Debug.LogError($"Fruit with name : {name} not found");
    //     }
    // }

    public Resource GetResource(ResourceName name)
    {
        if (resourcesDict.ContainsKey(name))
        {
            return resourcesDict[name];
        }
        else
        {
            Debug.LogError($"Fruit with name : {name} not found");
            return null;
        }
    }
}