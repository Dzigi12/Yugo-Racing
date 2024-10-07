using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPozicijaIgraca : MonoBehaviour
{

    public Transform igrac;
    public Transform pozicijaIgraca;

    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Player").transform;
        // Pozicioniranje igraca na pocetku trke
        igrac.position = pozicijaIgraca.position;
        igrac.rotation = pozicijaIgraca.rotation;
    }
}