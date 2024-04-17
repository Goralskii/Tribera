using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlCara : MonoBehaviour
{
    [SerializeField] private bool enSuelo = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tapete"))
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