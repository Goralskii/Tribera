using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public GameObject casillaActual;
    public GameObject slotActual;
    public GameObject activeArrow;
    public GameObject uiModel;
    public List<string> categoriasCompletas = new List<string>();
    public int ID;
    public int posActual = 0;
    public int vueltasCompletas = 0;
    public bool fichaActiva;
    public bool pierdeTurno;
    
    void Update()
    {
        if (fichaActiva)
        {
            if (!activeArrow.activeSelf)
            {
                activeArrow.SetActive(true);
                uiModel.SetActive(true);
            }
        }
        else
        {
            activeArrow.SetActive(false);
            uiModel.SetActive(false);
        }
        if (posActual == 0)
        {
            categoriasCompletas.Clear();
        }
        if (posActual >= 36)
        {
            posActual = 0;
            vueltasCompletas++;
        }
    }
}