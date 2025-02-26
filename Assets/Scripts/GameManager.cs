using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static PanelPuntuacion panelPuntuacion;
    private int puntuacion = 0;
    private int bolosDerribados = 0;
    private Turno turnoActual;
    private bool delayIniciado = false;
    private int numeroTurno = 1;
    private const int maxTurnos = 10;
    private int numeroTirada = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (panelPuntuacion == null)
        {
            panelPuntuacion = FindObjectOfType<PanelPuntuacion>();
        }

        turnoActual = new Turno();
    }

    public void ContarBolo()
    {
        bolosDerribados++;
        Debug.Log("Bolos derribados: " + bolosDerribados);
        turnoActual.TirarBolo();
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }
    }

    public void TiradaFallida()
    {
        turnoActual.TiroFallido();
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }
    }

    private IEnumerator DelayMostrarPanel()
    {
        yield return new WaitForSeconds(2);
        FinalizarTirada();
        delayIniciado = false;
    }

    public void FinalizarTirada()
    {
        puntuacion += bolosDerribados;
        panelPuntuacion.MostrarPanel(puntuacion, bolosDerribados, numeroTurno, numeroTirada);
        bolosDerribados = 0;

        if (turnoActual.TiroTerminado())
        {
            if (numeroTurno >= maxTurnos)
            {
                TerminarJuego();
            }
            else
            {
                numeroTurno++;
                turnoActual.NuevoTurno();
                numeroTirada = 1; // Reset throw number for new turn
            }
        }
        else
        {
            numeroTirada = 2; // Set throw number for the second throw
        }
    }

    private void TerminarJuego()
    {
        Debug.Log("Juego terminado. Puntuación final: " + puntuacion);
    }
}

public class Turno
{
    private int tirosRestantes;

    public Turno()
    {
        NuevoTurno();
    }

    public void NuevoTurno()
    {
        tirosRestantes = 2;
    }

    public void TirarBolo()
    {
        tirosRestantes--;
    }

    public void TiroFallido()
    {
        tirosRestantes--;
    }

    public bool TiroTerminado()
    {
        return tirosRestantes <= 0;
    }
}