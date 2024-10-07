using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PremestiAutoUIgru : MonoBehaviour
{
    public string imeScene;
    public int kolicinaBotova;
    public Slider slider;
    public GameObject greska;
    public GameObject auto;

    public void Awake()
    {
        auto = GameObject.FindGameObjectWithTag("Player");
    }

    // Odabir staza
    public void Staza1()
    {
        imeScene = "Staza01";
    }
    public void Staza2()
    {
        imeScene = "Staza02";
    }
    public void Staza3()
    {
        imeScene = "Staza03";
    }
    public void Potvrdi()
    {
        // Ako staza nije odabrana, izbaci gresku
        if (imeScene == "null")
        {
            StartCoroutine(Greska());
        }
        else
        {
            // Ako je staza odabrana povlaci vrednost slajdera i zapocinje ucitavanje odabrane staze
            kolicinaBotova = ((int)slider.value);
            PlayerPrefs.SetInt("KolicinaBotova", kolicinaBotova);
            StartCoroutine(LoadYourAsyncScene());
        }
    }
    IEnumerator Greska()
    {
        greska.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        greska.SetActive(false);
    }
    IEnumerator LoadYourAsyncScene()
    {
        // Postavljanje trenutne scene
        Scene trenutnaScena = SceneManager.GetActiveScene();

        // Ucitavanje scene u pozadini
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(imeScene, LoadSceneMode.Additive);

        // Sacekaj da se prethodna funkcija zavrsi
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Pomeranje objekta u zeljenu scenu
        SceneManager.MoveGameObjectToScene(auto, SceneManager.GetSceneByName(imeScene));
        // Brisanje prosle scene
        SceneManager.UnloadSceneAsync(trenutnaScena);
        // Konfigurisanje igraca
        auto.GetComponent<AutoSistemPozicija>().enabled = true;
        auto.GetComponent<AutoKontrola>().enabled = true;
        auto.GetComponent<ZvukMotora>().enabled = true;
        auto.GetComponent<Rigidbody>().useGravity = true;
        auto.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        auto.GetComponent<AutoKontrola>().Start();
        GameObject.FindGameObjectWithTag("IgracKocka").layer = 9;
    }
}
