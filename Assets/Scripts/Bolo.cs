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

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        gameObject.SetActive(false);
    }

    public void ReiniciarBolo()
    {
        gameObject.SetActive(true);

        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        derribado = false;
    }
}