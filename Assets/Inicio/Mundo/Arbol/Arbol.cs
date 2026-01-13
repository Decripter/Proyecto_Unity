using System;
using System.Collections;
using UnityEngine;

public class Arbol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer _hojas;

    public Material _hojasDist;
    public Material _hojasUto;
    public Material _hojasNeutro;

    private Color _hojasinicial;
    private Color _paloinicial;

    float tiempo;
    private Coroutine rutinaCambio;

    float PorcentajeObjetivo;
    float PorcentajeActual;

    private int estado;
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

        Actualizar();
        PorcentajeObjetivo = Mathf.InverseLerp(-10f, 10f, (float)estadoMundo);

        estado = estadoMundo;
    }

    private void Actualizar()
    {
        if(estado < 0)
        {
            float t = Mathf.InverseLerp(-10f, 0f, (float)estado);
            _hojas.material.color = Color.Lerp(_hojasDist.color, _hojasNeutro.color, t);
        }

        else
        {
            float t = Mathf.InverseLerp(0f, 10f, (float)estado);
            _hojas.material.color = Color.Lerp(_hojasNeutro.color, _hojasUto.color, t);
        }

    }

    void Start()
    {
            OnChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        Actualizar();
    }
}
