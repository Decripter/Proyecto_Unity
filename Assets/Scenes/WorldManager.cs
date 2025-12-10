using TMPro;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public int estadomundo = 0;

    public TextMeshProUGUI info;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        info.text = "Numero: " + estadomundo;
    }

    public void Utopia()
    {
        
    }

    public int GetEstado()
    {
        return estadomundo;
    }
}