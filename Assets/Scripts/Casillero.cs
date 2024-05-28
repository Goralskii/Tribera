using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casillero : MonoBehaviour
{
    [SerializeField] GameObject[] arraySlots;
    [SerializeField] bool isOcupado;
    public Sprite[] insignias;
    public SpriteRenderer show;
    public ControlTablero _controlTablero;
    public string categoria;
    void Awake()
    {
        _controlTablero = GameObject.Find("Tablero").GetComponent<ControlTablero>();
        categoria = GetComponent<MeshRenderer>().material.name;
        categoria = categoria.Substring(0, categoria.Length - 11);
        for (int i = 0; i<insignias.Length; i++)
        {
            if (insignias[i].name == categoria)
            {
                show.sprite = insignias[i];
            }
        }
    }
    public void AcomodarFicha(int ficha)
    {
        foreach (GameObject slot in arraySlots) {
            if (!slot.GetComponent<Slot>().isOccupied)
            {
                slot.GetComponent<Slot>().isOccupied = true;
                slot.GetComponent<Slot>().fichaOcupante = ficha;
                _controlTablero.arrayFichas[ficha].GetComponent<Ficha>().slotActual = slot;
                _controlTablero.arrayFichas[ficha].transform.position = slot.transform.position;
                return;
            }
        }
    }
    public void LiberarFicha(int ficha)
    {
        foreach (GameObject slot in arraySlots)
        {
            if (slot.GetComponent<Slot>().fichaOcupante == ficha)
            {
                slot.GetComponent<Slot>().isOccupied = false;
                slot.GetComponent<Slot>().fichaOcupante = 404;
                _controlTablero.arrayFichas[ficha].GetComponent<Ficha>().slotActual = null;                
                return;
            }
        }
    }
}