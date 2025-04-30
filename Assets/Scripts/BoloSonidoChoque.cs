using UnityEngine;

public class BoloSonidoChoque : MonoBehaviour
{
    public AudioClip sonidoChoque;
    private AudioSource audioSource;
    private float tiempoUltimoSonido = 0f;
    public float delayEntreSonidos = 0.5f; // en segundos
    public float umbralVelocidad = 0.3f;   // evita sonidos por choques suaves

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Solo suena si colisiona con la bola o con otro bolo
        if (collision.gameObject.CompareTag("Bola") || collision.gameObject.CompareTag("Bolo"))
        {
            float velocidadImpacto = collision.relativeVelocity.magnitude;

            if (velocidadImpacto > umbralVelocidad && Time.time - tiempoUltimoSonido > delayEntreSonidos)
            {
                audioSource.PlayOneShot(sonidoChoque);
                tiempoUltimoSonido = Time.time;
                Debug.Log("Bolo ha chocado con " + collision.gameObject.name + " a velocidad: " + velocidadImpacto);
            }
        }
    }
}