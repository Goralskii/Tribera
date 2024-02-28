using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dicecontrol : MonoBehaviour
{
    private float ejeX;
    private float ejeY;
    private float ejeZ;
    private Vector3 posicion_inicial;
    private Rigidbody rb;
    private bool dadoEnMovimiento = true;

    private int valor_dado;
    private int lado_oculto;




    void Start()
    {
        posicion_inicial = this.transform.position;
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepararDado();
        } 
    }

    void PrepararDado()
    {
        this.transform.position = posicion_inicial;
        rb.velocity = Vector3.zero;
        ejeX = Random.Range(0, 271);
        ejeY = Random.Range(0, 271);
        ejeZ = Random.Range(0, 271);

        this.transform.Rotate(ejeX, ejeY, ejeZ);
        ejeX = Random.Range(-3f, 3f);
        ejeY = Random.Range(-2f, 0f);
        ejeZ = Random.Range(-3f, 3f);
        rb.AddForce(new Vector3(ejeX, ejeY, ejeZ), ForceMode.Impulse);
    }

}
