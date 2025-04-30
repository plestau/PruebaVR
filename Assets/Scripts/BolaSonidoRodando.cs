using UnityEngine;

public class BolaSonidoRodando : MonoBehaviour
{
    public AudioClip sonidoRodando;
    private AudioSource audioSource;
    private float tiempoUltimoSonido = 0f;
    public float delayEntreSonidos = 1f; // tiempo mínimo entre sonidos

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pista"))
        {
            if (Time.time - tiempoUltimoSonido > delayEntreSonidos)
            {
                audioSource.clip = sonidoRodando;
                audioSource.Play();
                tiempoUltimoSonido = Time.time;
                Debug.Log("Sonido de bola rodando reproducido");
            }
        }
    }
}