using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTablero : MonoBehaviour
{
    public GameObject[] arrayTablero;
    public GameObject[] arrayFichas;
    public MenuControl _menuControl;
    public bool sePuedeMover;
    public int dadoCount;
    void Awake()
    {        
        AsignarFichas();
        _menuControl = GameObject.Find("Canvas").GetComponent<MenuControl>();
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
            arrayFichas[Ficha].transform.position = arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual+i].transform.position;
            
            if (arrayTablero[i].name == "Laguna")
            {
                Debug.Log("Laguna rotando");
                arrayFichas[Ficha].transform.Rotate(0, 45, 0);
            }
        }
        Debug.Log("Count: " + count);
        arrayFichas[Ficha].GetComponent<Ficha>().posActual += count;        
        Debug.Log("Ya movi ficha, ahora voy a acomodar");
        Debug.Log("Accediendo al casillero: " + arrayTablero[count].name);
        yield return new WaitForSeconds(0.5f);
        arrayTablero[count].GetComponent<Casillero>().AcomodarFicha(Ficha);
        arrayFichas[Ficha].GetComponent<Ficha>().posActual = arrayFichas[Ficha].GetComponent<Ficha>().posActual/2;
    }
}






