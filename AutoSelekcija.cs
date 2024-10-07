using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoSelekcija : MonoBehaviour
{
    public int brojac=0;
    public Spojler spojler; // Boja spojlera
    public Material Crvena; // Boja auta
    public Material Plava;  // Boja auta
    public Material Zuta;   // Boja auta
    public GameObject[] auti;

    // Prikaz sledeceg auta u nizu i nasledjivanje rotacije
    public void Napred() 
    {
        if (brojac == 1) 
        { 
            brojac = 0;
            auti[brojac].SetActive(true);
            auti[brojac + 1].SetActive(false);
            auti[brojac].transform.rotation = auti[brojac + 1].transform.rotation;
        }
        else
        {
            brojac++;
            auti[brojac].SetActive(true);
            auti[brojac - 1].SetActive(false);
            auti[brojac].transform.rotation = auti[brojac + -1].transform.rotation;
        }
    }

    // Prikaz prethodnog auta u nizu i nasledjivanje rotacije
    public void Nazad() 
    {
        if (brojac == 0)
        {
            brojac = 1;
            auti[brojac].SetActive(true);
            auti[brojac - 1].SetActive(false);
            auti[brojac].transform.rotation = auti[brojac - 1].transform.rotation;
        }
        else
        {
            brojac--;
            auti[brojac].SetActive(true);
            auti[brojac + 1].SetActive(false);
            auti[brojac].transform.rotation = auti[brojac + 1].transform.rotation;
        }
    }
    
    public void Boja_Plava () //Menjanje boje auta i spojlera u boju auta
    {
        auti[brojac].GetComponent<Renderer>().material = Plava;
        spojler.meshRendererSpojleraYugo.material = Plava;
    }
    public void Boja_Crvena()
    {
        auti[brojac].GetComponent<Renderer>().material = Crvena;
        spojler.meshRendererSpojleraYugo.material = Crvena;
    }
    public void Boja_Zuta()
    {
        auti[brojac].GetComponent<Renderer>().material = Zuta;
        spojler.meshRendererSpojleraYugo.material = Zuta;
    }
}

