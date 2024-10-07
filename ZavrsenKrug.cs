using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ZavrsenKrug : MonoBehaviour
{
    public GameObject kraj;

    public TextMeshProUGUI NajMinut;        // UI element najboljeg minuta
    public TextMeshProUGUI NajSekund;       // UI element najbolje sekunde
    public TextMeshProUGUI NajMilisekund;   // UI element najbolje milisekunde
    public TextMeshProUGUI BrojacKruga;     // UI element zavrsenih krugova
    public static int GotovKrug = 0;
    public GameObject igrac;

    public float Vreme;

    private void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            GotovKrug = igrac.GetComponent<AutoSistemPozicija>().krug;
            // Ako je igrac zavrsio treci krug, pokrece se scenario podiuma
            if (GotovKrug == 3)
            {
                kraj.GetComponent<Kraj>().enabled = true;
            }
            // Uzimanje vremena na kraju kruga
            Vreme = PlayerPrefs.GetFloat("Vreme");

            // Provera da li je vreme trenutnog kruga bolje od proslog
            if ((VremeKruga.Vreme <= Vreme && GotovKrug > 0) || GotovKrug == 1)
            {
                if (VremeKruga.SekundeBrojac <= 9)
                {
                    NajSekund.text = "0" + VremeKruga.SekundeBrojac + ":";
                }
                else
                {
                    NajSekund.text = "" + VremeKruga.SekundeBrojac + ":";
                }

                if (VremeKruga.MinutBrojac <= 9)
                {
                    NajMinut.text = "0" + VremeKruga.MinutBrojac + ":";
                }
                else
                {
                    NajMinut.text = "" + VremeKruga.MinutBrojac + ":";
                }

                NajMilisekund.text = "" + VremeKruga.MilisekundeBrojac.ToString("f0");

                //Debug.Log(VremeKruga.MinutBrojac + " " + VremeKruga.SekundeBrojac + " " + VremeKruga.MilisekundeBrojac);

                // Belezenje najboljeg vremena kruga
                PlayerPrefs.SetFloat("Vreme", VremeKruga.Vreme);
                // Vracanje vremena kruga na 0
                VremeKruga.MinutBrojac = 0;
                VremeKruga.SekundeBrojac = 0;
                VremeKruga.MilisekundeBrojac = 0;
                VremeKruga.Vreme = 0;
                // Ispis UI elementa kruga
                BrojacKruga.text = "" + GotovKrug;
            }
        }
    }
}
