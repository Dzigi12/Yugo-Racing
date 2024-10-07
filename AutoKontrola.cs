using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AutoKontrola : MonoBehaviour
{
    public float[] maksimalneBrzineZaStepen;     // Maksimalna brzina za svaki stepen prenosa
    public float[] minimalneBrzineZaStepen;      // Minimalna brzina za svaki stepen prenosa
    public float ubrzanje;                       // Ubrzanje auta
    public float dodatnoUbrzanje;                // Dodatno ubrzanje auta na osnovu stepena prenosa i obrtaja
    public float snagaKočenja;                   // Snaga kočenja
    public float maksimalniUgaoSkretanja = 20f;  // Maksimalni ugao tocka prilikom skretanja
    public float brzina;                         // Brzina kretanja auta
    public float maxBrzina;                      // Maksimalna brzina
    public WheelCollider leviPrednjiTocak;       // Collider prednjeg levog tocka
    public WheelCollider desniPrednjiTocak;      // Collider prednjeg desnog tocka
    public WheelCollider leviZadnjiTocak;        // Collider zadnjeg levog tocka
    public WheelCollider desniZadnjiTocak;       // Collider zadnjeg desnog tocka
    public Transform transformPrednjiLeviTočak;  // Transform prednjeg levog točka
    public Transform transformPrednjiDesniTočak; // Transform prednjeg desnog točka
    public Transform transformZadnjiLeviTočak;   // Transform zadnjeg levog točka
    public Transform transformZadnjiDesniTočak;  // Transform zadnjeg desnog točka

    public int trenutniStepenPrenosa = 1;        // Trenutni stepen prenosa
    public int maksimalniStepen = 5;             // Maksimalan broj stepena prenosa
    public float brzinaUnazad = 2000f;           // Brzina unazad
    public GameObject stopSvetla;                // Svetla kad auto koci
    public GameObject farovi;                    // Farovi
    public GameObject rikvercSvetlo;             // Svetla kad auto ide u rikverc
    public GameObject kamera1;                   // Kamera koja je iza auta
    public GameObject kamera2;                   // Kamera koja je na haubi auta
    public GameObject zadnjaKamera;              // Kamera koja gleda iza auta

    public TextMeshProUGUI tekstObrtaja;         // UI Tekst za obrtaje
    public TextMeshProUGUI tekstBrzine;          // UI Tekst za brzinu
    public TextMeshProUGUI tekstStepenaPrenosa;  // UI Tekst za stepen prenosa
    public Slider sliderObrtaja;                 // Slajder koji prikazuje obrtaje 

    public Rigidbody sasijaAuta;                 // Rigidbody auta
    public int brojacFarova = 0;                 // Brojac koji odredjuje da li je far upaljen ili ugasen
    public int brojacKamere = 0;                 // Brojac koji odredjuje koja kamera je aktivna
    public void Awake()
    {
        kamera1 = GameObject.FindGameObjectWithTag("Kamera1");
    }
    public void Start()
    {
        kamera1 = GameObject.FindGameObjectWithTag("Kamera1");
        // Inicijalizacija maksimalnih brzina za svaki stepen prenosa na osnovu maksimalnih stepeni
        maksimalneBrzineZaStepen = new float[maksimalniStepen];
        for (int i = 0; i < maksimalniStepen; i++)
        {
            maksimalneBrzineZaStepen[i] = (i + 1) * 10f;
            maksimalneBrzineZaStepen[4] = maxBrzina;
        }
        // Inicijalizacija minimalnih brzina za svaki stepen prenosa na osnovu maksimalnih stepeni
        minimalneBrzineZaStepen = new float[maksimalniStepen];
        for (int i = 0; i < maksimalniStepen; i++)
        {
            minimalneBrzineZaStepen[i] = (i * 10f) - (i * 3);
        }
        // Inicijalizacija UI komponenata i pocetne brzine
        sliderObrtaja = GameObject.FindGameObjectWithTag("Slider").GetComponentInChildren<Slider>();
        tekstBrzine = GameObject.FindGameObjectWithTag("BrzinaText").GetComponentInChildren<TextMeshProUGUI>();
        tekstStepenaPrenosa = GameObject.FindGameObjectWithTag("StepenPrenosaTekst").GetComponentInChildren<TextMeshProUGUI>();
        trenutniStepenPrenosa = 1;
    }


    void Update()
    {
        // Dobijanje komande za kretanje
        float kontrolaKretanja = Input.GetAxis("Vertical");

        // Odredjivanje brzine auta
        brzina = sasijaAuta.velocity.magnitude;

        // Primena kretanja na prednje točkove na osnovu trenutnog stepena prenosa
        if (trenutniStepenPrenosa > 0)
        {
            Ubrzaj(kontrolaKretanja);
            rikvercSvetlo.SetActive(false);
        }
        else
        {
            rikvercSvetlo.SetActive(true);
        }
        // Primena kretanja na prednje točkove na osnovu rikverc stepena prenosa
        if (trenutniStepenPrenosa == 0)
        {
            if ((Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) && brzina < 10)
            {
                leviPrednjiTocak.motorTorque = kontrolaKretanja*brzinaUnazad;
                desniPrednjiTocak.motorTorque = kontrolaKretanja*brzinaUnazad;
            }
            else
            {
                leviPrednjiTocak.motorTorque = 0f;
                desniPrednjiTocak.motorTorque = 0f;
            }
        }


        // Primena kočenja kada je pritisnuta Space tipka
        if (Input.GetKey(KeyCode.Space))
        {
            Koči();
            stopSvetla.SetActive(true);
        }
        else
        {
            // Puštanje kočnica
            leviPrednjiTocak.brakeTorque = 0f;
            desniPrednjiTocak.brakeTorque = 0f;
            leviZadnjiTocak.brakeTorque = 0f;
            desniZadnjiTocak.brakeTorque = 0f;
            stopSvetla.SetActive(false);
        }

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

        // Rotacija prednjih tockova prilikom skretanja
        leviPrednjiTocak.steerAngle = Input.GetAxis("Horizontal") * maksimalniUgaoSkretanja;
        desniPrednjiTocak.steerAngle = Input.GetAxis("Horizontal") * maksimalniUgaoSkretanja;

        // Rukovanje manuelnim menjanjem stepena prenosa
        RukujManuelnimMenjanjem();

        // Prikazivanje UI Tekst elemenata
        UITekst();

        // Paljenje i gasenje farova
        if (Input.GetKeyDown("h"))
        {
            brojacFarova++;
            if (brojacFarova % 2 == 0)
            {
                farovi.SetActive(false);
            }
            else farovi.SetActive(true);
        }

        // Menjanje kamere
        if (Input.GetKeyDown("c"))
        {
            zadnjaKamera.SetActive(false);
            brojacKamere++;
            if (brojacKamere % 2 != 0)
            {
                kamera1.SetActive(false);
                kamera2.SetActive(true);
            }
            else
            {
                kamera1.SetActive(true);
                kamera2.SetActive(false);
            }

        }

        // Pogled iza auta
        if (Input.GetKey("b"))
        {
            zadnjaKamera.SetActive(true);
            kamera1.SetActive(false);
            kamera2.SetActive(false);
        }
        else
        {
            zadnjaKamera.SetActive(false);
            if (brojacKamere % 2 != 0)
            {
                kamera1.SetActive(false);
                kamera2.SetActive(true);
            }
            else
            {
                kamera1.SetActive(true);
                kamera2.SetActive(false);
            }
        }
    }

    void Ubrzaj(float kontrolaKretanja)
    {
        // Provera da li je auto u ograničenju brzine za trenutni stepen prenosa
        if ((Input.GetKey("w") && (brzina < maksimalneBrzineZaStepen[trenutniStepenPrenosa - 1] && brzina >= minimalneBrzineZaStepen[trenutniStepenPrenosa - 1] && trenutniStepenPrenosa >= 1))|| trenutniStepenPrenosa<1)
        {
                // Ubrzanje u skladu sa trenutnim stepenom prenosa
                dodatnoUbrzanje = (50 / trenutniStepenPrenosa * (10 - (maksimalneBrzineZaStepen[trenutniStepenPrenosa-1] - brzina))) * 2;
                leviPrednjiTocak.motorTorque = kontrolaKretanja * (ubrzanje+dodatnoUbrzanje);
                desniPrednjiTocak.motorTorque = kontrolaKretanja * (ubrzanje+dodatnoUbrzanje);
        }
        else
        {
            // Ako je dostignuta brzina ograničenja, spreči dalje ubrzanje
            leviPrednjiTocak.motorTorque = 0f;
            desniPrednjiTocak.motorTorque = 0f;
        }
    }

    public void Koči()
    {
        // Primena kočenja na oba prednja točka
        leviPrednjiTocak.brakeTorque = snagaKočenja;
        desniPrednjiTocak.brakeTorque = snagaKočenja;
        leviZadnjiTocak.brakeTorque = snagaKočenja;
        desniZadnjiTocak.brakeTorque = snagaKočenja;
    }

    void RukujManuelnimMenjanjem()
    {
        // Pomeranje na viši stepen prenosa pritiskom na taster 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            if((trenutniStepenPrenosa == 0 && brzina<3) || (trenutniStepenPrenosa < maksimalniStepen && trenutniStepenPrenosa > 0))
            {
                trenutniStepenPrenosa++;
            }
        }

        // Pomeranje na niži stepen prenosa pritiskom na taster 'Q'
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if((trenutniStepenPrenosa == 1 && brzina<3) || (trenutniStepenPrenosa > 1))
            trenutniStepenPrenosa--;
        }
    }

    void UITekst()
    {
        // Prikaz brzine i stepena prenosa u UI
        if (trenutniStepenPrenosa > 0)
        {
            tekstStepenaPrenosa.text = "Stepen:" + trenutniStepenPrenosa.ToString();
            tekstBrzine.text = "Brzina:" + (brzina*2).ToString("0") + "km/h";
        }
        else
        {
            // Prikaz brzine i stepena prenosa u rikverc
            tekstStepenaPrenosa.text = "Stepen: R";
            tekstBrzine.text = "Brzina:" + (-(brzina*2)).ToString("0") + "km/h";
        }
        // Prikaz obrtaja za rikverc
        if (trenutniStepenPrenosa == 0)
        {
            sliderObrtaja.minValue = minimalneBrzineZaStepen[0];
            sliderObrtaja.maxValue = maksimalneBrzineZaStepen[0];
            sliderObrtaja.value = brzina;
        }
        else
        {   
            // Prikaz obrtaja za trenutnu brzinu, osim rikverca
            sliderObrtaja.minValue = minimalneBrzineZaStepen[trenutniStepenPrenosa - 1];
            sliderObrtaja.maxValue = maksimalneBrzineZaStepen[trenutniStepenPrenosa - 1];
            sliderObrtaja.value = brzina;
        }
    }
}
