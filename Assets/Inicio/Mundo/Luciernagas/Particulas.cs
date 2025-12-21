using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class Particulas : MonoBehaviour
{
    public ParticleSystem particulas;

    float tiempo;
    private Coroutine RutinaCambio;

    private Gradient Inicial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var main = particulas.colorOverLifetime;
        //main.color = Color.white;
    }
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
            RutinaCambio = StartCoroutine(Transicion(Color.gray));
            Debug.Log("Negro");
        }
        if (estadoMundo >= 0)
        {
            RutinaCambio = StartCoroutine(Transicion(Color.yellow));
            Debug.Log("Amarillo");
        }
    }

    IEnumerator Transicion(Color Final)
    {
        float duracion = 2.0f; // Tiempo que tarda en cambiar (2 segundos)
        float transcurrido = 0;

        while (transcurrido < duracion)
        {
            transcurrido += Time.deltaTime;
            float t = transcurrido / duracion; // Crea un valor de 0 a 1

            var main = particulas.colorOverLifetime;
            Inicial = main.color.gradient;

            // Aplicamos el Lerp
            Gradient nuevoGrad = InterpolarGradientes(Inicial, Final, t);

            main.color = new ParticleSystem.MinMaxGradient(nuevoGrad);

            yield return null; // Espera al siguiente frame
        }

    }


    Gradient InterpolarGradientes(Gradient inicial, Color destino, float t)
    {
        Gradient g = new Gradient();

        // Aquí interpolamos los colores de las llaves del gradiente
        // Para simplificar, haremos que todo el gradiente tienda al color destino
        GradientColorKey[] cKeys = inicial.colorKeys;
        for (int i = 0; i < cKeys.Length; i++)
        {
            cKeys[i].color = Color.Lerp(cKeys[i].color, destino, t);
        }

        // Mantenemos las transparencias (alpha) originales del gradiente
        g.SetKeys(cKeys, inicial.alphaKeys);
        return g;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
