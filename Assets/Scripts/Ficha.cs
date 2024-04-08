using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public GameObject casillaActual;
    public GameObject slotActual;
    public int posActual = 0;
    public int vueltasCompletas = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (posActual >= 36)
        {
            posActual = 0;
            vueltasCompletas++;
        }
    }
}
