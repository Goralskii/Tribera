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
            PlayerPrefs.DeleteAll();
            DontDestroyOnLoad(gameObject);
        }
    }
}
