using TMPro;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    private int estadomundo = 0;
    public static Action<int> Change;

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
        Change?.Invoke(estadomundo);
    }
    public void Distopia()
    {
        estadomundo -= 1;
        Change?.Invoke(estadomundo);
    }

    public int GetEstado()
    {
        return estadomundo;
    }


}