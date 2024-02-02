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

        // _button.onClick.AddListener(CheckTask);
        _text = transform.GetChild(1).GetComponent<Text>();
        _bText = transform.GetChild(2).GetComponentInChildren<Text>();
        _button = transform.GetChild(2).GetComponent<Button>();
        _bImage = transform.GetChild(2).GetComponent<Image>();
    }


    protected void UpdateTaskContent(string t1, string t2)
    {
        _text = transform.GetChild(1).GetComponent<Text>();
        _text.text = t1;
        _bText = transform.GetChild(2).GetComponentInChildren<Text>();
        _bText.text = t2;
    }
    protected void CheckTask() { }
}