using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SoundShoot;

    void Start()
    {
        SoundShoot = GetComponent<AudioSource>();
    }
    
    public void ShootMusicOn()
    {
        SoundShoot.Play();
    }
}
