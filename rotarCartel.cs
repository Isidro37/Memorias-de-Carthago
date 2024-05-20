using System.Collections;
using UnityEngine;

public class rotarCartel : MonoBehaviour
{
    private Vector3 originalRotation; // Almacena la rotación original del botón
    private Quaternion randomRotation; // Almacena la rotación aleatoria generada
    private bool rotating = false; // Bandera para indicar si el botón está rotando

    void Start()
    {
        originalRotation = transform.rotation.eulerAngles; // Obtener la rotación original
    }

    void Update()
    {
        if (!rotating)
        {
            StartCoroutine(RotateButton());
        }
    }

    IEnumerator RotateButton()
    {
        rotating = true;

        // Generar un ángulo de rotación aleatorio para los ejes X e Y
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        randomRotation = Quaternion.Euler(randomX, randomY, 0f);

        // Transición suave hacia la rotación aleatoria
        float elapsedTime = 0f;
        float duration = 1f; // Duración de la transición

        Quaternion startRotation = transform.rotation;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, randomRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Esperar un momento antes de volver a la posición original
        yield return new WaitForSeconds(0f);

        // Transición suave de vuelta a la rotación original
        elapsedTime = 0f;
        startRotation = transform.rotation;
        duration = 3f; // Duración de la transición de vuelta

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(originalRotation), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que el botón esté en la rotación original
        transform.rotation = Quaternion.Euler(originalRotation);
            yield return new WaitForSeconds(0f);

        rotating = false;
    }
}
