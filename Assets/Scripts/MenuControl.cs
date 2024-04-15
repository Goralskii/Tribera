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
    public Text categorias;
    public Text currentPlayer;
    public Text vueltas;
    public DiceControl dice1;
    public DiceControl dice2;
    public int valorTotalSend;
    public int cantidadPlayers;//esta variable conrola la cantidad de jugadores
    public int turno = 0;
    //public ControlTablero _controlTablero;
    public bool actualizado;
    private int valorD1 = 0;
    private int valorD2 = 0;//inicializa las variables
    bool InGame = false;
    public GameObject canvasMenuMain;
    public TMP_Dropdown dropdown;
    public bool sePuedeTirar;
    public bool avanzarTurno;
    public bool iniciado = false;
    public GameObject panelui;
    public GameObject buttonPrefab;
    public Transform parentTransform;
    public List<Button> buttonList;
    public GameObject especialPanel;
    public List<string> totalCategorias = new List<string>() { "HISTORIA", "GEOGRAFIA", "AMBIENTAL", "CULTURA"};

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
        if (iniciado)
        {
            ActualizarUI();
        }
        ControlarWin();
        ControlarTurno();
        

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
        if (InGame)
        {
            currentPlayer.text = "Jugador actual - P" + (turno+1);
            ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().fichaActiva = true;
            InGame = false;
        }
        
        if (!dice1.dadoEnMovimiento && !dice2.dadoEnMovimiento && !actualizado) //si entra al if ambos valores de los dados fueron asignados y ahora hay que mostrarlos por pantalla
        {
            sePuedeTirar = false;
            // Debug.Log("Valor mostrado d1: " + valorD1);
            // Debug.Log("Valor mostrado d2 : " + valorD2);
            Dado1.text = "Dado 1: " + valorD1.ToString();
            Dado2.text = "Dado 2: " + valorD2.ToString();
            valorTotalSend = valorD1 + valorD2;
            ValorTotal.text = "Valor Total: " + (valorTotalSend).ToString();
            ControlTablero.instance.dadoCount = valorTotalSend;
            ControlTablero.instance.sePuedeMover = true;
            actualizado = true;
            avanzarTurno = false;
            Debug.Log("Ya actualice valor dado");
            yield return new WaitForSeconds(1f);
            if (dice1.espacioPressed && ControlTablero.instance.sePuedeMover)
            {

                dice1.espacioPressed = false;
                dice2.espacioPressed = false;
                Debug.Log("Moviendo ficha...");
                
                StartCoroutine(ControlTablero.instance.MoverFicha(valorTotalSend, turno));
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => avanzarTurno);

                ControlarWin();
                turno++;
                ControlarTurno();
                while (ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().pierdeTurno)
                {
                    int turnoPrevio = turno;                    
                    ControlTablero.instance.arrayFichas[turnoPrevio].GetComponent<Ficha>().pierdeTurno = false;
                    turno++;
                    ControlarTurno();
                    yield return new WaitForSeconds(1f);

                }
                currentPlayer.text = "Jugador actual - P" + (turno+1);
                ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().fichaActiva = true;


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
    public void ControlarTurno()
    {
        if (turno > cantidadPlayers)
        {
            turno = 0;
        }
    }
   public void ControlarWin()
    {
        Debug.Log("catCount: " + ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas.Count);
        Debug.Log("vueltasCount: " + ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().vueltasCompletas);
        if (ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas.Count == 4 || ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().vueltasCompletas == 4)
        {
            Salir();
        }
    }
    public void ActualizarUI()
    {
        vueltas.text = "Vueltas completas: " + ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().vueltasCompletas.ToString();
        string textoLista = string.Join("\n -", ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas);

        // Asigna el texto generado al componente de texto UI
        categorias.text = "Categorias Completadas: \n -" + textoLista;
    }
    public void Jugar()
    {
        if (actualizado)
        {
            panelui.SetActive(true);
            InGame = true;
            canvasMenuMain.SetActive(false);
            cantidadPlayers = int.Parse(dropdown.options[dropdown.value].text) - 1;
            sePuedeTirar = true;
            iniciado = true;
            MostrarFichas();
        }
        
    }
    public void MostrarFichas()
    {
        for (int i = 0; i <= cantidadPlayers; i++)
        {
            ControlTablero.instance.arrayFichas[i].SetActive(true);
        }
    }
    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void CasillaEspecial(Ficha ficha)
    {
        especialPanel.SetActive(true);
        for (int i = 0;i < totalCategorias.Count; i++)
        {
            if (!ficha.categoriasCompletas.Contains(totalCategorias[i]))
            {
                GameObject buttonTemp = Instantiate(buttonPrefab, parentTransform);
                Button button = buttonTemp.GetComponent<Button>();
                buttonTemp.GetComponentInChildren<TMP_Text>().text = totalCategorias[i];
                buttonList.Add(button);
                button.onClick.AddListener(() => ButtonClicked(button));
            }
        }
    }
    public void ButtonClicked(Button clickedButton)
    {
        ControlTablero.instance.cateSend = clickedButton.GetComponentInChildren<TMP_Text>().text;
        especialPanel.SetActive(false);
        ClearButtons();
        ControlTablero.instance.especial = false;
    }
    public void ClearButtons()
    {
        foreach (Button button in buttonList)
        {
            Destroy(button.gameObject);
        }
        buttonList.Clear();
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        Debug.Log("Opción seleccionada: " + dropdown.options[dropdown.value].text);
    }

}