using UnityEngine;

public class Bolo : MonoBehaviour
{
    private bool derribado = false;
    public float rotacionMaxima = 25f;

    void OnCollisionEnter(Collision collision)
    {
        if (!derribado && collision.gameObject.CompareTag("Bolo"))
        {
            Vector3 rotacion = transform.eulerAngles;
            if (Mathf.Abs(rotacion.x) > rotacionMaxima || Mathf.Abs(rotacion.z) > rotacionMaxima)
            {
                derribado = true;
                GameManager.Instance.ContarBolo();
            }
        }
    }
}