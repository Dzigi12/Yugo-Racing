using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacijaAuta : MonoBehaviour
{
    public float rotacija;
    void Update()
    {
        // Rotiranje auta
        rotacija = 20 * Time.deltaTime;
        transform.Rotate(0, rotacija, 0);
    }
}
