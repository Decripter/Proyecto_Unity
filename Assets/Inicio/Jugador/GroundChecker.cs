using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask Suelo;
    public float RadioEsfera = 0.2f;
    public bool Tocando => _Tocando;
    private bool _Tocando;

    void Start()
    {
    }

    void Update()
    {
        _Tocando = Physics.CheckSphere(transform.position, RadioEsfera, Suelo);
        if (_Tocando)
        {
            //Debug.Log("Estoy tocando suelo");
        }

    }
}
