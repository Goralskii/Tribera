using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casillero : MonoBehaviour
{
    [SerializeField] GameObject[] arraySlots;
    [SerializeField] bool isOcupado;
    public ControlTablero _controlTablero;
    // Start is called before the first frame update

    void Awake()
    {
        _controlTablero = GameObject.Find("Tablero").GetComponent<ControlTablero>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcomodarFicha(int ficha)
    {
        Debug.Log("Acomodando Ficha");
        foreach (GameObject slot in arraySlots) {
            if (!slot.GetComponent<Slot>().isOccupied)
            {
                Debug.Log("Acomodando ficha en slot: " +  slot.name);
                _controlTablero.arrayFichas[ficha].transform.position = slot.transform.position;
                return;
            }
        }
    }   
}
