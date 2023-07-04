using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private AudioSource _soundForButton;
    [SerializeField] private AudioSource _soundDestroyTiles;


    public void PlaySoundForButton()
    {
        _soundForButton.Play();
    }

    public void PlaySoundDestroyTiles()
    {
        _soundDestroyTiles.Play();
    }
}
