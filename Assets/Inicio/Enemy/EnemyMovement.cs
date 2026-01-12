using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private CharacterController _Controller;
    public Transform player;
    public float detectionRadius = 5f;
    public float speed = 2f;
    public SpriteRenderer _SpriteRenderer;

    public float aceleracion = 5f;
    public float TargetSpeed;
    public Vector3 currentSpeed;

    private Vector2 movement;

    public Animator _Animator;

    [SerializeField]
    private GroundChecker _GroundChecker;

    private Vector3 fuerza;
    public float gravedad = -9.8f;

    private bool pacifico;
    private int dano;
    public int danomaximo;

    private float tamano;
    public float tamanomaximo;
    

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
        float tDistopia = Mathf.InverseLerp(-10f, 0f, (float)estadoMundo);
        dano = (int)Mathf.Lerp(danomaximo, 0, tDistopia);

        float intensidadRoja = Mathf.InverseLerp(0, danomaximo, dano);
        _SpriteRenderer.color = Color.Lerp(Color.white, Color.red, intensidadRoja);


        tamano = Mathf.Lerp(tamanomaximo, 1, tDistopia);
        transform.localScale = new Vector3(tamano, tamano, tamano);

        if (estadoMundo < 0)
        {
            pacifico = false;
            detectionRadius = 5f;
        }
        if(estadoMundo >= 0) 
        {
            pacifico = true;
            detectionRadius = 0f;
            _Animator.SetFloat("movement", 0);
        }
    }
    void Start()
    {
        _Controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if(!pacifico)
        {
            mover();
            AplicarGravedad();
        }
        
    }
         
    private void mover()
    {
        float distanciaJugador = Vector2.Distance(transform.position, player.position);


        if(distanciaJugador < detectionRadius)
        {
            Vector2 direction = (player.position-transform.position).normalized;
            movement = new Vector2 (direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }

        if (movement.x < 0)
        {
            _SpriteRenderer.flipX = false;
        }
        if (movement.x > 0)
        {
            _SpriteRenderer.flipX = true;
        }

        Vector3 movertarget = (transform.right * movement.x) * speed; //El move original


        _Animator.SetFloat("movement", movement.x * speed);


        currentSpeed = Vector3.Lerp(currentSpeed, movertarget, aceleracion * Time.deltaTime);

        _Controller.Move(currentSpeed * Time.deltaTime);
    }

    private void AplicarGravedad()
    {

        if (_GroundChecker.Tocando && fuerza.y < 0)
        {
            fuerza.y = -2f;
        }

        fuerza.y += gravedad * Time.deltaTime;
        _Controller.Move(fuerza * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!pacifico)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Vector2 direcciondano = new Vector2(transform.position.x, 0);
                other.gameObject.GetComponent<Movement>().RecibeDano(direcciondano, dano);

                Debug.Log(other);
            }
        }

    }
}
