using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private bool _isSettingsOpen = false;
    [SerializeField] private RectTransform _visualSettings;
    [SerializeField] private RectTransform _settingContent;
    [SerializeField] private Slider _musicSlider;

    private void Start()
    {
        _settingContent.gameObject.SetActive(false);
        _settingContent.DOAnchorPosY(124, 0);
    }

    public void Onclick()
    {
        if (!_isSettingsOpen)
        {
            OpenSettings();
        }
        else
        {
            CloseSettings();
        }
    }

    private void OpenSettings()
    {
        RotateSettings(-90, 0.5f);
        _settingContent.gameObject.SetActive(true);
        _settingContent.DOAnchorPosY(-27, 0.5f).SetEase(Ease.InOutSine);
        _isSettingsOpen = true;
    }

    private void CloseSettings()
    {
        RotateSettings(0, 0.5f);
        _settingContent.DOAnchorPosY(124, 0.5f).SetEase(Ease.InOutSine).OnComplete(
            () => _settingContent.gameObject.SetActive(false));
        _isSettingsOpen = false;
    }

    public void RotateSettings(int angle, float time)
    {
        _visualSettings.DOLocalRotate(new Vector3(0, 0, angle), time).SetEase(Ease.InOutSine);
    }
}
