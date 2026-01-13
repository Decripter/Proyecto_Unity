using System;
using System.Data;

using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable, Imatable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator _Animator;

    private CharacterController _Controller;
    public Transform player;
    public float detectionRadius = 5f;
    public float speed = 2f;
    public SpriteRenderer _SpriteRenderer;

    public float aceleracion = 5f;
    public float TargetSpeed;
    public Vector3 currentSpeed;
    private Vector2 movement;

    [SerializeField]
    private GroundChecker _GroundChecker;

    private Vector3 fuerza;
    public float gravedad = -9.8f;

    private bool pacifico;

    [SerializeField]
    private GameObject mensaje;
    private Renderer _renderer;
    [SerializeField]
    private TextMeshProUGUI _texto;
    private bool _interactuable = true;

    private bool jugadorestacerca;
    private bool Muerto;

    private void OnEnable()
    {
        jugadorestacerca = false;
        WorldManager.Change += OnChange;
    }
    private void OnDisable()
    {
        WorldManager.Change -= OnChange;
    }
    private void OnChange(int estadoMundo)
    {
        
        if(!Muerto)
        {
            _Animator.SetInteger("estadomundo", estadoMundo);
        }
        
        if (estadoMundo < 0)
        {
            pacifico = false;
            detectionRadius = 5f;
        }
        if (estadoMundo >= 0)
        {
            pacifico = true;
            detectionRadius = 0f;
            _Animator.SetFloat("movement", 0);
        }
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _Controller = GetComponent<CharacterController>();

        if (mensaje != null)
        {
            mensaje.SetActive(false);
        }
        //_texto = GetComponentInChildren<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jugadorestacerca)
        { }

        if (!Muerto)
        {
            mover();
        }
        AplicarGravedad();
    }

    private void mover()
    {
        float distanciaJugador = Vector2.Distance(transform.position, player.position);


        if (distanciaJugador < detectionRadius)
        {
            Vector3 dirHuir = transform.position - player.position;

            // Lo normalizamos para que solo nos de la dirección (valor de 1)
            movement = new Vector2(dirHuir.x, 0).normalized;
        }

        /*if(distanciaJugador > detectionRadius)
        {
            //set idle
        }*/

        else
        {
            movement = Vector2.zero;
        }

        if (movement.x < 0)
        {
            _SpriteRenderer.flipX = true;
        }
        if (movement.x > 0)
        {
            _SpriteRenderer.flipX = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if(!Muerto)
        {
            mensaje.SetActive(true);
            jugadorestacerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        mensaje.SetActive(false);
        jugadorestacerca = false;
    }


    public void Interactuar()
    {
        if(_interactuable && !Muerto)
        {
            WorldManager.instance.Estado(3);
            Debug.Log("Has ayudado a un Npc");

            _renderer.material.color = Color.white;
            _texto.text = "Ayudado!";
            _interactuable = false;  
        }
    }

    public void Matar()
    {
        if(_interactuable)
        {
            WorldManager.instance.Estado(-3);
            Debug.Log("Has matado a un Npc");
            Muerto = true;
            _Animator.SetBool("Muerto", Muerto);
            //Destroy(gameObject);
        }
    }

}
