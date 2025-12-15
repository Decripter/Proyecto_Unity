using System.Data;

using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable, Imatable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private GameObject mensaje;
    private Renderer _renderer;
    [SerializeField]
    private TextMeshProUGUI _texto;
    private bool _interactuable = true;

    bool jugadorestacerca;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        
        if (mensaje != null)
        {
            mensaje.SetActive(false);
        }
        //_texto = GetComponentInChildren<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        mensaje.SetActive(true);
        jugadorestacerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mensaje.SetActive(false);
        jugadorestacerca = false;
    }

    public void Interactuar()
    {
        if(_interactuable)
        {
            WorldManager.instance.Utopia();
            Debug.Log("Has hablado con un Npc");

            _renderer.material.color = Color.white;
            _texto.text = "Ayudado!";
            _interactuable = false;  
        }
    }

    public void Matar()
    {
        if(_interactuable)
        {
            WorldManager.instance.Distopia();
            Debug.Log("Has matado a un Npc");
            Destroy(gameObject);
        }
        
    }

    
}
