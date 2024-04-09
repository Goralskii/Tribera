using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private static Cleaner instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Debug.LogWarning("Se limpiaron las prefs");
            PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(gameObject);
        }
        // No destruir este objeto al cargar una nueva escena
        
    }

    

}
