using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slajder : MonoBehaviour
{
    public Slider brzina;
    public Slider kocenje;
    public Slider ubrzanje;
    float vrednostBrzine;   // Vrednost slajdera brzine
    float vrednostKocenja;  // Vrednost slajdera kocenja
    float vrednostUbrzanja; // Vrednost slajdera ubrzanja
    public TextMeshProUGUI brzina_text;
    public TextMeshProUGUI kocenje_text;
    public TextMeshProUGUI ubrzanje_text;
    public TextMeshProUGUI ime_text;
    public GameObject Yugo;
    public GameObject Stojadin;
    void Update()
    {
        // Inicijalizacija slajdera yuga
        if (Yugo.activeSelf == true)
        {
            ime_text.text = "Yugo";
            if (brzina.value < 120)
            {
                vrednostBrzine += 50f * Time.deltaTime;
                brzina.value = vrednostBrzine;
                brzina_text.text = ((int)vrednostBrzine).ToString() + "km/h";
            }
            else if (brzina.value >= 121)
            {
                vrednostBrzine -= 50f * Time.deltaTime;
                brzina.value = vrednostBrzine;
                brzina_text.text = ((int)vrednostBrzine).ToString() + "km/h";
            }

            if (kocenje.value < 70)
            {
                vrednostKocenja += 50f * Time.deltaTime;
                kocenje.value = vrednostKocenja;
                kocenje_text.text = ((int)vrednostKocenja).ToString();
            }
            else if (kocenje.value >= 71)
            {
                vrednostKocenja -= 50f * Time.deltaTime;
                kocenje.value = vrednostKocenja;
                kocenje_text.text = ((int)vrednostKocenja).ToString();
            }

            if (ubrzanje.value < 12)
            {
                vrednostUbrzanja += 10f * Time.deltaTime;
                ubrzanje.value = vrednostUbrzanja;
                ubrzanje_text.text = ((int)vrednostUbrzanja).ToString() + "s";
            }
        }
        
        // Inicijalizacija slajdera stojadina
        if (Stojadin.activeSelf == true)
        {
            ime_text.text = "Stojadin";
            if (brzina.value < 100)
            {
                vrednostBrzine += 50f * Time.deltaTime;
                brzina.value = vrednostBrzine;
                brzina_text.text = ((int)vrednostBrzine).ToString() + "km/h";
            }
            else if (brzina.value >= 101)
            {
                vrednostBrzine -= 50f * Time.deltaTime;
                brzina.value = vrednostBrzine;
                brzina_text.text = ((int)vrednostBrzine).ToString() + "km/h";
            }

            if (kocenje.value < 85)
            {
                vrednostKocenja += 50f * Time.deltaTime;
                kocenje.value = vrednostKocenja;
                kocenje_text.text = ((int)vrednostKocenja).ToString();
            }
            else if (kocenje.value >= 86)
            {
                vrednostKocenja -= 50f * Time.deltaTime;
                kocenje.value = vrednostKocenja;
                kocenje_text.text = ((int)vrednostKocenja).ToString();
            }

            if (ubrzanje.value < 10)
            {
                vrednostUbrzanja += 10f * Time.deltaTime;
                ubrzanje.value = vrednostUbrzanja;
                ubrzanje_text.text = ((int)vrednostUbrzanja).ToString() + "s";
            }
            else if (ubrzanje.value >= 11)
            {
                vrednostUbrzanja -= 10f * Time.deltaTime;
                ubrzanje.value = vrednostUbrzanja;
                ubrzanje_text.text = ((int)vrednostUbrzanja).ToString() + "s";
            }
        }
    }
}

