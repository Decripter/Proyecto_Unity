using System;
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

using Random = UnityEngine.Random;

public class EfectoRayo : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public CameraShake cameraShake;
    public AudioSource trueno;
    private int estadoActualMundo;

    private void OnEnable() => WorldManager.Change += ActualizarEstado;
    private void OnDisable() => WorldManager.Change -= ActualizarEstado;

    void ActualizarEstado(int estado) => estadoActualMundo = estado;

    void Start()
    {
        // ESTO inicia el ciclo infinito que se mantiene activo siempre
        StartCoroutine(BucleAmbiental());
    }

    IEnumerator BucleAmbiental()
    {
        while (true) // Se mantiene activo mientras el objeto exista
        {
            // Esperamos 2 segundos entre cada "intento" de rayo
            yield return new WaitForSeconds(2f);

            // Solo intentamos si el mundo está mal
            if (estadoActualMundo <= -5)
            {
                if (Random.value < 0.2f) // 50% de probabilidad
                {
                    // Ejecutamos el efecto visual (sin detener este bucle)
                    StartCoroutine(EjecutarEfectoVisual());
                }
            }
        }
    }

    IEnumerator EjecutarEfectoVisual()
    {
        // 1. El estallido inicial (Sonido y Shake se activan UNA vez)
        canvasGroup.alpha = 0.8f;
        trueno.Play();
        cameraShake.Shake(0.2f, 0.3f);

        // 2. El desvanecimiento suave (esto sí es un bucle rápido de frames)
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2f; // Se desvanece en medio segundo aprox
            yield return null; // Espera al siguiente frame
        }
    }

}
