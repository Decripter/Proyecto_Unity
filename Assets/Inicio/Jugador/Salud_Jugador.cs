using TMPro;
using UnityEngine;

public class Salud_Jugador : MonoBehaviour, Idanable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int SaludJugador;
    public TextMeshProUGUI info;

    private void Awake()
    {
        SaludJugador = 10;
    }

    private void Update()
    {
        info.text = "Salud: " + SaludJugador;
    }
    void Start()
    {
        
    }

    // Update is called once per frame

    public void CurarDano()
    {
        SaludJugador++;
        throw new System.NotImplementedException();
    }

    public void RecibirDano()
    {
        SaludJugador--;
        Debug.Log("recibiendo daño");
        throw new System.NotImplementedException();
    }
}
