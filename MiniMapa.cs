using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public GameObject auto;
    public GameObject sprite;   // Grafika na minimapi koja ce da prati botove

    void Update()
    {
        if (auto == null) Destroy(gameObject);
        else
        sprite.transform.position = auto.transform.position;
    }
}
