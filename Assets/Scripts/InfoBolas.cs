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
        Ray ray = new Ray(vrController.position, vrController.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Bolas bola = hit.transform.GetComponent<Bolas>();
            if (bola != null)
            {
                bolaActual = bola;
            }
        }
    }
}