using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIKontroler : MonoBehaviour
{
    [SerializeField] private Transform pozicijaMeteTransform;   // Transform tacke koje bot treba da prati
    private Vector3 pozicijaMete;       // Vektor pozicije tacke koje bot treba da prati 
    public AIPutLista aIPutLista;       
    public List<Transform> put;         // Lista tacaka koje bot treba da prati
    public int trenutni;                // Tacka koja je na redu da se prati
    public AIAutoKontola aiAutoKontola;
    public float daljinaPromene;        // Daljina od tacke koja odredjuje kad se prebacuje na sledecu tacku
    public float kocenje = 0f;          // Komanda kocenja
    public float kretanje = 0f;         // Komanda kretanja napred ili nazad
    public float skretanje = 0f;        // Komanda skretanja
    public float brzina;                // Brzina kojom se bot krece
    public bool autoUZoni;              // Provera da li je auto u zoni kocenja
    public GameObject farovi;           
    public GameObject stopSvetlo;
    public GameObject rikvercSvetlo;
    public bool zaglavljen = false;     // Provera da li je auto zaglavljen
    public bool gotovo = false;         // Provera da li je odbrojano 5 sekundi
    void Awake()
    { 
        aiAutoKontola = GetComponent<AIAutoKontola>();
        put = aIPutLista.put;   // Primanje liste tacaka koje bot treba da prati
        trenutni = 0;
        autoUZoni = false;
        StartCoroutine(Brojac());   // Inicijalizacija brojaca
    }
    void Update()
    {
        PostaviMetu(pozicijaMeteTransform.position);

        // Dobijanje brzine
        brzina = aiAutoKontola.brzina;

        // Racunanje u kom smeru se nalazi tacka koja treba da se prati u odnosu na bota
        float daljinaOdMete = Vector3.Distance(transform.position, pozicijaMete);
        Vector3 smerKaPozicijiMete = (pozicijaMete - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, smerKaPozicijiMete);
        //Debug.Log("Kretanje: " + dot);
        // Ako se auto zaglavio, zapocinjanje procesa odglavljivanja
        if (brzina < 1 && zaglavljen == false && gotovo == true)
        {
            StartCoroutine(Rikverc());
        }

        // Davanje komandi na osnovu pozicije tacke u odnosu na bota
        else if (zaglavljen==false)
        {
            if (dot > 0 && autoUZoni == false)
            {
                kretanje = 1f;
                kocenje = 0f;
                stopSvetlo.SetActive(false);
            }
            else if (autoUZoni == true)
            {
                kretanje = 0f;
                kocenje = 1f;
                stopSvetlo.SetActive(true);
            }
            else
            {
                float daljinaUnazad = 15f;
                if (daljinaOdMete > daljinaUnazad)
                {
                    kretanje = 1f;
                    rikvercSvetlo.SetActive(false);
                }
                else
                {
                    kretanje = -1f;
                    rikvercSvetlo.SetActive(true);
                }
            }

            float ugaoKretanja = Vector3.SignedAngle(transform.forward, smerKaPozicijiMete, Vector3.up);
            if (ugaoKretanja > 0)
            {
                skretanje = ugaoKretanja / 70f;
            }
            else
            {
                skretanje = ugaoKretanja / 70f;
            }
            //Debug.Log("Skretanje: " + ugaoKretanja);
        }

        // Prosledjivanje komandi botu
        aiAutoKontola.Kontrole(kretanje, skretanje, kocenje);

        // Ako se bot dovoljno priblizio tacki, pocinje da prati sledecu
        if (Vector3.Distance(put[trenutni].position, transform.position) < daljinaPromene)
        {
            trenutni++;
            // Ako je dosao do poslednje tacke, pocinje da prati pocetnu tacku
            if (trenutni == put.Count) trenutni = 0;
            pozicijaMeteTransform = put[trenutni];
        }
            
        // Iscrtavanje linije izmedju bota i tacke koju prati trenutno
        Debug.DrawRay(transform.position, put[trenutni].position - transform.position, Color.yellow);
    }

    // Pokusaj odglavljivanja
    IEnumerator Rikverc()
    {
        yield return new WaitForSeconds(1f);
        if (brzina < 1)
        {
            zaglavljen = true;
            kretanje = -1f;
            skretanje = -1f * skretanje;
            yield return new WaitForSeconds(3f);
            zaglavljen = false;
        }
        else zaglavljen = false;
    }
    IEnumerator Brojac()
    {
        yield return new WaitForSeconds(5f);
        gotovo = true;
    }

    // Postavljanje tacke za pracenje
    public void PostaviMetu(Vector3 pozicijaMete)
    {
        this.pozicijaMete = pozicijaMete;
    }

    void OnTriggerEnter(Collider other)
    {
        // Paljenje i gasenje farova
        if (other.gameObject.CompareTag("FaroviPali"))
        {
            farovi.SetActive(true);
        }
        if (other.gameObject.CompareTag("FaroviGasi"))
        {
            farovi.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        // Prilagodjavanje brzine u odredjenim zonama kocenja i indikacija da je auto u zoni
        if (other.gameObject.CompareTag("Zona (1)"))
        {
            if (brzina > 40f)
                autoUZoni = true;
            else
            {
                autoUZoni = false;
            }
        }
        else if (other.gameObject.CompareTag("Zona (2)"))
        {
            if (brzina > 30f)
                autoUZoni = true;
            else
            {
                autoUZoni = false;
            }
        }
        else if (other.gameObject.CompareTag("Zona (3)"))
        {
            if (brzina > 25f)
                autoUZoni = true;
            else
            {
                autoUZoni = false;
            }
        }
        else if (other.gameObject.CompareTag("Zona (4)"))
        {
            if (brzina > 45f)
                autoUZoni = true;
            else
            {
                autoUZoni = false;
            }
        }
        else if (other.gameObject.CompareTag("Zona (5)"))
        {
            if (brzina > 20f)
                autoUZoni = true;
            else
            {
                autoUZoni = false;
            }
        }
        else
        {
            autoUZoni = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Indikacija da je auto izasao iz zone i da prestane da koci
        if (other.gameObject.CompareTag("Zona (1)"))
        {
            autoUZoni = false;
        }
        else if (other.gameObject.CompareTag("Zona (2)"))
        {
            autoUZoni = false;
        }
        else if (other.gameObject.CompareTag("Zona (3)"))
        {
            autoUZoni = false;
        }
        else if (other.gameObject.CompareTag("Zona (4)"))
        {
            autoUZoni = false;
        }
        else if (other.gameObject.CompareTag("Zona (5)"))
        {
            autoUZoni = false;
        }
    }
}
