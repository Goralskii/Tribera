using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPanel : MonoBehaviour
{
    bool bandera =false;
    private void Start()
    {

    }
    public void Jugar(string SampleScene)
    {
        SceneManager.LoadScene(SampleScene);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }
}
