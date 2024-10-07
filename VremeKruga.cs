using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VremeKruga : MonoBehaviour
{
    public static int MinutBrojac;
    public static int SekundeBrojac;
    public static float MilisekundeBrojac;

    public static string MilisekundeString;

    public TextMeshProUGUI Minuti;      // UI element minuta
    public TextMeshProUGUI Sekunde;     // UI element sekunda
    public TextMeshProUGUI Milisekunde; // UI element stotinka

    public static float Vreme;

    void Update()
    {
        // Racunanje milisekunda
        MilisekundeBrojac += Time.deltaTime * 10;
        Vreme += Time.deltaTime;
        // Pretvaranje milisekunda u string i njihov ispis
        MilisekundeString = MilisekundeBrojac.ToString("f0");
        Milisekunde.text = "" + MilisekundeString;
        
        // Pretvaranje milisekunda u sekunde
        if (MilisekundeBrojac >= 10)
        {
            MilisekundeBrojac = 0;
            SekundeBrojac += 1;
        }

        if (SekundeBrojac <= 9)
        {
            Sekunde.text = "0" + SekundeBrojac + ":";
        }   
        else
        {
            Sekunde.text = "" + SekundeBrojac + ":";
        }

        // Pretvaranje sekunda u minute
        if (SekundeBrojac >= 60)
        {
            SekundeBrojac = 0;
            MinutBrojac += 1;
        }

        if (MinutBrojac <= 9)
        {
            Minuti.text = "0" + MinutBrojac + ":";
        }   
        else
        {
            Minuti.text = "" + MinutBrojac + ":";
        }
    }
}
