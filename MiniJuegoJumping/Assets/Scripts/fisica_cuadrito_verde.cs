using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisica_cuadrito_verde : MonoBehaviour
{
    // Start is called before the first frame update
    public float shrinkAmount = 0.8f; // Cantidad de encogimiento
    public float growAmount = 1f; // Cantidad de crecimiento
    public float duration = 0.8f; // Duración del encogimiento y crecimiento

    private Vector3 originalScale; // Escala original del cuadrado
    private bool isShrinking = false; // Estado de encogimiento
    void Start()

    {
        originalScale = transform.localScale; // Guarda la escala original del cuadrado
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si el personaje colisiona con el cuadrado
        {
            if (!isShrinking) // Si no está actualmente encogiendo
            {
                isShrinking = true; // Establece el estado de encogimiento a verdadero
                StartCoroutine(ShrinkCoroutine()); // Comienza la corrutina de encogimiento
            }
        }
    }

    IEnumerator ShrinkCoroutine()
    {
        float timer = 0; // Inicializa un temporizador

        while (timer < duration) // Mientras el temporizador sea menor que la duración del encogimiento
        {
            float scaleFactor = Mathf.Lerp(1, shrinkAmount, timer / duration); // Calcula el factor de escala
            transform.localScale = originalScale * scaleFactor; // Aplica la escala al cuadrado
            timer += Time.deltaTime; // Incrementa el temporizador
            yield return null; // Espera un frame
        }

        timer = 0; // Reinicia el temporizador

        while (timer < duration) // Mientras el temporizador sea menor que la duración del crecimiento
        {
            float scaleFactor = Mathf.Lerp(shrinkAmount, growAmount, timer / duration); // Calcula el factor de escala
            transform.localScale = originalScale * scaleFactor; // Aplica la escala al cuadrado
            timer += Time.deltaTime; // Incrementa el temporizador
            yield return null; // Espera un frame
        }

        isShrinking = false; // Establece el estado de encogimiento a falso
        yield return null;
    }
}
