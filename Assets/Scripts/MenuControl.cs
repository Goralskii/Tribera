using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public static MenuControl Instancia;
    [SerializeField] private TMP_Text Dado1;
    [SerializeField] private TMP_Text Dado2;
    [SerializeField] private TMP_Text ValorTotal; //es el valor total de la suma de los lados
    public DiceControl dice1;
    public DiceControl dice2;
    public int valorTotalSend;
    public int turnoCount;//esta variable conrola la cantidad de jugadores
    public int turno = 0;
    //public ControlTablero _controlTablero;
    public bool actualizado;
    private int valorD1 = 0;
    private int valorD2 = 0;//inicializa las variables
    bool InGame = false;
    public GameObject canvasMenuMain;
    public TMP_Dropdown dropdown;

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
    private void Update()
    {
        if ( turno > turnoCount)
        {
            turno = 0;
        }
    }
    void Start()
    {
        // Obtener la referencia al Dropdown        

        // Agregar opciones al Dropdown
        //ActualizarOpcionesDropdown();

        // Suscribirse al evento onValueChanged
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }
    public IEnumerator ActualizarValor()
    {
         valorD1 = dice1.valorDado;
         valorD2 = dice2.valorDado;

        
        if (!dice1.dadoEnMovimiento && !dice2.dadoEnMovimiento && !actualizado) //si entra al if ambos valores de los dados fueron asignados y ahora hay que mostrarlos por pantalla
        {
            ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().fichaActiva = true;
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
                StartCoroutine(ControlTablero.instance.MoverFicha(valorTotalSend, turno));
                yield return new WaitForSeconds(1f);
                //Mostra pregunta
                
                turno++;
                if (ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().pierdeTurno)
                {
                    ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().pierdeTurno = false;
                    turno++;
                }
                

            }

        }
        Debug.Log("Volviendo...");
        
    }

    public void LimpiarValores()
    {
        valorD1 = 0;
        valorD2 = 0;
        Dado1.text = "Dado 1: " + valorD1.ToString();
        Dado2.text = "Dado 2: " + valorD2.ToString();
        ValorTotal.text = "Valor Total: " + (valorD1 + valorD2).ToString();   
    }

   
    
    

    public void Jugar()
    {
        InGame = true;

        if (InGame == true)
        {
            // Desactivar el canvas MenuMain
            canvasMenuMain.SetActive(false);
            turnoCount = int.Parse(dropdown.options[dropdown.value].text)-1;
        }

    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }

    
    

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        Debug.Log("Opción seleccionada: " + dropdown.options[dropdown.value].text);
    }

}