using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class Agua : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer _renderer2;
    public Material _Agua;
    public Material _neutro;
    public Material _Lava;

    public Color EmisionLava;
    public Color EmisionAgua;
    public Color EmisionNeutra;

    float PorcentajeObjetivo;
    float PorcentajeActual;

    float tiempo;

    private Color Inicial;
    private Color InicialEm;

    private Idanable danableactual;

    private int estado;

    public float intervalo = 2f; // Cada cuánto tiempo sucede
    private float proximoDano;

    void Start()
    {
        OnChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        PorcentajeActual = Mathf.MoveTowards(PorcentajeActual, PorcentajeObjetivo, Time.deltaTime * 0.5f);
        Actualizar(PorcentajeActual);
    }

    private void OnEnable()
    {
        WorldManager.Change += OnChange;
        EmisionLava = _Lava.GetColor("_EmissionColor");
        EmisionAgua = _Agua.GetColor("_EmissionColor");
        EmisionNeutra = _neutro.GetColor("_EmissionColor");
    }
    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    private void OnChange(int estadoMundo)
    {
        tiempo += Time.deltaTime;

        float porcentaje = Mathf.InverseLerp(-2f, 2f, (float)estadoMundo);
        Actualizar(porcentaje);
        PorcentajeObjetivo = Mathf.InverseLerp(-2f, 2f, (float)estadoMundo);
        estado = estadoMundo; //Actualizar el valor, para que jale aplicar daño
    }

    private void Actualizar(float porcentaje)
    {
        if(estado < 0)
        {
            float t = Mathf.InverseLerp(-10f, 0f, (float)estado);
            _renderer2.material.color = Color.Lerp(_Lava.color, _neutro.color, t);
            Color colorInterp = Color.Lerp(EmisionLava, EmisionNeutra, t);
            _renderer2.material.SetColor("_EmissionColor", colorInterp);
        }
        else
        {
            float t = Mathf.InverseLerp(0f, 10f, (float)estado);
            _renderer2.material.color = Color.Lerp(_neutro.color, _Agua.color, t);
            Color colorInterp2 = Color.Lerp(EmisionNeutra, EmisionAgua, t);
            _renderer2.material.SetColor("_EmissionColor", colorInterp2);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Idanable idanable))
        {         
            if (Time.time >= proximoDano)
            {
                if(other.CompareTag("Player"))
                {
                    DanoPeriodico(other, idanable);
                    
                    proximoDano = Time.time + intervalo;
                }
            }
        }

        //Debug.Log(other);
    }

    private void DanoPeriodico(Collider other, Idanable victima)
    {
        if (estado < 0)
        {
            Vector2 direcciondano = new Vector2(transform.position.x, 0);
            other.gameObject.GetComponent<Movement>().RecibeDano(direcciondano, 5);
                //Debug.Log("Recibiendo daño");
        }

        if (estado > 0)
        {
                victima.CurarDano();
                //Debug.Log("Curando daño");  
        }
    }
}
