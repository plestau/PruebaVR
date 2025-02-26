using UnityEngine;
using System.Collections;

public class BolaEnZona : MonoBehaviour
{
    public bool esZonaBolos;
    private float tiempoEnZona = 0f;
    private bool bolaEnZona = false;
    public Transform destino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            bolaEnZona = true;
            StartCoroutine(ContarTiempoEnZona(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            bolaEnZona = false;
            tiempoEnZona = 0f;
        }
    }

    private IEnumerator ContarTiempoEnZona(Collider bola)
    {
        while (bolaEnZona)
        {
            tiempoEnZona += Time.deltaTime;
            if (tiempoEnZona >= 5f)
            {
                if (esZonaBolos)
                {
                    GameManager.Instance.TiradaAcertada();
                }
                bola.transform.position = destino.position;
                bola.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                bola.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                break;
            }
            yield return null;
        }
    }
}