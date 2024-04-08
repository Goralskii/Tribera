using UnityEngine;
using System.Collections;

public class MovementAnimation : MonoBehaviour
{
    public float duration = 1f;     // Duraci�n total de la animaci�n
    public float maxHeight = 1f;
    public static MovementAnimation instancia;
    private Vector3 startPosition;
    private Vector3 endPosition;


    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (instancia == null)//compruebo que esta accion no se realiz� con anterioridad
        {
            instancia = this;
        }
    }
    // Funci�n para iniciar la animaci�n de movimiento
    public IEnumerator Move(GameObject objectToMove, Vector3 start, Vector3 end)
    {
        // Guarda las posiciones inicial y final
        startPosition = start;
        endPosition = end;

        float elapsedTime = 0f;

        // Mientras el tiempo transcurrido sea menor que la duraci�n
        while (elapsedTime < duration)
        {
            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Calcula el par�metro t basado en el tiempo transcurrido
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Calcula la posici�n de la ficha utilizando una trayectoria de par�bola
            Vector3 newPosition = CalculateParabolicPosition(startPosition, endPosition, maxHeight, t);

            // Actualiza la posici�n de la ficha
            objectToMove.transform.position = newPosition;

            yield return null; // Pausa la ejecuci�n hasta el siguiente frame
        }

        // Aseg�rate de que la ficha est� en la posici�n final exacta
        objectToMove.transform.position = endPosition;
    }

    // Funci�n para calcular la posici�n en una trayectoria de par�bola
    private Vector3 CalculateParabolicPosition(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = -4f * t * t + 4f * t; // Funci�n parab�lica

        // Interpola linealmente entre la posici�n inicial y final en el eje XZ
        Vector3 result = Vector3.Lerp(start, end, t);

        // Ajusta la altura de la posici�n seg�n la funci�n parab�lica
        result.y += parabolicT * height;

        return result;
    }
}
