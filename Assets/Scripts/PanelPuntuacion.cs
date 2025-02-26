using UnityEngine;
using TMPro;
using System.Collections;

public class PanelPuntuacion : MonoBehaviour
{
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float duracion = 1.5f;
    public TextMeshProUGUI totalPuntuacionTexto, turnoPuntuacionTexto, turnoTexto, tiradaText;

    void Start()
    {
        transform.position = posicionInicial;
    }

    public void MostrarPanel(int totalPuntuacion, int turnoPuntuacion, int numeroTurno, int numeroTirada)
    {
        totalPuntuacionTexto.text = "Total: " + totalPuntuacion;
        turnoPuntuacionTexto.text = "Tirados: " + turnoPuntuacion;
        turnoTexto.text = "Turno: " + numeroTurno;
        tiradaText.text = "Tirada: " + numeroTirada;
        StartCoroutine(MoverPanel(posicionFinal));
    }

    IEnumerator MoverPanel(Vector3 objetivo)
    {
        float tiempo = 0;
        Vector3 inicio = transform.position;

        while (tiempo < duracion)
        {
            transform.position = Vector3.Lerp(inicio, objetivo, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = objetivo;

        yield return new WaitForSeconds(2);

        tiempo = 0;
        inicio = transform.position;
        objetivo = posicionInicial;

        while (tiempo < duracion)
        {
            transform.position = Vector3.Lerp(inicio, objetivo, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = objetivo;
    }
}