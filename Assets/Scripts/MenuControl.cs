using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public static MenuControl Instancia;
    public Text categorias;
    public Text currentPlayer;
    public Text vueltas;
    public DiceControl dice1;
    public DiceControl dice2;
    public int valorTotalSend;
    public int cantidadPlayers;//esta variable controla la cantidad de jugadores
    public int turno = 0;
    //public ControlTablero _controlTablero;
    public bool actualizado;
    private int valorD1 = 0;
    private int valorD2 = 0;//inicializa las variables
    bool InGame = false;
    public GameObject canvasMenuMain;
    public GameObject RulesPanel;
    public GameObject RulesPanel2;
    public GameObject HowToPlayPanel;
    public TMP_Dropdown dropdown;
    public bool sePuedeTirar;
    public bool avanzarTurno;
    public bool iniciado = false;
    public GameObject panelui;
    public GameObject buttonPrefab;
    public Transform parentTransform;
    public List<Button> buttonList;
    public GameObject especialPanel;
    public List<string> totalCategorias = new List<string>() { "HISTORIA", "GEOGRAFIA", "AMBIENTE", "CULTURA", "BIOLOGIA", "DEPORTES" };
    public Texture2D[] texturasDados;
    public RawImage dado1UI;
    public RawImage dado2UI;
    public ControlTablero controlTablero;
    public int jugadorIndex = 0;
    public GameObject MenuSeleccion;
    public bool randomizer = false;
    public GameObject winPanel;
    public GameObject AmbientSound;
    public GameObject HUD;
    public GameObject mainCamera;
    public GameObject VirtualCamera;
    public Ficha Ficha;
    public Button[] selectionButtons = new Button[5];
    //public GameObject[] ArrayBotonDado;
    public Image CartelJugador;
    public Sprite[] CartelTurnoJugador;
    public GameObject Paisaje;
    public Emblemas[] emblemas = new Emblemas[6];


    [Header("                                                    FICHAS")]
    public List<GameObject> listaFichasPrefabs;

    public void ButtonSelectionOnClick(Button btn, int idBtn)
    {
        Debug.LogWarning("Entrando al listener del boton");
        GameObject temp = Instantiate(listaFichasPrefabs[idBtn], ControlTablero.instance.arrayFichas[jugadorIndex].transform.position,
                    listaFichasPrefabs[idBtn].transform.rotation,
                    ControlTablero.instance.arrayFichas[jugadorIndex].transform);
        if (idBtn == 3)
        {
            Vector3 tempPos = temp.transform.position;
            temp.transform.position = new Vector3(tempPos.x, tempPos.y-0.03f, tempPos.z);
        }
        if (jugadorIndex == cantidadPlayers)
        {
            MenuSeleccion.SetActive(false);
            VirtualCamera.GetComponent<PlayableDirector>().enabled = true;
            HUD.SetActive(true);
            iniciado = true; 
            sePuedeTirar = true; 
            MostrarFichas(); 
        }
        jugadorIndex += 1;
    }
    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (Instancia == null)//compruebo que esta accion no se realizó con anterioridad
        {
            Instancia = this;
        }
    }

    private void Update()//cada frame
    {
        if (iniciado)
        {
            ActualizarUI();//actualiza los emblemas
        }
        ControlarWin();

        ControlarTurno();
        CartelJugador.sprite = CartelTurnoJugador[turno];

    }
    void Start()
    {
        //listener para botones de fichas
        for (int i = 0; i < selectionButtons.Length; i++)
        {
            int tempIndex = i;
            Debug.Log("Asignando listener a boton " + selectionButtons[tempIndex].gameObject.name);
            selectionButtons[i].onClick.AddListener(() => ButtonSelectionOnClick(selectionButtons[tempIndex], tempIndex));
        }
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
            //currentPlayer.text = "Jugador actual - P" + (turno + 1);
            ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().fichaActiva = true;
            InGame = false;
        }
        if (!dice1.dadoEnMovimiento && !dice2.dadoEnMovimiento && !actualizado) //si entra al if ambos valores de los dados fueron asignados y ahora hay que mostrarlos por pantalla
        {
            randomizer = false;
            sePuedeTirar = false;
            dado1UI.texture = texturasDados[valorD1 - 1];
            dado2UI.texture = texturasDados[valorD2 - 1];
            valorTotalSend = valorD1 + valorD2;
            ControlTablero.instance.dadoCount = valorTotalSend;
            ControlTablero.instance.sePuedeMover = true;
            actualizado = true;
            avanzarTurno = false;
            yield return new WaitForSeconds(1f);
            if (dice1.espacioPressed && ControlTablero.instance.sePuedeMover)
            {
                dice1.espacioPressed = false;
                dice2.espacioPressed = false;
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
                //currentPlayer.text = "Jugador actual - P" + (turno + 1);
                ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().fichaActiva = true;
            }
        }
    }
    public void LimpiarValores()
    {
        valorD1 = 0;
        valorD2 = 0;
        dado1UI.texture = texturasDados[0];
        dado2UI.texture = texturasDados[0];
    }
    public void ControlarTurno()//resetea turnos
    {
        if (turno > cantidadPlayers)
        {
            turno = 0;
        }
    }
    public void ControlarWin()
    {
        if (ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas.Count == totalCategorias.Count || ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().vueltasCompletas == 3)
        {
            winPanel.SetActive(true);
            avanzarTurno = false;
        }
    }
    public void ActualizarUI()
    {
        vueltas.text = " " + ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().vueltasCompletas.ToString();
        string textoLista = string.Join("\n -", ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas);
        foreach (Emblemas emblema in emblemas)
        {
            emblema.ResetTexture();
        }
        foreach (string categ in ControlTablero.instance.arrayFichas[turno].GetComponent<Ficha>().categoriasCompletas)
        {
            for (int i = 0; i < emblemas.Length; i++) { 
                if (emblemas[i].gameObject.name == categ) {
                    emblemas[i].ChangeState(true);
                }
            
            }
        }
        // Asigna el texto generado al componente de texto UI
        categorias.text = textoLista;
    }
    public void Jugar()
    {
        if (actualizado)
        {
            panelui.SetActive(true);
            InGame = true;
            canvasMenuMain.SetActive(false);
            RulesPanel.SetActive(true);
            cantidadPlayers = int.Parse(dropdown.options[dropdown.value].text) - 1;

        }
    }

    public void siguienteTutorial()//boton del primer panel de reglas
    {
        RulesPanel.SetActive(false);
        RulesPanel2.SetActive(true);
    }

    public void siguenteTutorial2()//boton del 2do panel de reglas
    {
        RulesPanel2.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void desactivarTutorial()//boton del panel como jugar
    {
        HowToPlayPanel.SetActive(false);
        MenuSeleccion.SetActive(true);
        dado1UI.gameObject.SetActive(true);
        dado2UI.gameObject.SetActive(true);
        AmbientSound.SetActive(true);
    }

    public void MostrarFichas()//para mostrar las fichas, en cada boton en vez de cambiar el id, cambiamos la posicion en el array del objeto
    {

        for (int i = 0; i <= 4; i++)//ficha
        {
            for (int j = 0; j <= cantidadPlayers; j++)//jugador
            {
                if (j == ControlTablero.instance.arrayFichas[i].GetComponent<Ficha>().ID)
                {
                    ControlTablero.instance.arrayFichas[i].SetActive(true);
                }
            }
        }
    }
    public IEnumerator RandomSprite() {
        if (randomizer) {
            dado1UI.texture = texturasDados[Random.Range(0, texturasDados.Length)];
            dado2UI.texture = texturasDados[Random.Range(0, texturasDados.Length)];
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(RandomSprite());
        }
        yield break;
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
        for (int i = 0; i < totalCategorias.Count; i++)
        {
            if (!ficha.categoriasCompletas.Contains(totalCategorias[i]))
            {
                Debug.Log("Creando botón: " + totalCategorias[i]);
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
    public void ReloadScene()
    {
        // Obtener el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Recargar la escena actual
        SceneManager.LoadScene(currentSceneIndex);
    }
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        Debug.Log("Opción seleccionada: " + dropdown.options[dropdown.value].text);
    }

    public void lanzarDados()//boton lanzar dados
    {
        dice1.SetearDados();
        dice2.SetearDados();
    }


}