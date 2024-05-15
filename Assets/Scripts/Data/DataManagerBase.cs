using UnityEngine;
using System.IO;

public class DataManagerBase<T>
{
    private string _fileName;
    private const string FOLDER_NAME = "Data";

    public DataManagerBase(string fileName)
    {
        _fileName = fileName;
        CheckFolderCreate();
    }

    private void CheckFolderCreate()
    {
        string path = GetFolderPath();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private string GetFolderPath()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + FOLDER_NAME;
    }

    private string GetFilePath()
    {
        return GetFolderPath() + Path.DirectorySeparatorChar + _fileName;
    }

    public void SaveData(T data)
    {
        string json = JsonUtility.ToJson(data);
        string path = GetFilePath();
        File.WriteAllText(path, json);
    }

    public T LoadData()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            return default;
        }
    }
}
