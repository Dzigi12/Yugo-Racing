using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SistemPozicijaV2 : MonoBehaviour
{
    public GameObject tacka;                //Objekat tacke
    public GameObject drzacTacaka;          //Prazan objekat koji je roditelj svih tacaka
    public GameObject trenutneTacke;        //Tacka koja je na redu
    public GameObject panelPozicija;
    public GameObject[] Kola;               //Lista auta
    public Transform[] pozicijaTacaka;      //Lista pozicija svih tacaka na stazi
    public GameObject[] tackaZaSvakiAuto;   //Lista trenutnih tacaka za svaki auto
    public GameObject[] drzacBotova;        //Drzac 

    public AutoSistemPozicija autoSistemPozicija;   //Referenca na skriptu

    public TextMeshProUGUI igrac;           //UI tekst pozicije igraca
    public TextMeshProUGUI bot1;            //UI tekst pozicije bota
    public TextMeshProUGUI bot2;            //UI tekst pozicije bota
    public TextMeshProUGUI bot3;            //UI tekst pozicije bota

    public int ukupnoKola;                 //Ukupan broj kola u trci
    public int ukupnoTacaka;               //Ukupan broj svih tacaka u trci
    private int brojac;                    //Brojac pozicije  
    public int kolicinaBotova;             //Ukupan broj botova
    public float pozicijaPanela;
    public bool gotovo = false;
    void Awake()
    {
        kolicinaBotova = PlayerPrefs.GetInt("KolicinaBotova");  //Preuzimanje informacije o kolicini botova
        Kola = new GameObject[kolicinaBotova+1];                //Postavljanje velicine niza
        Kola[0] = GameObject.FindGameObjectWithTag("Player");   
        for (int i = 1; i < kolicinaBotova+1; i++)
        {
            Kola[i] = drzacBotova[i-1];
        }
        ukupnoKola = Kola.Length;                           //Dobijanje ukupnog broja kola
        ukupnoTacaka = drzacTacaka.transform.childCount;    //Dobijanje ukupnog broja tacaka na stazi

        //Automatsko postavljanje jedinstvenog broja za svaki auto, igrac je uvek 0
        postaviBrojKola();

        //Uzimanje liste pozicija svih tacaka, njihovo ime i sloj
        postaviTacke();
    }
    void FixedUpdate()
    {   
        //Odredjivanje pozicije svakog auta u trci
        for(int i = 0; i < ukupnoKola; i++)
        {
            brojac = 0;
            for (int j = 0; j < ukupnoKola; j++) 
            {
                //Uporedjivanje [i] sa [j] autom, da li ima vise zavrsenih krugova
                if (Kola[i].GetComponent<AutoSistemPozicija>().krug > Kola[j].GetComponent<AutoSistemPozicija>().krug)
                {
                    brojac++;
                }
                //U slucaju da [i] i [j] auto imaju isti broj krugova, uporedjuju se predjene tacke
                else if (Kola[i].GetComponent<AutoSistemPozicija>().krug == Kola[j].GetComponent<AutoSistemPozicija>().krug)
                {
                    //Uporedjivanje [i] sa [j] autom, da li je presao vise tacaka
                    if (Kola[i].GetComponent<AutoSistemPozicija>().prodjeneTacke > Kola[j].GetComponent<AutoSistemPozicija>().prodjeneTacke)
                    {
                        brojac++;
                    }
                    //U slucaju da [i] i [j] auto imaju isti broj tacaka, meri se udaljenost do sledece tacke
                    else if (Kola[i].GetComponent<AutoSistemPozicija>().prodjeneTacke == Kola[j].GetComponent<AutoSistemPozicija>().prodjeneTacke)
                    {
                        //Uzimanje vrednosti udaljenosti [i] i [j] auta u odnosu na sledecu tacku
                        float distancaI = Vector3.Distance(Kola[i].transform.position, pozicijaTacaka[Kola[i].GetComponent<AutoSistemPozicija>().prodjeneTacke].transform.position);
                        float distancaJ = Vector3.Distance(Kola[j].transform.position, pozicijaTacaka[Kola[j].GetComponent<AutoSistemPozicija>().prodjeneTacke].transform.position);
                        if (distancaI < distancaJ)
                        {
                            brojac++;
                        }
                    }
                }
            }

            Kola[i].GetComponent<AutoSistemPozicija>().pozicijaAuta = ukupnoKola - brojac;  //Dodeljivanje pozicije svakom autu

            //Upravljanje UI elementima za pozicije

            panelPozicija.GetComponent<RectTransform>().sizeDelta = new Vector2(248, 66 - (15 * (4 - (ukupnoKola))));

            if (i == 0)
            {
                float y = (-22+(7*(4 - ukupnoKola))) + (15 * (ukupnoKola-((ukupnoKola - brojac))));
                igrac.GetComponent<RectTransform>().anchoredPosition = new Vector2(-16, y);
                igrac.text = ukupnoKola-brojac + ". Igrac";
            }
            if (i == 1)
            {
                float y = (-22 + (7 * (4 - ukupnoKola))) + (15 * (ukupnoKola - ((ukupnoKola - brojac))));
                bot1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-16, y);
                bot1.text = ukupnoKola - brojac + ". Bot1";
            }
            if (i == 2)
            {
                float y = (-22 + (7 * (4 - ukupnoKola))) + (15 * (ukupnoKola - ((ukupnoKola - brojac))));
                bot2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-16, y);
                bot2.text = ukupnoKola - brojac + ". Bot2";
            }   else if (kolicinaBotova - 2 < -1) { bot2.text = ""; }
            if (i == 3)
            {
                float y = (-22 + (7 * (4 - ukupnoKola))) + (15 * (ukupnoKola - ((ukupnoKola - brojac))));
                bot3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-16, y);
                bot3.text = ukupnoKola - brojac + ". Bot3";
            }   else if (kolicinaBotova - 3 < -1) { bot2.text = ""; }
        }
    }

    //Uzimanje liste pozicija svih tacaka, njihovo ime i sloj
    void postaviTacke()
    {
        //Lista koja ce da sadrzi poziciju svake tacke na stazi
        pozicijaTacaka = new Transform[ukupnoTacaka];

        //Dobijanje pozicije svake tacke
        for (int i = 0; i < ukupnoTacaka; i++)
        {
            pozicijaTacaka[i] = drzacTacaka.transform.GetChild(i).transform;
        }

        //Lista koja ce da sadrzi informaciju o sledecoj tacki svakog auta
        tackaZaSvakiAuto = new GameObject[ukupnoKola];

        //Postavljanje pocetne tacke, njihovo ime i sloj za svaki auto
        for (int i = 0; i < ukupnoKola; i++)
        {
            tackaZaSvakiAuto[i] = Instantiate(tacka, pozicijaTacaka[0].position, pozicijaTacaka[0].rotation, trenutneTacke.transform);
            tackaZaSvakiAuto[i].name = "Tacka " + i;
            tackaZaSvakiAuto[i].layer = 9 + i;
        }
    }

    //Postavljanje naredne tacke za svaki auto kad dodje u kontakt sa istom
    public void postavljanjeSledeceTacke(int brojKola, int brojTacaka)  //Primanje informacija o jedinstvenom broju auta i njegovim predjenim tackama
    {
        tackaZaSvakiAuto[brojKola].transform.position = pozicijaTacaka[brojTacaka].transform.position;  //Postavljanje pozicije tacke
        tackaZaSvakiAuto[brojKola].transform.rotation = pozicijaTacaka[brojTacaka].transform.rotation;  //Postavljanje rotacije tacke
    }

    //Automatsko postavljanje jedinstvenog broja za svaki auto, igrac je uvek 0
    public void postaviBrojKola()
    {
        for (int i = 0; i < ukupnoKola; i++)
        {
            Kola[i].GetComponent<AutoSistemPozicija>().brojKola = i;
            Kola[i].GetComponent<AutoSistemPozicija>().pozicijaAuta = 1;
        }
        gotovo = true;
    }
}
