using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;


public class DiceControl : MonoBehaviour
{
    private float ejeX;
    private float ejeY;
    private float ejeZ;
    private Vector3 posicionInicial;
    private Rigidbody rbdado;
    public bool dadoEnMovimiento = true;
    public ControlCara[] lados = new ControlCara[6];
    public int valorDado = 0;
    public bool espacioPressed;
    private int ladoOculto;
    int valor;
    private AudioSource diceSound;
    [SerializeField] private AudioClip singleDice;
    void Start()
    {
        posicionInicial = this.transform.position;
        rbdado = this.GetComponent<Rigidbody>();
        diceSound = GetComponent<AudioSource>();
        diceSound.clip = singleDice;
    }
    void Update()
    {
        if (MenuControl.Instancia.sePuedeTirar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PrepararDado();
                espacioPressed = true;
                diceSound.Play();
                MenuControl.Instancia.actualizado = false;
                MenuControl.Instancia.randomizer = true;
                StartCoroutine(MenuControl.Instancia.RandomSprite());
                ControlTablero.instance.sePuedeMover = false;
            }
        }
        if (rbdado.IsSleeping() && dadoEnMovimiento)
        {
            dadoEnMovimiento = false;
            ladoOculto = ComprobarLados();
            valorDado = 7 - ladoOculto;
            if (valorDado == 7)//O sea que lado oculto tiene valor 0 (ninguna cara esta apoyada)
            {
                rbdado.AddForce(x:3f, y:0, z:0, ForceMode.Impulse);//se le aplica esta fuerza para que caiga de un lado
                dadoEnMovimiento = true;
            }
        }
        StartCoroutine(MenuControl.Instancia.ActualizarValor());
    }
    public IEnumerator ThrowAndWait()
    {
        yield return new WaitForSeconds(10f);
    }
    void PrepararDado()
    {
        this.transform.position = posicionInicial;
        rbdado.velocity = Vector3.zero;
        MenuControl.Instancia.LimpiarValores();
        dadoEnMovimiento = true;
        ejeX = UnityEngine.Random.Range(0, 360);
        ejeY = UnityEngine.Random.Range(0, 360);
        ejeZ = UnityEngine.Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(ejeX, ejeY, ejeZ);
        ejeX = UnityEngine.Random.Range(-3f, 3f);
        ejeY = UnityEngine.Random.Range(-2f, 0f);
        ejeZ = UnityEngine.Random.Range(-3f, 3f);
        rbdado.AddForce(ejeX, ejeY, ejeZ, ForceMode.Impulse);
    }
    int ComprobarLados()
    {
        valor = 0;
        foreach (ControlCara cara in lados)
        {
            valor++;
            if (cara.ComprobarSuelo())
            {
                return valor;
            }
        }
        return valor;
    }
}