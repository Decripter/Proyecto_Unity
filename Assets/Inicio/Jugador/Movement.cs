using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController _Controller;
    public float AirSpeed;
    public float GroundSpeed;
    public SpriteRenderer _SpriteRenderer;

    public float jumpspeed;

    public float aceleracion;
    public float speed;
    public float TargetSpeed;
    public Vector3 currentSpeed;


    private Vector3 fuerza;
    public float gravedad = -9.8f;

    [SerializeField]
    private GroundChecker _GroundChecker;
    [SerializeField]
    private WallChecker _wallCheckerR;
    [SerializeField]
    private WallChecker _wallCheckerL;
    [SerializeField]
    private Salud_Jugador _Salud_Jugador;


    public Animator _Animator;
    private bool recibedano;

    public float fuerzarebote = 10f;
    void Start()
    {
        _Controller = GetComponent<CharacterController>();
        _GroundChecker = GetComponentInChildren<GroundChecker>();
    }

    
    void Update()
    {
        if(!recibedano)
        {
            mover();
        }
        
        AplicarGravedad();
        if (Input.GetKeyDown(KeyCode.Space) && (_GroundChecker.Tocando || _wallCheckerL.Tocando || _wallCheckerR.Tocando))
        {
            /*if(Input )
            {

            }*/
            if(!recibedano)
            {
                salto();
            }

        }
        _Animator.SetBool("Ensuelo", _GroundChecker.Tocando);
        _Animator.SetBool("RecibeDano", recibedano);

    }

    private void salto()
    {
            fuerza.y = jumpspeed;
    }

    private void mover()
    {

        if (_GroundChecker.Tocando)
        {
            speed = GroundSpeed;
        }
        else
        {
            speed = AirSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        
        if (x<0)
        {
            //transform.localScale = new Vector3(-1,1,1);
            _SpriteRenderer.flipX = true;
        }
        if (x>0)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            _SpriteRenderer.flipX = false;
        }

        Vector3 movertarget = (transform.right * x) * speed; //El move original

        _Animator.SetFloat("movement", x * speed);

        currentSpeed = Vector3.Lerp(currentSpeed, movertarget, aceleracion * Time.deltaTime);

       _Controller.Move(currentSpeed * Time.deltaTime);
    }

    private void AplicarGravedad()
    {

        if(_GroundChecker.Tocando && fuerza.y < 0)
        {
            fuerza.y = -2f;
        }

        fuerza.y += gravedad * Time.deltaTime;
        

        float friccion = 5f; // Cuanto más alto, más rápido se detiene el rebote
        fuerza.x = Mathf.MoveTowards(fuerza.x, 0, friccion * Time.deltaTime);

        _Controller.Move(fuerza * Time.deltaTime);
    }

    public void RecibeDano(Vector2 direccion, int cantDanio)
    {
        if(!recibedano)
        {
            recibedano = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;

            _Salud_Jugador.RecibirDano(cantDanio);
            fuerza = rebote*fuerzarebote;
        }

    }

    public void desactivadano()
    {
        recibedano = false;
    }

}
