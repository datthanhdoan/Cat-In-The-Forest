using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class DataManager : MonoBehaviour
{

    private ResourceManager _rM;
    private MapManager _mapManager;
    private QuestManager _questManager;

    private void Start()
    {
        _rM = ResourceManager.Instance;
        _mapManager = MapManager.Instance;
        _questManager = QuestManager.Instance;
        LoadAllData();
    }

    public void SaveAllData()
    {
        SaveResourceData();

        SaveRegionDataUnLocked();
    }

    public void LoadAllData()
    {
        // Debug.Log("Load All Data");
        // Resource
        LoadResourceData();

        // Map
        LoadRegionDataUnLocked();
        _mapManager.UpdateNavMesh(); // Update NavMesh after load Region

        LoadQuestInfoListData();
    }

    // Resource ---------------------

    // public void SaveResourceData() // wrong
    // {

    //     List<ItemData> itemDataList = _rM.GetItemDataList();

    //     Resource itemDataListWrapper = new Resource
    //     {
    //         itemList = itemDataList
    //     };

    //     string json = JsonUtility.ToJson(itemDataListWrapper);
    //     string path = Application.dataPath + Path.DirectorySeparatorChar + "resource.json";

    //     using (StreamWriter writer = new StreamWriter(path))
    //     {
    //         writer.Write(json);
    //     }

    // }

    public void SaveResourceData()
    {
        List<ItemData> itemDataList = _rM.GetItemDataList();
        Resource itemDataListWrapper = new Resource
        {
            coin = _rM.GetCoin(),
            diamond = _rM.GetDiamond(),
            itemList = itemDataList
        };

        string json = JsonUtility.ToJson(itemDataListWrapper);
        string path = Application.dataPath + Path.DirectorySeparatorChar + "resource.json";

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }
    }

    public void LoadResourceData()
    {
        // TODO : Fix can't load data from json
        string path = Application.dataPath + Path.DirectorySeparatorChar + "resource.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                Resource itemDataListWrapper = JsonUtility.FromJson<Resource>(json);
                _rM.SetCoin(itemDataListWrapper.coin);
                _rM.SetDiamond(itemDataListWrapper.diamond);
                _rM.SetItemDataList(itemDataListWrapper.itemList);

            }
        }
        else
        {
            Debug.Log("File not found");
        }
    }


    // Quest ---------------------

    public void LoadQuestInfoListData()
    {
        string path = Application.dataPath + Path.DirectorySeparatorChar + "quest.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                QuestInfoList questInfoListWrapper = JsonUtility.FromJson<QuestInfoList>(json);
                _questManager.SetQuestInfoList(questInfoListWrapper);
            }
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    // Map ---------------------
    public void SaveRegionDataUnLocked()
    {
        int index = _mapManager.GetLevel();
        string path = Application.dataPath + Path.DirectorySeparatorChar + "region.json";
        // string json = JsonUtility.ToJson(_mapManager);
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(index);
        }
    }

    public void LoadRegionDataUnLocked()
    {
        // Read from json file
        int index = 0;
        string path = Application.dataPath + Path.DirectorySeparatorChar + "region.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                index = int.Parse(json);
            }
        }
        else
        {
            Debug.Log("File not found");
        }

        // Load
        // index is current level , player has unlocked

        foreach (Transform region in _mapManager.region.transform)
        {
            region.gameObject.SetActive(false);
        }
        _mapManager.Setlevel(index);


        // Set active region
        for (int i = 0; i < index; i++)
        {
            var region = _mapManager.region.transform.GetChild(i).gameObject;

            region.SetActive(true);
            // Hide connected region of unlocked region and not last region
            if (i < index - 1)
            {
                region.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    // Quit ---------------------
    private void OnApplicationQuit()
    {
        SaveAllData();
    }

}
