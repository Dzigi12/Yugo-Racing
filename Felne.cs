using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felne : MonoBehaviour
{
    public MeshFilter[] meshFilterTockova;      
    public MeshRenderer[] meshRendererTockova;
    public MeshFilter[] meshFilterTockovaStojadin;
    public MeshRenderer[] meshRendererTockovaStojadin;
    public AutoSelekcija autoSelekcija;
    public Material materialTockova;
    [SerializeField] Mesh[] mesheviTockova;
    public GameObject kamera;
    public GameObject auto;
    public Spojler spojler;
    private Vector3 brzina = Vector3.zero;
    public int opcija;
    public int brojac = 0;

    // Prikaz ekrana za felne
    public void Kamera()
    {
        auto.GetComponent<RotacijaAuta>().enabled = false;
        opcija = 1;
        spojler.opcija = 0;
    }

    // Povratak na glavni ekran
    public void Potvrdi()
    {
        auto.GetComponent<RotacijaAuta>().enabled = true;
        opcija = 2;
    }
    public void Update()
    {
        // Postavljanje kamere prilikom odabira ekrana za felne
        if (opcija == 1)
        {
            // Ako je u pitanju yugo
            if (autoSelekcija.brojac == 0)
            {
                Vector3 pozicija1 = new Vector3(-7.7f, -0.68f, 4.64f);
                kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicija1, ref brzina, 0.5f);
                kamera.transform.rotation = Quaternion.Euler(3.2f, 22.7f, 0f);
                StartCoroutine(RotirajAuto());
            }
            // Ako je u pitanju stojadin
            else if (autoSelekcija.brojac == 1)
            {
                Vector3 pozicija1 = new Vector3(-7.9f, -0.54f, 5.77f);
                kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicija1, ref brzina, 0.5f);
                kamera.transform.rotation = Quaternion.Euler(6.9f, 23.6f, 0f);
                StartCoroutine(RotirajAuto());
            }
        }

        // Vracanje kamere na mesto prilikom povratka na glavni ekran
        else if (opcija == 2)
        {
            Vector3 pozicija2 = new Vector3(-9.4f, 0.56f, -0.6f);
            kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicija2, ref brzina, 0.5f);
            kamera.transform.rotation = Quaternion.Euler(6.872f, 27.609f, 0.53f);
        }
    }


    // Rotiranje auta prilikom odabira ekrana za felne
    IEnumerator RotirajAuto()
    {
        float brzina = 0.0003f;
        while (autoSelekcija.auti[autoSelekcija.brojac].transform.rotation.y <= 290 && opcija == 1)
        {
            autoSelekcija.auti[autoSelekcija.brojac].transform.rotation = Quaternion.Slerp(autoSelekcija.auti[autoSelekcija.brojac].transform.rotation, Quaternion.Euler(0, 290, 0), brzina * Time.time);
            yield return null;
        }
        autoSelekcija.auti[autoSelekcija.brojac].transform.rotation = Quaternion.Euler(0, 290, 0);
        yield return null;
    }

    // Prolazenje kroz niz felni unapred
    public void Sledeci()
    {
        if (brojac == 3) 
        { 
            brojac = 0;
            if(autoSelekcija.brojac == 0)
            {
                    MenjajTockoveYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajTockoveStojadin();
            }
        }
        else 
        {
            brojac++;
            if (autoSelekcija.brojac == 0)
            {
                    MenjajTockoveYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajTockoveStojadin();
            }
        }
    }

    // Prolazenje kroz niz felni unazad
    public void Prethodni()
    {
        if (brojac == 0)
        {
            brojac = 3;
            if (autoSelekcija.brojac == 0)
            {
                    MenjajTockoveYugo();
            }
            else if  (autoSelekcija.brojac == 1)
            {
                MenjajTockoveStojadin();
            }
        }
        else
        {
            brojac--;
            if (autoSelekcija.brojac == 0)
            {
                MenjajTockoveYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajTockoveStojadin();
            }
        }
    }

    // Promena felna yuga
    public void MenjajTockoveYugo()
    {
        for (int i = 0; i < 4; i++)
        {
            meshRendererTockova[i].material = materialTockova;
            meshFilterTockova[i].mesh = mesheviTockova[brojac];
        }
    }

    // Promena felna stojadina
    public void MenjajTockoveStojadin()
    {
        for (int i = 0; i < 4; i++)
        {
            meshRendererTockovaStojadin[i].material = materialTockova;
            meshFilterTockovaStojadin[i].mesh = mesheviTockova[brojac];
        }
    }
}
