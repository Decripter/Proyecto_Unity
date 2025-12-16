using UnityEngine;

public class Arbol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer _hojas;
    public Renderer _palo;

    public Material _hojasDist;
    public Material _paloDist;
    public Material _hojasUto;
    public Material _palodUto;
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
        //float lerp = Mathf.PingPong(Time.time, 2f) / 2f;
        if (estadoMundo < 0)
        {
            _hojas.material = _hojasDist;
            _palo.material = _paloDist;

            
        }
        if (estadoMundo >= 0)
        {
            _hojas.material = _hojasUto;
            _palo.material = _palodUto;
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
