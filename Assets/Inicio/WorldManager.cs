using TMPro;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{
    public AudioSource Lluvia;
    public AudioSource Pajaros;

    public static WorldManager instance;
    public int estadomundo = 0;
    public static Action<int> Change;

    public TextMeshProUGUI info;


    [Header("Mecánica de Altura")]
    public Transform jugador;
    private float alturaMaximaAlcanzada = 0f;
    public float sensibilidadSubida = 1f; // Cuánto sube el valor por cada metro nuevo
    public float penalizacionCaida = 2f;  // Cuánto baja el valor al caer

    public float ProximoCambio;
    public float intervaloUtopico = 2f; // Cada cuánto tiempo sucede
    public float intervaloDistopico = 3f; // Cada cuánto tiempo sucede
    private void OnEnable()
    {
        WorldManager.Change += OnChange;
    }
    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    private void OnChange(int estadoMundo)
    {
        float t1 = Mathf.InverseLerp(-10f, 0f, (float)estadoMundo);
        float t2 = Mathf.InverseLerp(0f, 10f, (float)estadoMundo);

        if (estadoMundo < 0)
        {
            Lluvia.volume = Mathf.Lerp(1f, 0f, t1);
        }
        if(estadoMundo > 0)
        {
            Pajaros.volume = Mathf.Lerp(0f, 1f, t2);
        }
        if(estadoMundo == 0)
        {
            Lluvia.volume = 0;
            Pajaros.volume = 0;
        }

    }

    private void Awake()
    {
        instance = this;
        estadomundo = 0;
    }

    private void Update()
    {
        info.text = "Mundo: " + estadomundo;

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Estado(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Estado(-1);
        }

        AlturaJugador();
    }

    private void AlturaJugador()
    {
        float alturaActual = jugador.position.y;

        // CASO A: El jugador está superando su récord (EL MUNDO MEJORA)
        if (alturaActual > alturaMaximaAlcanzada)
        {
            // Calculamos cuánto ha superado el récord en este frame
            float diferencia = alturaActual - alturaMaximaAlcanzada;

            if (Time.time >= ProximoCambio && estadomundo <= 10)
            {
                    Estado(1);
                    ProximoCambio = Time.time + intervaloUtopico;
                    alturaMaximaAlcanzada = alturaActual;
            }

        }
        
        
        // CASO B: El jugador ha caído por debajo de su récord (EL MUNDO EMPEORA)
        if (alturaActual < alturaMaximaAlcanzada - 3f) // Un pequeño margen para evitar errores
        {

            if (Time.time >= ProximoCambio && estadomundo >= -10)
            {
                Estado(-1);
                ProximoCambio = Time.time + intervaloDistopico;
                
            }
        }


    }

    public void Estado(int valor)
    {
        estadomundo += valor;
        Change?.Invoke(estadomundo);
    }
    public float GetEstado()
    {
        return Mathf.InverseLerp(-10f,10f,estadomundo);
    }


}