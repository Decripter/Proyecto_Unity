using UnityEngine;

public class Interac_Jugador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool jugadorestacerca;
    private IInteractable _interactuableActual;
    private Imatable _matableActual;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && jugadorestacerca)
        {
            _interactuableActual.Interactuar();
        }

        if (Input.GetKeyDown(KeyCode.F) && jugadorestacerca)
        {
            _matableActual.Matar();
            Limpiar();
        }
    }

    private void Limpiar()
    {
        _matableActual = null;
        _interactuableActual = null;
        jugadorestacerca = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        jugadorestacerca = true;
        //Debug.Log(other.gameObject);
        if(other.TryGetComponent(out IInteractable interactable))
        {
            _interactuableActual = interactable;
        }

        if (other.TryGetComponent(out Imatable Matable))
        {
            _matableActual = Matable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        jugadorestacerca = false;
        if(other.TryGetComponent(out IInteractable interactable))
        {
            if(interactable == _interactuableActual)
            {
                _interactuableActual = null;
            }
        }

        if (other.TryGetComponent(out Imatable Matable))
        {
            if (Matable == _matableActual)
            {
                _matableActual = null;
            }
        }

    }
}
