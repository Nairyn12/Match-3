using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMusicOnOff : MonoBehaviour
{
    [SerializeField] SoundsController _sc;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider; 

    public void MusicOnOff()
    {
        if (musicSlider.value > -50)
        {
            musicSlider.value = -50;
            _sc.MusicVolume();
        }
        else
        {
            musicSlider.value = 5;
            _sc.MusicVolume();
        }
    }

    public void SoundOnOff()
    {
        if (soundSlider.value > -50)
        {
            soundSlider.value = -50;
            _sc.SoundVolume();
        }
        else
        {
            soundSlider.value = 5;
            _sc.SoundVolume();
        }
    }
}
