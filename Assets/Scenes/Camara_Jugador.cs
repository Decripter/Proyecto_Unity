using Unity.VisualScripting;
using UnityEngine;

public class Camara_Jugador : MonoBehaviour
{
    public float limiteX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        var posx = Mathf.Clamp(transform.position.x, -limiteX, limiteX);
        transform.position = new Vector3(posx, transform.position.y,-10 );
    }
}
