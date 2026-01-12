using System;
using System.Collections;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer _hojas;
    public Renderer _palo;

    public Material _hojasDist;
    public Material _paloDist;
    public Material _hojasUto;
    public Material _paloUto;

    private Color _hojasinicial;
    private Color _paloinicial;

    float tiempo;
    private Coroutine rutinaCambio;

    float PorcentajeObjetivo;
    float PorcentajeActual;
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
        tiempo += Time.deltaTime;

        float porcentaje = Mathf.InverseLerp(-10f, 10f, (float)estadoMundo);
        Actualizar(porcentaje);
        PorcentajeObjetivo = Mathf.InverseLerp(-10f, 10f, (float)estadoMundo);
        /*if (estadoMundo < 0)
        {
            rutinaCambio = StartCoroutine(Transicion(_hojasDist, _paloDist));
        }
        if (estadoMundo >= 0)
        {
            rutinaCambio = StartCoroutine(Transicion(_hojasUto, _paloUto));
        }*/
    }

    private void Actualizar(float porcentaje)
    {
        _hojas.material.color = Color.Lerp(_hojasDist.color, _hojasUto.color, porcentaje);
        _palo.material.color = Color.Lerp(_paloDist.color, _paloUto.color, porcentaje);
    }

    ///Lerp Perrón
    IEnumerator Transicion(Material hojasDist, Material paloDist)
    {
        float duracion = 2.0f; // Tiempo que tarda en cambiar (2 segundos)
        float transcurrido = 0;

        while (transcurrido < duracion)
        {
            transcurrido += Time.deltaTime;
            float t = transcurrido / duracion; // Crea un valor de 0 a 1

            _hojasinicial = _hojas.material.color;
            _paloinicial = _palo.material.color;

    // Aplicamos el Lerp
            _hojas.material.color = Color.Lerp(_hojasinicial, hojasDist.color, t);
            _palo.material.color = Color.Lerp(_paloinicial, paloDist.color, t);

            yield return null; // Espera al siguiente frame
        }

        _hojas.material.color = hojasDist.color;
        _palo.material.color = paloDist.color;
    }


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
}
