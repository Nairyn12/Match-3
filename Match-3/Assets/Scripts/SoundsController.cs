using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private AudioSource _soundForButton;
    [SerializeField] private AudioSource _soundDestroyTiles;
    [SerializeField] private AudioMixer am;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Awake()
    {
        _gm.loadingEvent += LoadingSoundMusicParam;
    }

    public void PlaySoundForButton()
    {
        _soundForButton.Play();
    }

    public void PlaySoundDestroyTiles()
    {
        _soundDestroyTiles.Play();
    }

    public void SoundVolume()
    {
        am.SetFloat("Sound", soundSlider.value);
        _gm.SoundVolume = soundSlider.value;
    }

    public void MusicVolume()
    {
        am.SetFloat("Music", musicSlider.value);
        _gm.MusicVolume = musicSlider.value;
    }

    private void LoadingSoundMusicParam()
    {
        musicSlider.value = _gm.MusicVolume;
        soundSlider.value = _gm.SoundVolume;
    }
}
