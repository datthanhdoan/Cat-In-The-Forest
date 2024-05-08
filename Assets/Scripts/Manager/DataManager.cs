using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine.UI;
using System;

// TODO : Tạo SAVE cho quest

public class DataManager : MonoBehaviour
{
    public static event Action OnDataLoaded;
    private ResourceManager _rM;
    private MapManager _mapManager;
    [SerializeField] private QuestManager _questManager;

    private const string FOLDER_NAME = "Data";


    private void Start()
    {
        _rM = ResourceManager.Instance;
        _mapManager = MapManager.Instance;
        LoadAllData();
    }

    public void SaveAllData()
    {
        if (!Directory.Exists(Application.dataPath + Path.DirectorySeparatorChar + FOLDER_NAME))
        {
            Directory.CreateDirectory(Application.dataPath + Path.DirectorySeparatorChar + FOLDER_NAME);
        }
        SaveResourceData();
        SaveQuestData();
        SaveRegionDataUnLocked();
    }

    public void LoadAllData()
    {

        CheckFolderCreate();

        LoadResourceData();

        // Map
        LoadRegionDataUnLocked();
        _mapManager.UpdateNavMesh(); // Update NavMesh after load Region

        LoadQuestData();
        OnDataLoaded?.Invoke();
    }

    public void CheckFolderCreate()
    {
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    // Resource ---------------------

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
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "resource.json";

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }
    }

    public void SaveRegionDataUnLocked()
    {
        int index = _mapManager.GetLevel();
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "region.json";
        // Windows path : C:\Users\{username}\AppData\LocalLow\{company name}\{project name}
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(index);
        }
    }

    public void SaveQuestData()
    {
        QuestData questInfoListWrapper = _questManager._questInfoList;
        string json = JsonUtility.ToJson(questInfoListWrapper);
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "quest.json";

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }
    }


    public void LoadResourceData()
    {
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "resource.json";
        if (File.Exists(path))
        {
            using var reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            Resource itemDataListWrapper = JsonUtility.FromJson<Resource>(json);
            _rM.SetCoin(itemDataListWrapper.coin);
            _rM.SetDiamond(itemDataListWrapper.diamond);
            _rM.SetItemDataList(itemDataListWrapper.itemList);
        }
        else
        {
            File.Create(path).Dispose();
            // Load default data from Resources folder
            TextAsset itemDataList = Resources.Load<TextAsset>("resource");
            Resource itemDataListWrapper = JsonUtility.FromJson<Resource>(itemDataList.text);
            // Load default data from Resources folder

            _rM.SetCoin(itemDataListWrapper.coin);
            _rM.SetDiamond(itemDataListWrapper.diamond);
            _rM.SetItemDataList(itemDataListWrapper.itemList);

            // Write the default data to the new file
            using var writer = new StreamWriter(path, false);
            string json = JsonUtility.ToJson(itemDataListWrapper);
            writer.Write(json);
        }
    }


    // Quest ---------------------

    public void LoadQuestData()
    {
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "quest.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                QuestData questInfoListWrapper = JsonUtility.FromJson<QuestData>(json);
                _questManager.SetQuestData(questInfoListWrapper);
            }
        }
        else
        {
            File.Create(path).Dispose();
            // Load default data from Resources folder
            TextAsset questInfoList = Resources.Load<TextAsset>("quest");
            QuestData questInfoListWrapper = JsonUtility.FromJson<QuestData>(questInfoList.text);
            // Load default data from Resources folder
            Debug.Log(questInfoList.text);
            _questManager.SetQuestData(questInfoListWrapper);

            // Write the default data to the new file
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                string json = JsonUtility.ToJson(questInfoListWrapper);
                writer.Write(json);
            }
        }
    }

    // Map ---------------------

    public void LoadRegionDataUnLocked()
    {
        // Read from json file
        int index = 0;
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME + Path.DirectorySeparatorChar + "region.json";
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
            File.Create(path).Dispose();
            TextAsset regionData = Resources.Load<TextAsset>("region");
            int regionDataWrapper = int.Parse(regionData.text);
            index = regionDataWrapper;
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
