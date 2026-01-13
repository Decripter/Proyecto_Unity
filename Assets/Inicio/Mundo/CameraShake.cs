using System;
using System.Collections;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 posicionOriginal;

    public void Shake(float duracion, float fuerza)
    {
        posicionOriginal = transform.localPosition;
        StartCoroutine(ProcesoShake(duracion, fuerza));
    }

    IEnumerator ProcesoShake(float duracion, float fuerza)
    {
        float transcurrido = 0f;
        while (transcurrido < duracion)
        {
            float x = UnityEngine.Random.Range(-1f, 2f) * fuerza;
            float y = UnityEngine.Random.Range(-1f, 2f) * fuerza;

            transform.localPosition = new Vector3(posicionOriginal.x + x, posicionOriginal.y + y, posicionOriginal.z);

            transcurrido += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = posicionOriginal;
    }
}

