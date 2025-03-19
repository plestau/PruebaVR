using UnityEngine;

public class BolasVuelven : MonoBehaviour
{
    public Transform destino;
    public bool esCanal, esFondo;
    private GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        gameManager = GameManager.Instance;
        if (other.CompareTag("Bola"))
        {
            if (esCanal)
            {
                if (gameManager.bolosDerribados > 0)
                {
                    Debug.Log("Tirada acertada por canal");
                    gameManager.TiradaAcertada();
                }
                else
                {
                    gameManager.TiradaFallida();
                }
            }
            else if (esFondo)
            {
                Debug.Log("Tirada acertada por fondo");
                gameManager.TiradaAcertada();
                other.transform.position = destino.position;
                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            else
            {
                other.transform.position = destino.position;
                other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }
}