using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Kraj : MonoBehaviour
{
    public GameObject poz1;         // Pozicija 1 na podiumu
    public GameObject poz2;         // Pozicija 2 na podiumu
    public GameObject poz3;         // Pozicija 3 na podiumu
    public GameObject krajTekst;    // Tekst na kraju trke
    public TextMeshPro poz1Tekst;   // Tekst iznad auta na prvoj poziciji na podiumu
    public TextMeshPro poz2Tekst;   // Tekst iznad auta na drugoj poziciji na podiumu
    public TextMeshPro poz3Tekst;   // Tekst iznad auta na trecoj poziciji na podiumu
    public SistemPozicijaV2 sistemPozicijaV2;
    float kretanje = 0f;
    float skretanje = 0f;
    float kocenje = 1f;
    int a = 0;

    private void Update()
    {
        if (a == 0)
        {
            StartCoroutine(Brojac());
        }
    }

    // Ispis teksta za kraj trke
    public IEnumerator Brojac()
    {
        krajTekst.SetActive(true);
        yield return new WaitForSeconds(3f);
        krajTekst.SetActive(false);
        Podium();
        this.GetComponent<KrajTrke>().enabled = true;
        a = 1;
    }

    // Inicijalizacija podiuma, postavljanje ucesnika i njihovih imena
    public void Podium()
    {
        for (int i = 0; i < sistemPozicijaV2.ukupnoKola; i++)
        {
            if (sistemPozicijaV2.Kola[i].CompareTag("Player"))
            {
                sistemPozicijaV2.Kola[i].GetComponent<AutoKontrola>().Koči();
                sistemPozicijaV2.Kola[i].GetComponent<AutoKontrola>().enabled = false;
            }
            else
            {
                sistemPozicijaV2.Kola[i].GetComponent<AIKontroler>().enabled = false;
                sistemPozicijaV2.Kola[i].GetComponent<AIAutoKontola>().Kontrole(kretanje, skretanje, kocenje);
            }
        }
        for (int i = 0; i < sistemPozicijaV2.ukupnoKola; i++)
        {
            if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().pozicijaAuta == 1)
            {
                sistemPozicijaV2.Kola[i].transform.position = poz1.transform.position;
                sistemPozicijaV2.Kola[i].transform.rotation = poz1.transform.rotation;
                sistemPozicijaV2.Kola[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola == 0)
                {
                    poz1Tekst.text = "Igrac";
                }
                else
                {
                    poz1Tekst.text = "Bot" + sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola;
                }
            }
            if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().pozicijaAuta == 2)
            {
                sistemPozicijaV2.Kola[i].transform.position = poz2.transform.position;
                sistemPozicijaV2.Kola[i].transform.rotation = poz2.transform.rotation;
                sistemPozicijaV2.Kola[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola == 0)
                {
                    poz2Tekst.text = "Igrac";
                }
                else
                {
                    poz2Tekst.text = "Bot" + sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola;
                }
            }
            if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().pozicijaAuta == 3)
            {
                sistemPozicijaV2.Kola[i].transform.position = poz3.transform.position;
                sistemPozicijaV2.Kola[i].transform.rotation = poz3.transform.rotation;
                sistemPozicijaV2.Kola[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola == 0)
                {
                    poz3Tekst.text = "Igrac";
                }
                else
                {
                    poz3Tekst.text = "Bot" + sistemPozicijaV2.Kola[i].GetComponent<AutoSistemPozicija>().brojKola;
                }
            }
        }
        sistemPozicijaV2.enabled = false;
    }
}
