using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutoKontola : MonoBehaviour
{
    public float ubrzanje;                       // Ubrzanje auta
    public float snagaKočenja;                   // Snaga kočenja
    public float maksimalniUgaoSkretanja = 20f;  // Maksimalni ugao tocka prilikom skretanja
    public float brzina;                         // Brzina kretanja auta
    public float maksimalnaBrzina = 60f;         // Maksimalna brzina auta
    public WheelCollider leviPrednjiTocak;       // Collider prednjeg levog tocka
    public WheelCollider desniPrednjiTocak;      // Collider prednjeg desnog tocka
    public WheelCollider leviZadnjiTocak;        // Collider zadnjeg levog tocka
    public WheelCollider desniZadnjiTocak;       // Collider zadnjeg desnog tocka
    public Transform transformPrednjiLeviTočak;  // Transform prednjeg levog točka
    public Transform transformPrednjiDesniTočak; // Transform prednjeg desnog točka
    public Transform transformZadnjiLeviTočak;   // Transform zadnjeg levog točka
    public Transform transformZadnjiDesniTočak;  // Transform zadnjeg desnog točka
    public float brzinaUnazad = -2000f;          // Brzina unazad
    public Rigidbody sasijaAuta;                 // Rigidbody auta
    public AutoSistemPozicija autoSistemPozicija;// Referenca na skriptu

    void Update()
    {
        // Odredjivanje brzine auta
        brzina = sasijaAuta.velocity.magnitude;
        // Definisanje pozicije i rotacije tockova
        Vector3 poz = transform.position;
        Quaternion rot = transform.rotation;

        // Primena pozicije i rotacije tockova sa collider-a na transform
        desniPrednjiTocak.GetWorldPose(out poz, out rot);
        transformPrednjiDesniTočak.position = poz;
        transformPrednjiDesniTočak.rotation = rot;

        desniZadnjiTocak.GetWorldPose(out poz, out rot);
        transformZadnjiDesniTočak.position = poz;
        transformZadnjiDesniTočak.rotation = rot;

        leviPrednjiTocak.GetWorldPose(out poz, out rot);
        transformPrednjiLeviTočak.position = poz;
        transformPrednjiLeviTočak.rotation = rot * Quaternion.Euler(0, 180, 0); // Ispravka rotacije tocka jer je sa leve strane, da bi se vrteo u dobrom smeru

        leviZadnjiTocak.GetWorldPose(out poz, out rot);
        transformZadnjiLeviTočak.position = poz;
        transformZadnjiLeviTočak.rotation = rot * Quaternion.Euler(0, 180, 0); // Ispravka rotacije tocka jer je sa leve strane, da bi se vrteo u dobrom smeru
    }
    public void Kontrole(float kretanje, float skretanje, float kocenje) // Primanje kontrola za kretanje bota
    {
        // Ogranicavanje maksimalne brzine bota
        if (sasijaAuta.velocity.magnitude >= maksimalnaBrzina)
        {
            desniPrednjiTocak.motorTorque = 0f;
            leviPrednjiTocak.motorTorque = 0f;
        }
        else
        {
            // Ubrzavanje bota na osnovu primljenih kontrola
            desniPrednjiTocak.motorTorque = (ubrzanje + autoSistemPozicija.pozicijaAuta * 150) * kretanje;
            leviPrednjiTocak.motorTorque = (ubrzanje + autoSistemPozicija.pozicijaAuta * 150) * kretanje;
        }

        // Skretanje bota na osnovu primljenih kontrola
        leviPrednjiTocak.steerAngle = skretanje * maksimalniUgaoSkretanja;
        desniPrednjiTocak.steerAngle = skretanje * maksimalniUgaoSkretanja;

        // Kocenje bota na osnovu primljenih kontrola
        leviPrednjiTocak.brakeTorque = snagaKočenja * kocenje;
        desniPrednjiTocak.brakeTorque = snagaKočenja * kocenje;
        leviZadnjiTocak.brakeTorque = snagaKočenja * kocenje;
        desniZadnjiTocak.brakeTorque = snagaKočenja * kocenje;
    }
}
