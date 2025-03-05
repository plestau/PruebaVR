using UnityEngine;
using TMPro;

public class InfoBolas : MonoBehaviour
{
    public TextMeshProUGUI pesoTexto;
    public TextMeshProUGUI colorTexto;
    private Bolas bolaActual;

    private Camera cameraMain;
    public Transform vrController; // Añade una referencia al controlador de VR

    void Start()
    {
        cameraMain = Camera.main;
    }

    void Update()
    {
        if (cameraMain != null)
        {
            transform.LookAt(cameraMain.transform);
            transform.Rotate(0, 180, 0);

            DetectBola();

            if (bolaActual != null)
            {
                pesoTexto.text = "Peso: " + bolaActual.peso;
                colorTexto.text = "Color: " + bolaActual.color;
            }
        }
    }

    void DetectBola()
    {
        if (vrController == null)
        {
            Debug.LogWarning("VR Controller is not assigned.");
            return;
        }

        Ray ray = new Ray(vrController.position, vrController.forward);
        RaycastHit hit;

        // Draw a debug line in the scene
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        // Log for debugging
        Debug.Log($"Ray Origin: {ray.origin}, Ray Direction: {ray.direction}");

        if (Physics.Raycast(ray, out hit))
        {
            Bolas bola = hit.transform.GetComponent<Bolas>();
            if (bola != null)
            {
                bolaActual = bola;
                Debug.Log($"Bola detected: {bola.peso}, {bola.color}");
            }
            else
            {
                Debug.Log("No Bola component found on the hit object.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
        }
    }
}