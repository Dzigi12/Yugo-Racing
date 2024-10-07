using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NazadUMeni : MonoBehaviour
{
    public string imeScene;
    public GameObject auto;

    public void Awake()
    {
        auto = GameObject.FindGameObjectWithTag("Player");
    }
    public void Nazad()
    {
        StartCoroutine(LoadYourAsyncScene());
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
        auto.GetComponent<AutoSistemPozicija>().enabled = false;
        auto.GetComponent<ZvukMotora>().enabled = false;
        auto.GetComponent<Rigidbody>().useGravity = false;
        auto.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        auto.transform.position = new Vector3(-1.05f, -0.57f, 10.02f);
        auto.transform.rotation = Quaternion.Euler(0, 230, 0);
    }
}
