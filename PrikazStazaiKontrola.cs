using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrikazStazaiKontrola : MonoBehaviour
{
    public GameObject Meni;     // Ekran za prikaz menija
    public GameObject Staze;    // Ekran za biranje staza
    public PremestiAutoUIgru premestiAutoUIgru;
    public GameObject GreskaKola;   // Tekst greske
    public GameObject GreskaStaza;  // Tekst greske
    public GameObject Kontrole;     // Ekran za kontrole

    private void Awake()
    {
        //Pustanje muzike u meniju
        GameObject.FindGameObjectWithTag("Muzika").GetComponent<MuzikaMeni>().PlayMusic();
    }
    public void meni()
    {
        // Vracanje u meni iz ekrana odabira mapa i kontrola
        Meni.SetActive(true);
        GreskaStaza.SetActive(false);
        Staze.SetActive(false);
        Kontrole.SetActive(false);
    }
    public void proveraAuto()
    {
        bool autoIzabran;

        // Provera da li je igrac odabrao auto, ako nije izbacuje gresku
        if (premestiAutoUIgru.auto == null)
        {
            autoIzabran = false;
            GreskaKola.SetActive(true);
        }
        else 
        { 
            // Ako je auto odabran, moze da bira stazu
            autoIzabran = true;
            Meni.SetActive(false);
            Staze.SetActive(true);
        }

    }
    // Prikaz ekrana za kontrole
    public void kontrole()
    {
        Meni.SetActive(false);
        Kontrole.SetActive(true);
    }
}
