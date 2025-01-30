using UnityEngine;

public class BolasVuelven : MonoBehaviour
{
    public Transform destino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            Debug.Log("Bola");
            other.transform.position = destino.position;
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
