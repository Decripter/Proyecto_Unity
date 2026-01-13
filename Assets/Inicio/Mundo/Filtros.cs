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
        if (estadoMundo > 0)
        {
            float t = Mathf.InverseLerp(0f, 10f, (float)estadoMundo);

            volumenUtopia.weight = t;
            volumenDistopia.weight = 0f; 
        }
        else if (estadoMundo < 0)
        {
            float t = Mathf.InverseLerp(0f, -10f, (float)estadoMundo);

            volumenDistopia.weight = t;
            volumenUtopia.weight = 0f; 
        }
        else
        {
            volumenUtopia.weight = 0f;
            volumenDistopia.weight = 0f;
        }
    }
}
