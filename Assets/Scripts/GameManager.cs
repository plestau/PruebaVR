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
    private Bolo[] bolos;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (panelPuntuacion == null)
        {
            panelPuntuacion = FindObjectOfType<PanelPuntuacion>();
        }

        turnoActual = new Turno();
        bolos = FindObjectsOfType<Bolo>();
    }

    private IEnumerator DelayMostrarPanel()
    {
        yield return new WaitForSeconds(2);
        delayIniciado = false;
    }
    
    public void BoloDerribado()
    {
        bolosDerribados++;
    }

    public void TiradaAcertada()
    {
        Debug.Log("Tiro acertado. Bolos derribados: " + bolosDerribados);
        StartCoroutine(DelayPuntuar());
    }

    private IEnumerator DelayPuntuar()
    {
        yield return new WaitForSeconds(2);

        puntuacion += bolosDerribados;
        panelPuntuacion.MostrarPanel(puntuacion, bolosDerribados, numeroTurno, numeroTirada);
        Debug.Log("Puntuación: " + puntuacion);
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }
        numeroTirada++;
        turnoActual.TiradaRealizada();
    }

    public void TiradaFallida()
    {
        panelPuntuacion.MostrarPanel(puntuacion, 0, numeroTurno, numeroTirada);
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }
        numeroTirada++;
        turnoActual.TiradaRealizada();
    }
    
    public void SiguienteTurno()
    {
        if (numeroTurno < maxTurnos)
        {
            numeroTurno++;
            numeroTirada = 1;
            turnoActual.NuevoTurno();
            ReiniciarBolos();
        }
        else
        {
            TerminarJuego();
        }
    }
    
    public void ReiniciarBolosDerribados()
    {
        bolosDerribados = 0;
    }
    
    private void ReiniciarBolos()
    {
        foreach (Bolo bolo in bolos)
        {
            bolo.ReiniciarBolo();
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

    public void TiradaRealizada()
    {
        GameManager.Instance.ReiniciarBolosDerribados();
        tirosRestantes--;
        if (tirosRestantes <= 0)
        {
            GameManager.Instance.SiguienteTurno();
        }
    }
}