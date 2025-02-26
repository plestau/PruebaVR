using System.Collections;
using UnityEngine;

public class Bolo : MonoBehaviour
{
    private bool derribado = false;
    private float rotacionMaxima = 30f;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Rigidbody rb;

    private void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!derribado && (collision.gameObject.CompareTag("Bola") || collision.gameObject.CompareTag("Bolo")))
        {
            Vector3 rotacion = transform.eulerAngles;
            if (Mathf.Abs(rotacion.x) > rotacionMaxima || Mathf.Abs(rotacion.z) > rotacionMaxima || Mathf.Abs(rotacion.y) > rotacionMaxima)
            {
                derribado = true;
                GameManager.Instance.BoloDerribado();
                StartCoroutine(DesactivarBolo());
            }
        }
    }

    private IEnumerator DesactivarBolo()
    {
        yield return new WaitForSeconds(1);
        
        // En lugar de desactivar el bolo, desactivamos la física para evitar problemas
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        // Opcionalmente puedes esconderlo en lugar de desactivarlo
        gameObject.SetActive(false);
    }

    public void ReiniciarBolo()
    {
        // Reactivar el objeto antes de moverlo
        gameObject.SetActive(true);

        // Resetear posición y rotación
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        // Resetear física
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        derribado = false;
    }
}