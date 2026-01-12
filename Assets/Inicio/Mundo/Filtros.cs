using UnityEngine;
using UnityEngine.Rendering;

public class Filtros : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Referencias de Volúmenes")]
    public Volume volumenUtopia;
    public Volume volumenDistopia;

    private void OnEnable()
    {
        // Nos suscribimos al evento del WorldManager
        WorldManager.Change += ActualizarPesos;
    }

    private void OnDisable()
    {
        WorldManager.Change -= ActualizarPesos;
    }

    void ActualizarPesos(int estadoMundo)
    {
        // Caso 1: Mundo en Utopía (Estado mayor a 0)
        if (estadoMundo > 0)
        {
            // Mapeamos de 0 a 10 para que el peso sea 0 a 1
            float t = Mathf.InverseLerp(0f, 10f, (float)estadoMundo);

            volumenUtopia.weight = t;
            volumenDistopia.weight = 0f; // Nos aseguramos que el otro esté apagado
        }
        // Caso 2: Mundo en Distopía (Estado menor a 0)
        else if (estadoMundo < 0)
        {
            // Mapeamos de 0 a -10 para que el peso sea 0 a 1
            float t = Mathf.InverseLerp(0f, -10f, (float)estadoMundo);

            volumenDistopia.weight = t;
            volumenUtopia.weight = 0f; // Nos aseguramos que el otro esté apagado
        }
        // Caso 3: Mundo Neutro (Estado exactamente 0)
        else
        {
            volumenUtopia.weight = 0f;
            volumenDistopia.weight = 0f;
        }
    }
}
