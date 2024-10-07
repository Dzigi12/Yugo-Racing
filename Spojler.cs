using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spojler : MonoBehaviour
{
    public GameObject kamera;                           // Kamera u sceni
    public GameObject auto;                             // 
    public Felne felne;                                 // Referenca na skriptu
    private Vector3 brzina = Vector3.zero;              //
    public MeshFilter meshFilterSpojleraYugo;           // Mesh Filter spojlera Yuga
    public MeshRenderer meshRendererSpojleraYugo;       // Mesh Render spojlera Yuga
    public MeshFilter meshFilterSpojleraStojadin;       // Mesh Filter spojlera Stojadina
    public MeshRenderer meshRendererSpojleraStojadin;   // Mesh Render spojlera Stojadina
    public AutoSelekcija autoSelekcija;                 // Referenca na skriptu
    public Material materialSpojlera;                   // Materijal spojlera
    [SerializeField] Mesh[] mesheviSpojleraYugo;        // Mesh spojlera Yuga 
    [SerializeField] Mesh[] mesheviSpojleraStojadin;    // Mesh spojlera Stojadina
    public int brojac = 0;                              // Brojac niza spojlera
    public int opcija; 
    
    // Prikaz ekrana za spojler
    public void Kamera()
    {
        auto.GetComponent<RotacijaAuta>().enabled = false;
        opcija = 1;
        felne.opcija = 0;
    }
    // Povratak na glavni ekran
    public void Potvrdi()
    {
        auto.GetComponent<RotacijaAuta>().enabled = true;
        opcija = 2;
    }
    public void Update()
    {
        // Postavljanje kamere prilikom odabira ekrana za spojlere
        if (opcija == 1)
        {
            // Ako je u pitanju yugo
            if (autoSelekcija.brojac==0) 
            {
                Vector3 pozicija1 = new Vector3(-2, 1.5f, 6);
                kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicija1, ref brzina, 0.5f);
                kamera.transform.rotation = Quaternion.Euler(16f, 21, 0f);
                StartCoroutine(RotirajAuto());
            }
            // Ako je u pitanju stojadin
            else if (autoSelekcija.brojac==1)
            {
                Vector3 pozicija1 = new Vector3(-2.8f, 1.5f, 6.3f);
                kamera.transform.position = Vector3.SmoothDamp(kamera.transform.position, pozicija1, ref brzina, 0.5f);
                kamera.transform.rotation = Quaternion.Euler(16f, 21, 0f);
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
    // Rotiranje auta prilikom odabira ekrana za spojler
    IEnumerator RotirajAuto()
    {
        float brzina = 0.0003f;
        while (autoSelekcija.auti[autoSelekcija.brojac].transform.rotation.y <= 250 && opcija == 1)
        {
            autoSelekcija.auti[autoSelekcija.brojac].transform.rotation = Quaternion.Slerp(autoSelekcija.auti[autoSelekcija.brojac].transform.rotation, Quaternion.Euler(0, 250, 0), brzina * Time.time);
            yield return null;
        }
        autoSelekcija.auti[autoSelekcija.brojac].transform.rotation = Quaternion.Euler(0, 250, 0);
        yield return null;
    }
    // Prolazenje kroz niz spojlera unapred
    public void Sledeci()
    {
        if (brojac == 4)
        {
            brojac = 0;
            if (autoSelekcija.brojac == 0)
            {
                MenjajSpojlerYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajSpojlerStojadin();
            }
        }
        else
        {
            brojac++;
            if (autoSelekcija.brojac == 0)
            {
                MenjajSpojlerYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajSpojlerStojadin();
            }
        }
    }
    // Prolazenje kroz niz spojlera unazad
    public void Prethodni()
    {
        if (brojac == 0)
        {
            brojac = 4;
            if (autoSelekcija.brojac == 0)
            {
                MenjajSpojlerYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajSpojlerStojadin();
            }
        }
        else
        {
            brojac--;
            if (autoSelekcija.brojac == 0)
            {
                MenjajSpojlerYugo();
            }
            else if (autoSelekcija.brojac == 1)
            {
                MenjajSpojlerStojadin();
            }
        }
    }
    // Menjanje spojlera Yuga
    public void MenjajSpojlerYugo()
    {
        meshRendererSpojleraYugo.material = autoSelekcija.auti[autoSelekcija.brojac].GetComponent<Renderer>().material;
        meshFilterSpojleraYugo.mesh = mesheviSpojleraYugo[brojac];
    }
    // Menjanje spojlera Stojadina
    public void MenjajSpojlerStojadin()
    {
        meshRendererSpojleraStojadin.material = autoSelekcija.auti[autoSelekcija.brojac].GetComponent<Renderer>().material;
        meshFilterSpojleraStojadin.mesh = mesheviSpojleraStojadin[brojac];
    }
}
