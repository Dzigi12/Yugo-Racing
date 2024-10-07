using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZvukMotoraBot : MonoBehaviour
{
    public AudioSource motor;
    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;
    public float maxRPM = 7000f;
    public AIAutoKontola aIAutoKontola;
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
        // Simulirajte RPM (broj obrtaja) - ovde bi trebalo da koristite stvarne podatke iz vašeg vozila
        //rpm = Mathf.PingPong(Time.time * 1000f, maxRPM);
        rpm = aIAutoKontola.brzina * 100f;

        // Izračunajte pitch na osnovu trenutnog RPM-a
        float pitch = Mathf.Lerp(minPitch, maxPitch, rpm / maxRPM);
        motor.pitch = pitch;

        if (podium.GetComponentInChildren<KrajTrke>().enabled == true)
        {
            motor.mute = true;
        }
        else motor.mute = false;
    }
}

