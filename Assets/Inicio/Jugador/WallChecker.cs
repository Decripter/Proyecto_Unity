using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public LayerMask Pared;
    public float RadioEsfera = 0.2f;
    public bool Tocando => _Tocando;
    private bool _Tocando;

    void Start()
    {
    }

    void Update()
    {
        _Tocando = Physics.CheckSphere(transform.position, RadioEsfera, Pared);
        if (_Tocando)
        {
            //Debug.Log("Estoy tocando una pared");
        }

    }
}
