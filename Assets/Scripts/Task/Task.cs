using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    protected TaskManagerment _tm;
    protected ProgressManager _pm;
    protected MapManager _map;
    // [SerializeField] protected GameObject _taskPrefab;
    protected Text _text;
    protected Text _bText;
    protected Button _button;
    protected Image _bImage;

    protected void Start()
    {
        _tm = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManagerment>();
        _pm = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();
        _map = GameObject.Find("MapManager").GetComponent<MapManager>();

        _text = transform.GetChild(0).GetComponentInChildren<Text>();

        _bText = transform.GetChild(1).GetComponentInChildren<Text>();
        _button = transform.GetChild(1).GetComponent<Button>();
        _button.onClick.AddListener(CheckTask);
        _bImage = transform.GetChild(1).GetComponent<Image>();
    }


    protected void UpdateTaskContent(string t1, string t2)
    {
        _text.text = t1;
        _bText.text = t2;
    }
    protected virtual void CheckTask() { }
}