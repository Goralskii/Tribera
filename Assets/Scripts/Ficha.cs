using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public GameObject casillaActual;
    public GameObject slotActual;
    //public GameObject activeArrow;
    public int ID;
    public int posActual = 0;
    public int vueltasCompletas = 0;
    public bool fichaActiva;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (fichaActiva)
        //{
        //    if (!activeArrow.activeSelf)
        //    {
        //        activeArrow.SetActive(true);
        //    }
        //}
        //else
        //{
        //    activeArrow.SetActive(false);
        //}

        if (posActual >= 36)
        {
            posActual = 0;
            vueltasCompletas++;
        }
    }
}
