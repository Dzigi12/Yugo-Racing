using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapaIgrac : MonoBehaviour
{
    public GameObject auto;     // Igrac
    public GameObject sprite;   // Grafika koja ce da prati igraca na minimapi

    void Start()
    {
        auto = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        sprite.transform.position = auto.transform.position;
    }
}
