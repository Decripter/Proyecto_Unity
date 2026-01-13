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
            // 2 seg entre rayos
            yield return new WaitForSeconds(2f);

            if (estadoActualMundo <= -5)
            {
                if (Random.value < 0.2f)
                {
                    StartCoroutine(EjecutarEfectoVisual());
                }
            }
        }
    }

    IEnumerator EjecutarEfectoVisual()
    {
        canvasGroup.alpha = 0.8f;
        trueno.Play();
        cameraShake.Shake(0.2f, 0.3f);

        // 2. El desvanecimiento suave
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2f;
            yield return null;
        }
    }

}
