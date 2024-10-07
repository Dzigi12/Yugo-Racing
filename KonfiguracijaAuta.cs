using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonfiguracijaAuta : MonoBehaviour
{
    public GameObject glavniEkran;
    public GameObject boje;
    public GameObject felne;
    public GameObject spojler;

    // Inicijalizacija ekrana biranja boje
    public void Sakri()
    {
        glavniEkran.SetActive(false);
        boje.SetActive(true);
    }

    // Inicijalizacija povratka na glavni ekran
    public void Potvrdi()
    {
        glavniEkran.SetActive(true);
        boje.SetActive(false);
        felne.SetActive(false);
        spojler.SetActive(false);
    }

    // Inicijalizacija ekrana biranja felni
    public void Felne()
    {
        glavniEkran.SetActive(false);
        felne.SetActive(true);
    }

    // Inicijalizacija ekrana biranja spojlera
    public void Spojler()
    {
        glavniEkran.SetActive(false);
        spojler.SetActive(true);
    }
}
