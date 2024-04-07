using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public static MenuControl Instancia;
    [SerializeField] private TMP_Text Dado1;
    [SerializeField] private TMP_Text Dado2;
    [SerializeField] private TMP_Text ValorTotal; //es el valor total de la suma de los lados
    public DiceControl dice1;
    public DiceControl dice2;
    public int valorTotalSend;
    //public ControlTablero _controlTablero;
    public bool actualizado;
    private int valorD1 = 0;
    private int valorD2 = 0;//inicializa las variables
    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (Instancia == null)//compruebo que esta accion no se realizó con anterioridad
        {
            Instancia = this;
        }
    }
    private void Awake()
    {
        //_controlTablero = GameObject.Find("Tablero").GetComponent<ControlTablero>();
    }
    public IEnumerator ActualizarValor()
    {
         valorD1 = dice1.valorDado;
         valorD2 = dice2.valorDado;
      

        if (!dice1.dadoEnMovimiento && !dice2.dadoEnMovimiento && !actualizado) //si entra al if ambos valores de los dados fueron asignados y ahora hay que mostrarlos por pantalla
        {
           // Debug.Log("Valor mostrado d1: " + valorD1);
           // Debug.Log("Valor mostrado d2 : " + valorD2);
            Dado1.text = "Dado 1: " + valorD1.ToString();
            Dado2.text = "Dado 2: " + valorD2.ToString();
            valorTotalSend = valorD1 + valorD2;
            ValorTotal.text = "Valor Total: " + (valorTotalSend).ToString();
            ControlTablero.instance.dadoCount = valorTotalSend;
            ControlTablero.instance.sePuedeMover = true;
            actualizado = true;
            Debug.Log("Ya actualice valor dado");
            yield return new WaitForSeconds(1f);
            if (dice1.espacioPressed && ControlTablero.instance.sePuedeMover)
            {

                dice1.espacioPressed = false;
                dice2.espacioPressed = false;
                Debug.Log("Moviendo ficha...");
                StartCoroutine(ControlTablero.instance.MoverFicha(ControlTablero.instance.dadoCount, 1));
            }

        }
        Debug.Log("Volviendo...");
        yield return new WaitForSeconds(10f);
    }

    public void LimpiarValores()
    {
        valorD1 = 0;
        valorD2 = 0;
        Dado1.text = "Dado 1: " + valorD1.ToString();
        Dado2.text = "Dado 2: " + valorD2.ToString();
        ValorTotal.text = "Valor Total: " + (valorD1 + valorD2).ToString();   
    }
}
