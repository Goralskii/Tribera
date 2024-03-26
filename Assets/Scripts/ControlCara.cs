using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//este algoritmo controla si los sensores del dado entran en contacto con el tapete

public class ControlCara : MonoBehaviour
{
    
   
    [SerializeField] private bool enSuelo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tapete")
        {
            enSuelo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enSuelo = false;
    }

    public bool ComprobarSuelo()
    {
        return enSuelo;
    }
}
