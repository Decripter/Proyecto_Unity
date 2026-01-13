using UnityEngine;

public class Fondo : MonoBehaviour
{
    public SpriteRenderer fondoDist; // Para valor -10
    public SpriteRenderer fondoNeut; // Para valor 0
    public SpriteRenderer fondoUto;  // Para valor 10

    void Start()
    {
        OnChange(0);
    }
    private void OnEnable()
    {
        WorldManager.Change += OnChange;

    }
    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    void OnChange(int estado)
    {
        // t va de 0 a 1 (donde 0.5 es el centro/neutro)
        float t = Mathf.InverseLerp(-10f, 10f, (float)estado);

        if (t < 0.5f)
        {
            // Transición de Distópico a Neutro
            float mezcla = t / 0.5f;

            SetAlpha(fondoDist, 1 - mezcla);
            SetAlpha(fondoNeut, mezcla);
            SetAlpha(fondoUto, 0);
        }
        else
        {
            // Transición de Neutro a Utópico
            float mezcla = (t - 0.5f) / 0.5f;

            SetAlpha(fondoDist, 0);
            SetAlpha(fondoNeut, 1 - mezcla);
            SetAlpha(fondoUto, mezcla);
        }
    }

    void SetAlpha(SpriteRenderer spr, float alpha)
    {
        Color c = spr.color;
        c.a = alpha;
        spr.color = c;
    }
}
