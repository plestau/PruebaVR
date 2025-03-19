using UnityEngine;
using TMPro;
using System.Collections;

public class PanelPuntuacion : MonoBehaviour
{
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float duracion = 1.5f;
    public TextMeshProUGUI totalPuntuacionTexto, turnoPuntuacionTexto, turnoTexto, tiradaText, indicadorTurno, indicadorTirada, indicadorPuntuacion;

    void Start()
    {
        transform.position = posicionInicial;
        indicadorTurno.text = "Turno: 1";
        indicadorTirada.text = "Tirada: 1";
        indicadorPuntuacion.text = "Puntuación: 0";
    }

    public void MostrarPanel(int totalPuntuacion, int turnoPuntuacion, int numeroTurno, int numeroTirada)
    {
        totalPuntuacionTexto.text = "Total: " + totalPuntuacion;
        turnoPuntuacionTexto.text = "Tirados: " + turnoPuntuacion;
        turnoTexto.text = "Turno: " + numeroTurno;
        tiradaText.text = "Tirada: " + numeroTirada;
        StartCoroutine(MoverPanel(posicionFinal));
    }
    
    public void ActualizarMarcador()
    {
        indicadorTirada.text = "Tirada: " + (GameManager.Instance.numeroTirada + 1);
        indicadorTurno.text = "Turno: " + GameManager.Instance.numeroTurno;
        indicadorPuntuacion.text = "Puntuación: " + GameManager.Instance.puntuacion;
        if(indicadorTirada.text == "Tirada: 3")
        {
            indicadorTurno.text = "Turno: " + (GameManager.Instance.numeroTurno + 1);
            indicadorTirada.text = "Tirada: 1";
        }
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