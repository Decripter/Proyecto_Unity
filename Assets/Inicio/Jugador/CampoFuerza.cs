using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class CampoFuerza : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystemForceField ForceField;

    private void OnEnable()
    {
        WorldManager.Change += OnChange;
    }

    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    private void OnChange(int estadoMundo)
    {
        if (estadoMundo > 0)
        {
            ForceField.gravity = 0.5f;
        }

        if (estadoMundo < 0)
        {
            ForceField.gravity = 25;
        }
        
    }

        void Start()
                {
                }

    // Update is called once per frame
    void Update()
    {
        
    }
}
