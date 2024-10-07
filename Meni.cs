using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Meni : MonoBehaviour
{
    //Ucitavanje scena
    public void autoSelekcija()
    {
        SceneManager.LoadScene(2);
    }

    public void nazad()
    {
        SceneManager.LoadScene(0);
    }

    public void izadji()
    {
        Application.Quit();
    }
}
