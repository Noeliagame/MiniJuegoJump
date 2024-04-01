using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float downwardDistance = 0.5f; // Distancia hacia abajo cuando el personaje cae sobre la plataforma
    public float speed = 2f; // Velocidad de movimiento de la plataforma hacia abajo
    public float returnSpeed = 4f; // Velocidad de retorno de la plataforma a su posición original
    public float returnDelay = 0.5f; // Retraso antes de que la plataforma vuelva a su posición original

    private Vector2 originalPosition;
    private bool isReturning = false;
    private Rigidbody2D rb;

    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si el objeto que colisiona es el jugador
        {
            isReturning = false;
            Vector2 targetPosition = originalPosition - Vector2.up * downwardDistance;
            StartCoroutine(MovePlatform(targetPosition, speed));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si el objeto que sale de la colisión es el jugador
        {
            isReturning = true;
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    System.Collections.IEnumerator MovePlatform(Vector2 targetPosition, float moveSpeed)
    {
        while ((Vector2)transform.position != targetPosition)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
            yield return null;
        }
    }

    System.Collections.IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnDelay);

        while ((Vector2)transform.position != originalPosition && isReturning)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime));
            yield return null;
        }
    }
}