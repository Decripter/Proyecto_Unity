using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class Agua : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer _renderer;
    public Material _Agua;
    public Material _Lava;
    public Color EmisionLava;
    public Color EmisionAgua;


    float tiempo;
    private Coroutine RutinaCambio;

    private Color Inicial;
    private Color InicialEm;

    private Idanable danableactual;

    private int estado;

    private void OnEnable()
    {
        WorldManager.Change += OnChange;
        EmisionLava = _Lava.GetColor("_EmissionColor");
        EmisionAgua = _Agua.GetColor("_EmissionColor");
    }
    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    private void OnChange(int estadoMundo)
    {
        tiempo += Time.deltaTime * 1;
        if (estadoMundo < 0)
        {
            RutinaCambio = StartCoroutine(Transicion(_Lava, EmisionLava));
            //Debug.Log("Lava");
        }
        if (estadoMundo >= 0)
        {

            RutinaCambio = StartCoroutine(Transicion(_Agua, EmisionAgua));
            //Debug.Log("Agua");
        }
        estado = estadoMundo;
    }

    IEnumerator Transicion(Material Final, Color EmisionFinal)
    {
        float duracion = 2.0f; // Tiempo que tarda en cambiar (2 segundos)
        float transcurrido = 0;

        while (transcurrido < duracion)
        {
            transcurrido += Time.deltaTime;
            float t = transcurrido / duracion; // Crea un valor de 0 a 1

            Inicial = _renderer.material.color;
            InicialEm = _renderer.material.GetColor("_EmissionColor");

            // Aplicamos el Lerp
            _renderer.material.color = Color.Lerp(Inicial, Final.color, t);
            Color colorInterp = Color.Lerp(InicialEm, EmisionFinal, t);

            _renderer.material.SetColor("_EmissionColor", colorInterp);

            yield return null; // Espera al siguiente frame
        }

        _renderer.material.color = Final.color;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Idanable idanable))
        {
            //danableactual = idanable;
            //danableactual.RecibirDano();
            Coroutine rutina = StartCoroutine(DanoPeriodico(idanable));
        }

        Debug.Log(other);

    }
    IEnumerator DanoPeriodico(Idanable victima)
    {
        if(estado < 0)
        {
            while (true) // Bucle infinito mientras la corrutina esté viva
            {
                victima.RecibirDano();
                Debug.Log("Recibiendo daño");
                yield return new WaitForSeconds(0.5f);
            }
        }

        if (estado >= 0)
        {
            while (true) // Bucle infinito mientras la corrutina esté viva
            {
                victima.CurarDano();
                Debug.Log("Curando daño");
                yield return new WaitForSeconds(0.5f);
            }
        }

    }
}
