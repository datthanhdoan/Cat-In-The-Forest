using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private AudioSource _musicAudioSource;

    private void Start()
    {
        // listen for value change
        _musicSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        _musicAudioSource.volume = volume;
    }
}
