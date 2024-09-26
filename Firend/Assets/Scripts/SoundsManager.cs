using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public Slider soundSlider;
    public AudioMixer audioMixer;
    public float volume;
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            Load();
        }
        else
        {
            SliderMusic();
        }
    }

    public void SliderMusic()
    {
        volume = soundSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat("volume");
        SliderMusic();
    }
}
