using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static PanelPuntuacion panelPuntuacion;
    internal int puntuacion = 0;
    internal int bolosDerribados = 0;
    private Turno turnoActual;
    private bool delayIniciado = false;
    internal int numeroTurno = 1;
    private const int maxTurnos = 10;
    internal int numeroTirada = 1;
    private Bolo[] bolos;

    public AudioClip sonidoPleno;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (panelPuntuacion == null)
        {
            panelPuntuacion = FindFirstObjectByType<PanelPuntuacion>();
        }

        turnoActual = new Turno();
        bolos = FindObjectsOfType<Bolo>();
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator DelayMostrarPanel()
    {
        yield return new WaitForSeconds(1);
        delayIniciado = false;
    }

    public void BoloDerribado()
    {
        bolosDerribados++;
    }

    public void TiradaAcertada()
    {
        StartCoroutine(DelayPuntuar());
    }

    private IEnumerator DelayPuntuar()
    {

        yield return new WaitForSeconds(1);

        puntuacion += bolosDerribados;
        panelPuntuacion.MostrarPanel(puntuacion, bolosDerribados, numeroTurno, numeroTirada);
        panelPuntuacion.ActualizarMarcador();
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }

        if (bolosDerribados == 10 && numeroTirada == 1)
        {
            AccionEspecialPleno();
        }

        numeroTirada++;
        turnoActual.TiradaRealizada();
    }

    private void AccionEspecialPleno()
    {
        if (sonidoPleno != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoPleno);
        }
        // muestra el panel de puntuacion diciendo que es un pleno
        panelPuntuacion.MostrarPanel(puntuacion, 10/10, numeroTurno, numeroTirada);
        panelPuntuacion.ActualizarMarcador();
        if (!delayIniciado)
        {
            delayIniciado = true;
            StartCoroutine(DelayMostrarPanel());
        }
        ReiniciarBolos();
    }

    public void TiradaFallida()
    {
        panelPuntuacion.MostrarPanel(puntuacion, 0, numeroTurno, numeroTirada);
        panelPuntuacion.ActualizarMarcador();
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