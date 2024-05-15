using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static event Action OnDataLoaded;
    private ResourceManager _rM;
    private MapManager _mapManager;
    [SerializeField] private QuestManager _questManager;

    private DataManagerBase<Resource> _resourceDataManager;
    private DataManagerBase<QuestData> _questDataManager;
    private DataManagerBase<RegionData> _regionDataManager;

    private void Start()
    {
        _rM = ResourceManager.Instance;
        _mapManager = MapManager.Instance;

        _resourceDataManager = new DataManagerBase<Resource>("resource.json");
        _questDataManager = new DataManagerBase<QuestData>("quest.json");
        _regionDataManager = new DataManagerBase<RegionData>("region.json");
        LoadAllData();
    }

    public void SaveAllData()
    {
        _resourceDataManager.SaveData(new Resource
        {
            coin = _rM.GetCoin(),
            diamond = _rM.GetDiamond(),
            itemList = _rM.GetItemDataList()
        });

        _questDataManager.SaveData(_questManager._questInfoList);

        _regionDataManager.SaveData(_mapManager._regionData);
    }

    public void LoadAllData()
    {
        Resource resourceData = _resourceDataManager.LoadData();
        if (resourceData != null)
        {
            _rM.SetCoin(resourceData.coin);
            _rM.SetDiamond(resourceData.diamond);
            _rM.SetItemDataList(resourceData.itemList);
        }
        else
        {
            LoadDefaultResourceData();
        }

        QuestData questData = _questDataManager.LoadData();
        if (questData != null)
        {
            _questManager.SetQuestData(questData);
        }
        else
        {
            LoadDefaultQuestData();
        }

        RegionData regionLevel = _regionDataManager.LoadData();
        if (regionLevel != null)
        {
            _mapManager.Setlevel(regionLevel.level);
        }
        else
        {
            _mapManager.Setlevel(1);
        }

        _mapManager.UpdateNavMesh();

        OnDataLoaded?.Invoke();
    }

    private void LoadDefaultResourceData()
    {
        TextAsset itemDataList = Resources.Load<TextAsset>("resource");
        Resource itemDataListWrapper = JsonUtility.FromJson<Resource>(itemDataList.text);

        _rM.SetCoin(itemDataListWrapper.coin);
        _rM.SetDiamond(itemDataListWrapper.diamond);
        _rM.SetItemDataList(itemDataListWrapper.itemList);
    }

    private void LoadDefaultQuestData()
    {
        TextAsset questInfoList = Resources.Load<TextAsset>("quest");
        QuestData questInfoListWrapper = JsonUtility.FromJson<QuestData>(questInfoList.text);

        _questManager.SetQuestData(questInfoListWrapper);
    }



    private void OnApplicationQuit()
    {
        SaveAllData();
    }
}

