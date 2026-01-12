using System;
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

using Random = UnityEngine.Random;

public class EfectoRayo : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Arrastra el CanvasGroup del objeto aquí
    public CameraShake cameraShake;
    public AudioSource trueno;

    private void OnEnable()
    {
        WorldManager.Change += IntentarRayo;
    }
    
    private void OnDisable()
    {
     WorldManager.Change -= IntentarRayo;
    }

    void IntentarRayo(int estado)
    {
        // Solo hay rayos si el mundo está muy mal (ej. menor a -5)
        if (estado < -5)
        {
            // Un 10% de probabilidad de que caiga un rayo cuando el mundo cambia
            if (UnityEngine.Random.value < 2f)
            {
                StartCoroutine(DispararRayo());
            }
        }
    }
    IEnumerator DispararRayo()
    {
        canvasGroup.alpha = 0.8f; // Aparece de golpe
        while (canvasGroup.alpha > 0)
        {
            cameraShake.Shake(0.2f, 0.3f);
            canvasGroup.alpha -= Time.deltaTime * 2f; // Se desvanece
            trueno.Play();
            yield return null;
        }
    }
}
