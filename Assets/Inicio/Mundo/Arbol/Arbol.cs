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
        tiempo += Time.deltaTime * 1;
        if (estadoMundo < 0)
        {
            rutinaCambio = StartCoroutine(Transicion(_hojasDist, _paloDist));
        }
        if (estadoMundo >= 0)
        {
            rutinaCambio = StartCoroutine(Transicion(_hojasUto, _paloUto));
        }
    }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
