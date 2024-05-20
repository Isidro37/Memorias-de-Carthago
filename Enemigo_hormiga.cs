using System.Collections;
using UnityEngine;

public class Enemigo_hormiga : MonoBehaviour
{
    public float velocidadMovimiento = 3f; // Velocidad de movimiento del enemigo
    public float distanciaMinimaAtaque; // Distancia mínima para atacar

    private GameObject personaje; // Referencia al GameObject del personaje
    private Animator animator; // Referencia al Animator del enemigo
    private Rigidbody2D rb; // Referencia al Rigidbody del enemigo
    private bool atacando = false; // Bandera para indicar si el enemigo está atacando
    gestionar_vida gestionarvida;
    bool dentroAtaque=false;
        public float knockbackForce = 5f; // Fuerza del knockback
    public float knockbackDuration = 0.2f; // Duración del knockback en segundos

    void Start()
    {
        personaje = GameObject.Find("Personaje");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gestionarvida = personaje.GetComponent<gestionar_vida>();

        // Iniciar la rutina de movimiento hacia el personaje
        
    }

    IEnumerator Ataque()
    {
        while (true)
        {
            if (!atacando)
            {
                atacando = true;
                while (true)
                {
                    // Calcula la dirección hacia el personaje
                    Vector3 direccion = (personaje.transform.position - transform.position).normalized;

                    // Aplica la velocidad de movimiento hacia el personaje
                    float direccionMovimientoX = direccion.x;
                    float direccionMovimientoY = direccion.y;

                    // Verifica si el enemigo y el personaje están a la misma altura
                    if (Mathf.Abs(personaje.transform.position.y - transform.position.y) < 0.1f)
                    {
                        // Ajusta la dirección en el eje Y para que el enemigo se eleve un poco
                        direccionMovimientoY = 0.5f; // Ajusta este valor según lo que necesites
                    }

                    // Si la distancia en el eje X es menor que un umbral pequeño (por ejemplo, 0.1f), el enemigo mantendrá su dirección actual
                    if (Mathf.Abs(personaje.transform.position.x - transform.position.x) < 0.1f)
                    {
                        // No se invierte la dirección
                    }
                    else
                    {
                        // Si la distancia no es pequeña, se invierte la dirección según la posición del personaje
                        if (direccionMovimientoX > 0 && transform.localScale.x > 0)
                        {
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                        else if (direccionMovimientoX < 0 && transform.localScale.x < 0)
                        {
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                    }

                    // Aplica la velocidad de movimiento
                    rb.velocity = new Vector2(direccionMovimientoX * velocidadMovimiento, direccionMovimientoY * velocidadMovimiento);

                    yield return null;
                }
            }
        }
    }
void Update()
{
      float distancia = Vector3.Distance(transform.position, personaje.transform.position);
      if(distanciaMinimaAtaque>distancia && !dentroAtaque){
        StartCoroutine(Ataque());
        dentroAtaque=true;
      }
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Personaje")
        {
           
            gestionarvida.DetectarDaño(true);
            collision.gameObject.GetComponent<gestionar_vida>().HacerDaño(collision.GetContact(0).normal);
        }
    }

}
