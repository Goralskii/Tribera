using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class AnsManager : MonoBehaviour
{
    public AudioClip m_correctSound = null;
    public AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private TriviaManager m_triviaManager;
    [SerializeField] private ControlTablero m_controlTablero;
    // Añadir una variable para almacenar las categorías seleccionadas
    public static AnsManager instance;
    public int m_score = 0;
    public int? scoreLimit = 1;
    public string cate;
    public AudioSource m_audioSource;
    public void StartTrivia(string categoria)
    {
        cate = categoria;
        m_triviaManager.StartTrivia(cate); // Inicia la secuencia de preguntas
    }        
    public IEnumerator WaitAndEnd(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EndTimer();
    }
    public void StartTimer()
    {
        //timerStarted = true;
    }
    public void EndTimer()   // Do something when the timer ends, such as show the quiz results
    {
        if (m_audioSource.isPlaying) m_audioSource.Stop();
        m_audioSource.clip = m_incorrectSound;
        m_audioSource.Play();
        GameOver();
    }
    public void ShowWinnerScreen(bool val)
    {
        if (val)
        {
            //winnerScreen.SetActive(true);
            //winnerScreen.GetComponentInChildren<Text>().text = "¡Felicidades, Respondiste Todas las Preguntas!";
        }else
        {
            //winnerScreen.SetActive(true);
            //winnerScreen.GetComponentInChildren<Text>().text = "¡Felicidades, Ganaste!";
        }
    }
    public IEnumerator GiveAnswerRoutine(Button optionButton, bool answer)
    {
        if (m_audioSource.isPlaying) m_audioSource.Stop();        
        m_audioSource.clip = answer ? m_correctSound : m_incorrectSound;
        optionButton.GetComponent<Image>().color = answer ? m_correctColor : m_incorrectColor;       
        //blockOption.SetActive(true);
        m_audioSource.Play();
        Debug.Log("Respuesta:  " + answer);
        if (answer)
        {
            m_score++;
            //Ocultar panel, resetear score y avanzar 2
            yield return StartCoroutine(CloseResetAndGo());
        }
        else
        {
            m_score++;
            yield return StartCoroutine(CloseAndNext());
        }
    }    
    private IEnumerator CloseResetAndGo()
    {
        m_score = 0;
        m_controlTablero.arrayFichas[MenuControl.Instancia.turno].GetComponent<Ficha>().categoriasCompletas.Add(cate);
        yield return StartCoroutine(m_controlTablero.MoverFicha(2, m_controlTablero.arrayFichas[MenuControl.Instancia.turno].GetComponent<Ficha>().ID));
        //blockOption.SetActive(false);
    }
    // Corutina para esperar y luego mostrar el juego terminado
    private IEnumerator CloseAndNext()
    {
        yield return new WaitForSeconds(1.5f);
        m_score = 0;
        m_controlTablero.questionPanel.SetActive(false);
        m_controlTablero.arrayFichas[MenuControl.Instancia.turno].GetComponent<Ficha>().fichaActiva = false;
        MenuControl.Instancia.avanzarTurno = true;
        MenuControl.Instancia.sePuedeTirar = true;
        //blockOption.SetActive(false);
    }
    public void GameOver()
    {
        PlayerPrefs.DeleteKey("SelectedCategories");
        SceneManager.LoadScene("MainMenu");
    }
    private void OnApplicationQuit()
    {
        // Limpia todas las claves de PlayerPrefs al salir de la aplicación
        Debug.Log("Limpiando prefs");
        PlayerPrefs.DeleteAll();
    }
}