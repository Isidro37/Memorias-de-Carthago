using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class character_controller : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    private Animator animator;
    public LayerMask capaSuelo;
    public float umbral = 0.5f;
    public float umbralDeMirada = 0.1f;
    public Audio_Manager audioManager;
        public Audio_Manager pasosManager;

    public AudioClip sonidosalto;
    public AudioClip sonidopaso1;
    public AudioClip sonidopaso2;
    public AudioClip sonidopaso4;
    public bool sePuedeMover=true;
    [SerializeField] private Vector2 velocidadRebote= new Vector2(15,15);
    
    public AudioClip sonidopuñetazo;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private bool enProcesoDeSalto = false;
    public float tiempoEntreAtaques = 0.3f;
    private bool puedeAtacar = true;
    bool andado=true;
    bool parado=true;
    bool saltando=false;
    bool izquierda;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(sePuedeMover){
        ProcesarMovimiento();
        Salto();
        Pegar();
        }
        MoverReflejo();
    }

    public bool EstaenSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void Salto()
    {
        if(fuerzaSalto>0){
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && EstaenSuelo() && !enProcesoDeSalto)
        {
            enProcesoDeSalto = true;
            rigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audioManager.ReproducirSonido(sonidosalto);
            saltando=true;
        }

        if ((!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.JoystickButton0)) && rigidbody.velocity.y > 0)
        {
            rigidbody.AddForce(Vector2.down * fuerzaSalto / 10, ForceMode2D.Impulse);
        }
    }}

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        if (inputMovimiento != 0f) { animator.SetBool("isRunning", true); 
        parado=false;
                     if(andado){StartCoroutine(ReproducirSonidoAleatorio());}

        
        
        } else { animator.SetBool("isRunning", false); parado=true;}
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
        Orientacion(inputMovimiento);
    }
 IEnumerator ReproducirSonidoAleatorio()
    {
        if (!andado){}
        else
    
    
   if(!parado&&!saltando){
        andado=false;
    
            int numeroAleatorio = Random.Range(1, 4);
 string nombreEscenaActual = SceneManager.GetActiveScene().name;
 
        switch (numeroAleatorio)
        {
            case 1:
                pasosManager.ReproducirSonido(sonidopaso1);
                break;
            case 2:
                pasosManager.ReproducirSonido(sonidopaso2);
                break;
            case 3:
                pasosManager.ReproducirSonido(sonidopaso4);
                break;
            
        }
           yield return new WaitForSeconds(0.33f);
        andado=true;
       


}

        
    }
    void Saltar()
    {
        float inputMovimiento2 = Input.GetAxis("Vertical");
        Rigidbody2D rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.velocity = new Vector2(inputMovimiento2 * velocidad, rigidbody2.velocity.x);
    }

    void Pegar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && puedeAtacar)
        {
                        StartCoroutine(EsperarParaAtacar());

            // Comprueba si el joystick apunta hacia arriba
            if (vertical > umbral)
            {
                Debug.Log("Apuntando hacia arriba");
            }
            // Comprueba si el joystick apunta hacia abajo
            else if (vertical < -umbral)
            {
                Debug.Log("Apuntando hacia abajo");
            }
            // Comprueba si el joystick apunta hacia los lados
            else if (Mathf.Abs(horizontal) > umbral)
            {
                // Comprueba si también está apuntando hacia arriba o abajo mientras apunta hacia los lados
                if (vertical > umbral)
                {
                    Debug.Log("Apuntando hacia arriba mientras apunta hacia los lados");
                }
                else if (vertical < -umbral)
                {
                    Debug.Log("Apuntando hacia abajo mientras apunta hacia los lados");
                }
                else
                {
                    Debug.Log("Apuntando hacia los lados");
                    animator.SetBool("hit", true);
                }
            }
            // Comprueba si está mirando hacia arriba o abajo pero ha dejado de dirigir el joystick hacia esa dirección
            else if (Mathf.Abs(vertical) > umbralDeMirada)
            {
                Debug.Log("Mirando hacia arriba o abajo, pero no dirigiendo activamente hacia esa dirección");
                                    animator.SetBool("hit", true);

            }else{                    animator.SetBool("hit", true);
}
 bool estadoActualHit = animator.GetBool("hit");
 if(estadoActualHit){audioManager.ReproducirSonido(sonidopuñetazo);}
        }
        else
        {
            animator.SetBool("hit", false);
        }
    }

    void Orientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0&&!izquierda) || (mirandoDerecha == false && inputMovimiento > 0&&!izquierda))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if((mirandoDerecha == false && inputMovimiento < 0&&izquierda) || (mirandoDerecha == true && inputMovimiento > 0&&izquierda))
        {
                        mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
   public void RecibirDireccion(bool miraizquierda)
{

    izquierda = miraizquierda;
}
public void Knockback(Vector2 puntoGolpe)
{
   rigidbody.velocity=new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
}

IEnumerator MoverPersonajeGradualmente(Vector3 nuevaPosicion)
{
    // Calcula la distancia total que el personaje debe recorrer
    float distanciaTotal = Vector3.Distance(transform.position, nuevaPosicion);

    // Calcula la velocidad a la que el personaje se moverá para recorrer toda la distancia en 1 segundo
    float velocidadMovimiento = distanciaTotal / 0.25f;

    // Mientras la distancia entre la posición actual y la nueva posición sea mayor que un umbral pequeño
    while (Vector3.Distance(transform.position, nuevaPosicion) > 0.01f)
    {
        // Calcula el paso hacia la nueva posición usando Lerp
        float distanciaCubierta = velocidadMovimiento * Time.deltaTime;
        Vector3 siguientePosicion = Vector3.MoveTowards(transform.position, nuevaPosicion, distanciaCubierta);

        // Aplica el nuevo paso de movimiento
        transform.position = siguientePosicion;

        yield return null; // Espera al siguiente frame
    }

    // Asegura que la posición final sea exactamente la nueva posición
    transform.position = nuevaPosicion;
}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EstaenSuelo())
        {
            enProcesoDeSalto = false;
            saltando=false;
        }
    }
     IEnumerator EsperarParaAtacar()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }
    void MoverReflejo(){
            GameObject reflejo = GameObject.Find("Reflejo");
if (reflejo != null)
    {
        reflejo.transform.position = new Vector3(transform.position.x, -transform.position.y-0.61f, transform.position.z-2);
    }

    }
    
}
