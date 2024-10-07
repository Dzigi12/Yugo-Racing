using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzikaMeni : MonoBehaviour
{
    public AudioSource muzika;
    public static MuzikaMeni muzikaMeni;
    public void Awake()
    {
        // Prevacivanje muzike menija kroz scene
        if (muzikaMeni == null)
        {
            muzikaMeni = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        muzika = GetComponentInChildren<AudioSource>();
    }

    public void PlayMusic()
    {
        if (muzika.isPlaying) return;
        muzika.Play();
    }

    public void StopMusic()
    {
        muzika.Stop();
    }
}
