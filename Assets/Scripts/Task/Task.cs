using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    protected ProgressManager _pm;
    protected MapManager _map;
    protected GameManagerment _gm;
    // [SerializeField] protected GameObject _taskPrefab;
    protected Text _text;
    protected Text _bText;
    protected Button _button;
    protected Image _bImage;

    protected void Awake()
    {
        _pm = ProgressManager.instance;
        _map = MapManager.instance;
        _gm = GameManagerment.instance;

        _text = transform.GetChild(0).GetComponentInChildren<Text>();
        _bText = transform.GetChild(1).GetComponentInChildren<Text>();
        _button = transform.GetChild(1).GetComponent<Button>();
        _bImage = transform.GetChild(1).GetComponent<Image>();

        _button.onClick.AddListener(CheckTask);
    }


    protected void UpdateTaskContent(string t1, string t2)
    {
        _text.text = t1;
        _bText.text = t2;
    }
    protected virtual void CheckTask() { }
}