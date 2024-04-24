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
        _mapManager = MapManager.instance;
        _questManager = QuestManager.Instance;
    }

    public void SaveAllData()
    {
        SaveResourceData();

        SaveRegionDataUnLocked();
    }

    public void LoadAllData()
    {
        // Resource
        LoadResourceData();

        // Map
        LoadRegionDataUnLocked();
        _mapManager.UpdateNavMesh(); // Update NavMesh after load Region

        LoadQuestInfoListData();
    }


    public void SaveResourceData()
    {

        List<ItemData> itemDataList = _rM.GetItemDataList();

        ItemDataList itemDataListWrapper = new ItemDataList
        {
            itemList = itemDataList
        };

        string json = JsonUtility.ToJson(itemDataListWrapper);
        string path = Application.dataPath + "/Json/item.json";
        Debug.Log(json);
        Debug.Log("Save Data to: " + path);

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }

    }

    public void LoadResourceData()
    {

        // TODO : Fix can't load data from json
        string path = Application.dataPath + "/Json/item.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                Debug.Log(json);
                ItemDataList itemDataListWrapper = JsonUtility.FromJson<ItemDataList>(json);
                _rM.SetItemDataList(itemDataListWrapper.itemList);
            }
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void SaveRegionDataUnLocked()
    {
        int index = _mapManager.level;
        string path = Application.dataPath + "/Json/region.json";
        Debug.Log("Save Data to: " + path);
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(index);
        }
    }

    public void LoadQuestInfoListData()
    {
        string path = Application.dataPath + "/Json/quest.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                Debug.Log(json);
                QuestInfoList questInfoListWrapper = JsonUtility.FromJson<QuestInfoList>(json);
                Debug.Log(questInfoListWrapper.questList);
                _questManager.SetQuestInfoList(questInfoListWrapper);
            }
        }
        else
        {
            Debug.Log("File not found");
        }
    }
    public void LoadRegionDataUnLocked()
    {
        int index = 0;
        string path = Application.dataPath + "/Json/region.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                Debug.Log(json);
                index = int.Parse(json);
                Debug.Log("Load Data from: " + path);
            }
        }
        else
        {
            Debug.Log("File not found");
        }
        // index is current level , player has unlocked
        for (int i = 0; i < index; i++)
        {
            _mapManager.region.transform.GetChild(i).gameObject.SetActive(true);
        }
    }




}
