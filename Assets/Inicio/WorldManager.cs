using TMPro;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    private int estadomundo = 0;

    public TextMeshProUGUI info;

    private void Awake()
    {
        instance = this;
        estadomundo = 0;
    }

    private void Update()
    {
        info.text = "Mundo: " + estadomundo;
    }

    public void Utopia()
    {
        estadomundo += 1;
    }
    public void Distopia()
    {
        estadomundo -= 1;
    }

    public int GetEstado()
    {
        return estadomundo;
    }
}