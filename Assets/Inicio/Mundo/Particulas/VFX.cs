using UnityEngine;

public class VFX : MonoBehaviour
{
    [Header("Sistemas de Partículas")]
    public ParticleSystem particulasAsteroides; // Aparecen en Distopía (-10)
    public ParticleSystem particulasSakura;     // Aparecen en Utopía (+10)

    [Header("Configuración Máxima")]
    public int maxAsteroides = 50;
    public int maxSakura = 50;

    private void OnEnable() => WorldManager.Change += OnChange;
    private void OnDisable() => WorldManager.Change -= OnChange;

    private void OnChange(int estadoMundo)
    {
        float t1 = Mathf.InverseLerp(-10f, 0f, (float)estadoMundo);
        float t2 = Mathf.InverseLerp(0f, 10f, (float)estadoMundo);

        var mainAsteroides = particulasAsteroides.main;
        var mainSakura = particulasSakura.main;

        if (estadoMundo < 0)
        {
            mainAsteroides.maxParticles = (int)Mathf.Lerp(maxAsteroides, 0, t1);
            mainSakura.maxParticles = 0;
        }
        else if (estadoMundo > 0)
        {
            mainSakura.maxParticles = (int)Mathf.Lerp(0, maxSakura, t2);
            mainAsteroides.maxParticles = 0;
        }
        else
        {
            mainAsteroides.maxParticles = 0;
            mainSakura.maxParticles = 0;
        }
    }
}
