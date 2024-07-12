using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ficha : MonoBehaviour
{
    public GameObject casillaActual;
    public GameObject slotActual;
    public GameObject activeArrow;
    //public GameObject uiModel;
    public List<string> categoriasCompletas = new List<string>();
    public int ID;
    public int posActual = 0;
    public int vueltasCompletas = 0;
    public bool fichaActiva;
    public bool pierdeTurno;

    public List<GameObject> listaFichasUI;
    public int indexFichaUI;

    void Update()
    {
        if (fichaActiva)
        {
            if (!activeArrow.activeSelf)
            {
                Color color;
                int red;
                int green;
                int blue;

                // Convertir a rango 0.0 a 1.0
                activeArrow.SetActive(true);
                string nombreFicha = transform.GetChild(1).name;
                Debug.Log("Nombre ficha: " + nombreFicha);

                //// Desactivar todos los elementos en listaFichasUI antes de activar el correspondiente
               foreach (var fichaUI in listaFichasUI)
                {
                    fichaUI.SetActive(false) ;
                }

                switch (nombreFicha)
                {
                    case "Yacare2(Clone)":
                        listaFichasUI[0].SetActive(true);
                        //Debug.Log("Se activo el UI de :" + listaFichasUI[0].gameObject.name);
                        indexFichaUI = 0;
                        red = 22;
                        green = 63;
                        blue = 33;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Dorado(Clone)":
                        listaFichasUI[1].SetActive(true);
                        indexFichaUI = 1;
                        red = 201;
                        green = 143;
                        blue = 0;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Surubi(Clone)":
                        listaFichasUI[2].SetActive(true);
                        indexFichaUI = 2;
                        red = 123;
                        green = 107;
                        blue = 82;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Carpincho1(Clone)":
                        listaFichasUI[3].SetActive(true);
                        indexFichaUI = 3;
                        red = 255;
                        green = 145;
                        blue = 4;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Yaguarete(Clone)":
                        listaFichasUI[4].SetActive(true);
                        indexFichaUI = 4;
                        red = 255;
                        green = 221;
                        blue = 73;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            activeArrow.SetActive(false);
            //listaFichasUI[indexFichaUI].gameObject.SetActive(false);
        }
        //if (posActual == 0)
        //{
        //    categoriasCompletas.Clear();
        //}
        if (posActual >= 36)
        {
            posActual = 0;
            vueltasCompletas++;
        }
    }
}