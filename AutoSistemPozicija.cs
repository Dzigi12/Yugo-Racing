using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSistemPozicija : MonoBehaviour
{ 
    public int prodjeneTacke = 0;   // Kolicina tacaka koje je auto prosao
    public int brojKola;            // Jedinstveni broj koji oznacava pojedinacni auto
    public SistemPozicijaV2 sistemPozicijaV2;
    public int pozicijaAuta;        // Pozicija auta u trci
    public int krug = 0;            // Broj prodjenih krugova

    private void Start()
    {
        sistemPozicijaV2 = GameObject.FindGameObjectWithTag("SistemPozicija").GetComponent<SistemPozicijaV2>();
        if (sistemPozicijaV2.gotovo == true) { unistiBota(); } // Unistavanje visak botova
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meta"))    // Proveravanje da li je auto dosao u kontakt sa tackom
        {
            prodjeneTacke++;    // Povecavanje kolicine tacaka koje je auto prosao

            // Ako su prodjene sve tacke, povecava se krug i vraca se na pocetnu tacku
            if(prodjeneTacke == sistemPozicijaV2.ukupnoTacaka)
            {
                prodjeneTacke = 0;
                krug++;
            }
            sistemPozicijaV2.postavljanjeSledeceTacke(brojKola, prodjeneTacke);     // Prosledjivanje informacija o jedinstvenom broju auta i njegovim predjenim tackama
        }
    }
    public void unistiBota()
    {
        if (pozicijaAuta == 0)
        {
            Destroy(gameObject);
        }
    }
}
