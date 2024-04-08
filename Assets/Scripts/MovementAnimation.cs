using UnityEngine;
using System.Collections;

public class MovementAnimation : MonoBehaviour
{
    public float duration = 1f;     // Duración total de la animación
    public float maxHeight = 1f;
    public static MovementAnimation instancia;
    private Vector3 startPosition;
    private Vector3 endPosition;


    private void OnEnable()//Se ejecuta cuando el objeto esta activo y es llamado
    {
        if (instancia == null)//compruebo que esta accion no se realizó con anterioridad
        {
            instancia = this;
        }
    }
    // Función para iniciar la animación de movimiento
    public IEnumerator Move(GameObject objectToMove, Vector3 start, Vector3 end)
    {
        // Guarda las posiciones inicial y final
        startPosition = start;
        endPosition = end;

        float elapsedTime = 0f;

        // Mientras el tiempo transcurrido sea menor que la duración
        while (elapsedTime < duration)
        {
            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Calcula el parámetro t basado en el tiempo transcurrido
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Calcula la posición de la ficha utilizando una trayectoria de parábola
            Vector3 newPosition = CalculateParabolicPosition(startPosition, endPosition, maxHeight, t);

            // Actualiza la posición de la ficha
            objectToMove.transform.position = newPosition;

            yield return null; // Pausa la ejecución hasta el siguiente frame
        }

        // Asegúrate de que la ficha esté en la posición final exacta
        objectToMove.transform.position = endPosition;
    }

    // Función para calcular la posición en una trayectoria de parábola
    private Vector3 CalculateParabolicPosition(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = -4f * t * t + 4f * t; // Función parabólica

        // Interpola linealmente entre la posición inicial y final en el eje XZ
        Vector3 result = Vector3.Lerp(start, end, t);

        // Ajusta la altura de la posición según la función parabólica
        result.y += parabolicT * height;

        return result;
    }
}
