using UnityEngine;

public class ControladorNiebla : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Colores de la atmósfera")]
    public Color colorDisto = new Color(0.2f, 0.1f, 0.2f); // Morado oscuro/Tóxico
    public Color colorUto = new Color(0.7f, 0.9f, 1f);     // Azul claro/Cielo

    [Header("Densidad de la niebla")]
    public float densidadDisto = 0.05f; // Mucha niebla (se ve poco)
    public float densidadUto = 0.005f;  // Casi nada de niebla (se ve lejos)

    private void OnEnable() => WorldManager.Change += ActualizarNiebla;
    private void OnDisable() => WorldManager.Change -= ActualizarNiebla;

    void ActualizarNiebla(int estado)
    {
        float t = WorldManager.instance.GetEstado();

        RenderSettings.fogColor = Color.Lerp(colorDisto, colorUto, t);

        RenderSettings.fogDensity = Mathf.Lerp(densidadDisto, densidadUto, t);

        RenderSettings.ambientLight = Color.Lerp(colorDisto * 0.5f, colorUto * 0.5f, t);
    }
}
