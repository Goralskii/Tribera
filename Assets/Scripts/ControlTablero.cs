using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTablero : MonoBehaviour
{
    public static ControlTablero instance;
    public GameObject[] arrayTablero;
    public GameObject[] arrayFichas;
    public MenuControl _menuControl;
    public MovementAnimation _movementAnimation;
    public bool sePuedeMover;
    public int dadoCount;
    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (instance == null)//compruebo que esta accion no se realizó con anterioridad
        {
            instance = this;
        }
    }
    void Awake()
    {        
        AsignarFichas();
        //_menuControl = GameObject.Find("Canvas").GetComponent<MenuControl>();
        _movementAnimation = GameObject.Find("Script").GetComponent<MovementAnimation>();
    }    
    public void AsignarFichas()
    {
        arrayFichas = GameObject.FindGameObjectsWithTag("Ficha");
    }
    public IEnumerator MoverFicha(int count, int Ficha)
    {
        arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].GetComponent<Casillero>().LiberarFicha(Ficha);
        for (int i = 0; i <= count; i++)
        {
            yield return new WaitForSeconds(0.5f);//delay
            
            //arrayFichas[Ficha].transform.position = arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual+i].transform.position;
            if (i != 0)
            {
                if (arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].name == "Laguna")
                {
                    Debug.Log("Laguna rotando");
                    arrayFichas[Ficha].transform.Rotate(0, 90, 0);
                }
                yield return StartCoroutine(_movementAnimation.Move(arrayFichas[Ficha], arrayFichas[Ficha].transform.position, arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].transform.position));
            }
            arrayFichas[Ficha].GetComponent<Ficha>().posActual++;
        }
        arrayFichas[Ficha].GetComponent<Ficha>().posActual--;
        Debug.Log("Count: " + count);
        //arrayFichas[Ficha].GetComponent<Ficha>().posActual += count;        
        Debug.Log("Ya movi ficha, ahora voy a acomodar");
        Debug.Log("Accediendo al casillero: " + arrayTablero[count].name);
        yield return new WaitForSeconds(0.5f);        
        arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].GetComponent<Casillero>().AcomodarFicha(Ficha);        
        arrayFichas[Ficha].GetComponent<Ficha>().casillaActual = arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual];
    }
}






