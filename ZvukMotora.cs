using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZvukMotora : MonoBehaviour
{
    public AudioSource motor;       // Zvuk motora
    public float minPitch = 0.8f;   // Najmanji "pitch" motora
    public float maxPitch = 2.0f;   // Najveci "pitch" motora
    public float maxRPM = 7000f;    // Najveci rpm motora
    public AutoKontrola autoKontrola;
    public float rpm;
    public GameObject podium;

    void Start()
    {
        motor.loop = true;
        motor.Play();

        podium = GameObject.FindGameObjectWithTag("Podium");
    }

    void Update()
    {
        // Simuliranje obrtaja motora
        rpm = autoKontrola.brzina * 100f;

        // Izracunavanje "pitch-a" na osnovu obrtaja
        float pitch = Mathf.Lerp(minPitch, maxPitch, rpm / maxRPM);
        motor.pitch = pitch;

        // Gasenje motora na podiumu
        if (podium.GetComponentInChildren<KrajTrke>().enabled == true)
        {
            motor.mute = true;
        }
        else motor.mute = false;
    }
}

