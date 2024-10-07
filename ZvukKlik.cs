using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZvukKlik : MonoBehaviour
{
    public AudioSource klik;    // Zvuk dugmica u meniju
    public AudioSource sprej;   // Zvuk menjanja boje auta
    public void Klik()
    {
        klik.Play();
    }
    public void Sprej()
    {
        sprej.Play();
    }
}
