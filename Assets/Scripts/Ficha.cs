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

                //// Desactivar todos los elementos en listaFichasUI antes de activar el correspondiente
                //foreach (var fichaUI in listaFichasUI)
                //{
                //    fichaUI.gameObject.SetActive(false);
                //}

                switch (nombreFicha)
                {
                    case "Yacare2(Clone)":
                        listaFichasUI[0].gameObject.SetActive(true);
                        //Debug.Log("Se activo el UI de :" + listaFichasUI[0].gameObject.name);
                        indexFichaUI = 0;
                        red = 57;
                        green = 100;
                        blue = 72;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Dorado(Clone)":
                        listaFichasUI[1].gameObject.SetActive(true);
                        indexFichaUI = 1;
                        red = 172;
                        green = 162;
                        blue = 53;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Surubi(Clone)":
                        listaFichasUI[2].gameObject.SetActive(true);
                        indexFichaUI = 2;
                        red = 107;
                        green = 119;
                        blue = 140;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Carpincho1(Clone)":
                        listaFichasUI[3].gameObject.SetActive(true);
                        indexFichaUI = 3;
                        red = 149;
                        green = 113;
                        blue = 37;
                        color = new Color(red / 255f, green / 255f, blue / 255f);
                        activeArrow.GetComponent<SpriteRenderer>().color = color;
                        Debug.Log("el nombre de la ficha es: " + indexFichaUI);
                        break;
                    case "Yaguarete(Clone)":
                        listaFichasUI[4].gameObject.SetActive(true);
                        indexFichaUI = 4;
                        red = 204;
                        green = 182;
                        blue = 141;
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
            listaFichasUI[indexFichaUI].gameObject.SetActive(false);
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