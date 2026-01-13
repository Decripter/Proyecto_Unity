using UnityEngine;

public class IluminacionGlobal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light sol;

    [Header("Configuración de Intensidad")]
    public float intensidadDisto = 0.1f; // Casi oscuro
    public float intensidadUto = 1.2f;   // Muy brillante

    [Header("Configuración de Color")]
    public Color colorDisto = new Color(0.1f, 0.05f, 0.2f); // Morado/Noche
    public Color colorUto = new Color(1f, 0.95f, 0.8f);    // Cálido/Día

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
        float t = WorldManager.instance.GetEstado(); // 0 a 1
        
        float anguloX = Mathf.Lerp(-20f, 170f, t);
        sol.transform.rotation = Quaternion.Euler(anguloX, -30f, 0f);


        sol.intensity = Mathf.Lerp(intensidadDisto, intensidadUto, t);
        sol.color = Color.Lerp(colorDisto, colorUto, t);


        RenderSettings.ambientIntensity = t;
        RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(0.2f, 1.0f, t));
        RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(Color.black, Color.gray, t));
    }
}
