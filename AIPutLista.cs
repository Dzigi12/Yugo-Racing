using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPutLista : MonoBehaviour
{
    public List<Transform> put; // Lista tacaka koje bot treba da prati

    private void Awake()
    {
        // Kreiranje liste
        foreach(Transform tr in gameObject.GetComponentInChildren<Transform>())
        {
            put.Add(tr);
        }
    }
}
