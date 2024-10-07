using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PomeranjeAuta : MonoBehaviour
{
    public string imeScene;
    public GameObject auto;
    public AutoSelekcija autoSelekcija;

    public void Potvrdi()
    {
        auto = autoSelekcija.auti[autoSelekcija.brojac];
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

        // Pomeranje objekata u zeljenu scenu
        SceneManager.MoveGameObjectToScene(auto, SceneManager.GetSceneByName(imeScene));
        // Brisanje prosle scene
        SceneManager.UnloadSceneAsync(trenutnaScena);
        // Konfigurisanje odabranog auta
        auto.transform.position = new Vector3(-1.05f, -0.57f, 10.02f);
        auto.transform.rotation = Quaternion.Euler(0, 230, 0);
        auto.GetComponent<RotacijaAuta>().enabled = false;
    }
}
