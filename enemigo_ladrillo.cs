using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_ladrillo : MonoBehaviour
{
    bool dentro=false;
    public float rango = 0.4f; // Rango de detección
     float velocidadMovimiento = 3f; // Velocidad de movimiento del ladrillo hacia el personaje

    private GameObject personaje; // Referencia al GameObject del personaje
    private Animator animator; // Referencia al Animator del enemigo
    private Rigidbody2D rb; // Referencia al Rigidbody del ladrillo
    bool dañar=false;
    bool personajeEnCollider=false;
    
     float fuerzaKnockback = 5f; 
     float fuerzaSaltoKnockback = 2f; 

gestionar_vida gestionarvida;
character_controller character_controller;
    void Start()
    {
        // Buscar el GameObject del personaje
        personaje = GameObject.Find("Personaje");

        // Obtener el componente Animator del enemigo
        animator = GetComponent<Animator>();

        // Obtener el componente Rigidbody2D del ladrillo
        rb = GetComponent<Rigidbody2D>(); // Usamos GetComponent directamente en el script del ladrillo
        gestionarvida=personaje.GetComponent<gestionar_vida>();
                character_controller=personaje.GetComponent<character_controller>();

    }
 private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Personaje")
        {
            personajeEnCollider = true;

            if (dañar)
            {
                gestionarvida.DetectarDaño(true);
                collision.gameObject.GetComponent<gestionar_vida>().HacerDaño(collision.GetContact(0).normal);
                        

            }
            else{StartCoroutine(Esperar_daño());}
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Personaje")
        {
            personajeEnCollider = false;
        }
    }
    IEnumerator Esperar_daño()
    {
        while (personajeEnCollider&&!dañar)
    {
        yield return null;
    }
    if(dañar&&personajeEnCollider){gestionarvida.DetectarDaño(true);}
    
    }void Update()
{
    
                if ((transform.position.x- personaje.transform.position.x)<1f&&(transform.position.x- personaje.transform.position.x)>-1f)
                {
                    Debug.Log("Ha entrado");
                    if(dentro){}else{
                    StartCoroutine(MismaPosicion());}
                }else{
    // Verificar si el personaje está cerca en el eje x y a la misma altura
    if (personaje != null)
    {
        float distanciaX = Mathf.Abs(transform.position.x - personaje.transform.position.x);
        float distanciaY = Mathf.Abs(transform.position.y - personaje.transform.position.y);

        if (distanciaX <= rango - 8f && distanciaY < 1f) // 1f es la tolerancia de altura
        {
            // Activar la animación
            animator.SetBool("ladrillo_tocado", true);
            animator.SetBool("ladrillo_lejos", false);
        }
        else
        {
            // Desactivar la animación si el personaje está fuera del rango
            animator.SetBool("ladrillo_tocado", false);
            // Detener el movimiento si el personaje está fuera del rango
            rb.velocity = Vector2.zero;
        }
    }
    
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    if (stateInfo.IsName("enemigo animacion"))
    {
        dañar = true;
        //Meter movimiento
        float direccionMovimiento = Mathf.Sign(personaje.transform.position.x - transform.position.x);

        // Si la distancia en el eje X es menor que un umbral pequeño (por ejemplo, 0.1f), el enemigo mantendrá su dirección actual
        if (Mathf.Abs(personaje.transform.position.x - transform.position.x) < 0.1f)
        {
            // No se invierte la dirección
        }
        else
        {
            // Si la distancia no es pequeña, se invierte la dirección según la posición del personaje
            if (direccionMovimiento < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            if (direccionMovimiento > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        // Aplicar la velocidad de movimiento gradualmente hacia el personaje

        rb.velocity = new Vector2(direccionMovimiento * velocidadMovimiento, rb.velocity.y);

        float distancia = (personaje.transform.position.x - transform.position.x);

        if (distancia > 15 || distancia < -15)
        {
            animator.SetBool("ladrillo_lejos", true);
            dañar = false;
        }
    }
}
}
IEnumerator MismaPosicion(){
    dentro=true;
    Vector3 posicionEnemigo;
    posicionEnemigo = transform.position;
 Debug.Log("Dentro");
    while((transform.position.x- personaje.transform.position.x)<1f&&(transform.position.x- personaje.transform.position.x)>-1f)
              {
        Debug.Log("AAAA");
        transform.position=posicionEnemigo;
                yield return null;
    }
    dentro=false;
    Debug.Log("Fuera");

}
}
