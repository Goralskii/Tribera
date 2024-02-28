using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFicha : MonoBehaviour
{
    private bool seleccionado = false;
    private Vector3 posicionInicial;

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            seleccionado = true;
            posicionInicial = transform.position;
        }
    }

    void Update()
    {
        if (seleccionado)
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit))
            {
                if (hit.collider.CompareTag("Casillero")) // Asegúrate de que los casilleros tengan el tag "Casillero"
                {
                    transform.position = new Vector3(hit.collider.transform.position.x, 
                                                    transform.position.y, 
                                                    hit.collider.transform.position.z);

                    if (Input.GetMouseButtonUp(0))
                    {
                        seleccionado = false;
                        // Aquí puedes agregar código adicional cuando se suelta el clic del mouse
                    }
                }
            }
        }
    }
}



