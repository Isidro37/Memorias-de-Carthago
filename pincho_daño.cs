using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pincho_daño : MonoBehaviour
{
    private GameObject personaje;
    public GameObject pantalla;

    private Vector3 ultimaPosicionGuardada;
    private bool tocandoSuelo = false;
    gestionar_vida gestionarvida;

    void Start()
    {
        personaje = GameObject.Find("Personaje");
        StartCoroutine(GuardarPosicionPeriodicamente());
        gestionarvida=personaje.GetComponent<gestionar_vida>();
                ultimaPosicionGuardada = personaje.transform.position;

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Personaje")
        {
            gestionarvida.DetectarDaño(true);
            pantalla.SetActive(true);
            personaje.transform.position=ultimaPosicionGuardada;
            StartCoroutine(EsperandoPantalla());
        }
    }

    IEnumerator EsperandoPantalla(){
        yield return new WaitForSeconds(3f);
        pantalla.SetActive(false);

    }
    IEnumerator GuardarPosicionPeriodicamente()
    {
        while (true)
        {
            // Espera 5 segundos
            yield return new WaitForSeconds(5f);

            // Verifica si el personaje está tocando el suelo
            while (!personaje.GetComponent<character_controller>().EstaenSuelo())
            {
        yield return null;
            }
                            ultimaPosicionGuardada = personaje.transform.position;

        }
    }

}

