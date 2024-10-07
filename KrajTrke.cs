using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrajTrke : MonoBehaviour
{
    public GameObject kamera;           // Kamera podiuma
    public GameObject pozicijaKamere;   // Pocetna tacka kamere
    public GameObject pozicijaKamere1;  // Krajnja tacka kamere
    public GameObject UITrke;           // UI elementi trke
    public GameObject UIPodium;         // UI elementi podiuma
    public GameObject podium;           // Assets podiuma
    public GameObject igrac;
    private Vector3 brzina = Vector3.zero;
    public AudioSource publika;         // Zvuk publike
    public AudioSource muzika;          // Pozadinska muzika trke

    void Start()
    {
        // Uzimanje igraca, gasenje njegovih kamera, inicijalizacija podiuma
        igrac = GameObject.FindGameObjectWithTag("Player");
        igrac.GetComponent<AutoKontrola>().kamera1.SetActive(false);
        igrac.GetComponent<AutoKontrola>().kamera2.SetActive(false);
        igrac.GetComponent<AutoKontrola>().zadnjaKamera.SetActive(false);
        podium.SetActive(true);
        publika.Play();
        muzika.Stop();
        kamera.GetComponent<KameraKontroler>().enabled = false;
        kamera.transform.position = pozicijaKamere1.transform.position;
        kamera.transform.rotation = pozicijaKamere1.transform.rotation;
    }

    void Update()
    {   
        // Pomeranje kamere na podium ekranu i prikazivanje UI elemenata
        kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicijaKamere.transform.position, ref brzina, 1.5f);
        StartCoroutine(Brojac());
        IEnumerator Brojac()
        {
            UITrke.SetActive(false);
            yield return new WaitForSeconds(4.5f);
            UIPodium.SetActive(true);
        }
    }
}
