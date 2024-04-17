using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlTablero : MonoBehaviour
{
    public static ControlTablero instance;
    public GameObject[] arrayTablero;
    public GameObject[] arrayFichas;
    public MovementAnimation _movementAnimation;
    public AnsManager _ansManager;
    public GameObject questionPanel;
    public bool sePuedeMover;
    public int dadoCount;
    public string cateSend;
    public bool especial = false;
    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (instance == null)//compruebo que esta accion no se realizó con anterioridad
        {
            instance = this;
        }
    }
    void Awake()
    {
        _movementAnimation = GameObject.Find("Script").GetComponent<MovementAnimation>();
    }
    public void AsignarFichas()
    {
        arrayFichas = GameObject.FindGameObjectsWithTag("Ficha");
    }
    public IEnumerator MoverFicha(int count, int Ficha)
    {        
        arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].GetComponent<Casillero>().LiberarFicha(Ficha);
        if (questionPanel.activeSelf)
        {
            yield return new WaitForSeconds(1.5f);
            questionPanel.SetActive(false);
        }
        for (int i = 0; i <= count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (i != 0)
            {
                if (arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].name == "Laguna")
                {
                    arrayFichas[Ficha].transform.Rotate(0, 90, 0);
                }
                Vector3 posDestino = arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].transform.position;
                posDestino.y += 0.05f;
                yield return StartCoroutine(_movementAnimation.Move(arrayFichas[Ficha], arrayFichas[Ficha].transform.position, posDestino));
            }
            arrayFichas[Ficha].GetComponent<Ficha>().posActual++;
        }
        arrayFichas[Ficha].GetComponent<Ficha>().posActual--;
        yield return new WaitForSeconds(0.5f);
        arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual].GetComponent<Casillero>().AcomodarFicha(Ficha);
        arrayFichas[Ficha].GetComponent<Ficha>().casillaActual = arrayTablero[arrayFichas[Ficha].GetComponent<Ficha>().posActual];
        if (arrayFichas[Ficha].GetComponent<Ficha>().casillaActual.name != "Laguna" && arrayFichas[Ficha].GetComponent<Ficha>().casillaActual.name != "Salida")
        {
            if (!(arrayFichas[Ficha].GetComponent<Ficha>().categoriasCompletas.Contains(arrayFichas[Ficha].GetComponent<Ficha>().casillaActual.GetComponent<Casillero>().categoria)))
            {
                cateSend = arrayFichas[Ficha].GetComponent<Ficha>().casillaActual.GetComponent<Casillero>().categoria;
                if (cateSend == "ESPECIAL")
                {
                    especial = true;
                    //Mostrar panel de categorias
                    MenuControl.Instancia.CasillaEspecial(arrayFichas[Ficha].GetComponent<Ficha>());

                }
                yield return new WaitUntil(() => !especial);
                StartCoroutine(MostrarPregunta(cateSend));
            } else
            {
                arrayFichas[Ficha].GetComponent<Ficha>().fichaActiva = false;
                MenuControl.Instancia.avanzarTurno = true;
                MenuControl.Instancia.sePuedeTirar = true;
            }
        }
        else
        {
            if (arrayFichas[Ficha].GetComponent<Ficha>().casillaActual.name == "Laguna") arrayFichas[Ficha].GetComponent<Ficha>().pierdeTurno = true;
            arrayFichas[Ficha].GetComponent<Ficha>().fichaActiva = false;
            MenuControl.Instancia.avanzarTurno = true;
            MenuControl.Instancia.sePuedeTirar = true;
        }
    }
    public IEnumerator MostrarPregunta(string cat)
    {
        questionPanel.SetActive(true);
        _ansManager.StartTrivia(cat);
        yield return new WaitForSeconds(10f);
    }
}