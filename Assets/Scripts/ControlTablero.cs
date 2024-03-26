using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTablero : MonoBehaviour
{
    [SerializeField] GameObject[] arrayTablero = new GameObject[36];
    [SerializeField] GameObject[] arrayFichas;
    public MenuControl _menuControl;
    public bool sePuedeMover;
    public int dadoCount;
    void Awake()
    {
        AsignarCasilleros();
        AsignarFichas();
        _menuControl = GameObject.Find("Canvas").GetComponent<MenuControl>();
    }

    public void AsignarCasilleros()
    {

        //asignar casillero de salida
        arrayTablero[0] = GameObject.Find("Salida");

        //Asignar Lagunas
        arrayTablero[6] = GameObject.Find("Laguna 1");
        arrayTablero[15] = GameObject.Find("Laguna 2");
        arrayTablero[24] = GameObject.Find("Laguna 3");
        arrayTablero[33] = GameObject.Find("Laguna 4");
        
        // Asignar los casilleros normales
        for (int i = 1; i < arrayTablero.Length; i++)
        {
            if (arrayTablero[i] == null) // Si no se ha asignado ya como Laguna
            {
                arrayTablero[i] = GameObject.Find("Casillero " + i);
            }
        }

        // Verificar la asignación de los casilleros (para fines de depuración)
        /*for (int i = 0; i < arrayTablero.Length; i++)
        {
            if (arrayTablero[i] != null)
            {
                Debug.Log("Casillero " + i + " asignado: " + arrayTablero[i].name);
            }
            else
            {
                Debug.LogWarning("Casillero " + i + " no asignado.");
            }
        }*/
    }
    public void AsignarFichas()
    {
        arrayFichas = GameObject.FindGameObjectsWithTag("Ficha");
    }
    public IEnumerator MoverFicha(int count, int Ficha)
    {
        for (int i = 0; i <= count; i++)
        {
            yield return new WaitForSeconds(0.5f);//delay
            arrayFichas[Ficha].transform.position = arrayTablero[i].transform.position;
            
        }
    }
}






