using UnityEngine;

public class Canal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            GameManager.Instance.TiradaFallida();
        }
    }
}