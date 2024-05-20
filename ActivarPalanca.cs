using System.Collections;
using UnityEngine;

public class ActivarPalanca : MonoBehaviour
{
    bool dentro = false;
    private Animator animator;
    private GameObject puerta;
    public float velocidadMovimiento = 1.0f; // Velocidad de movimiento de la puerta
    public Audio_Manager EfectsManager;
    bool activado = false;
public AudioClip pincho_subiendo;
 private bool palancaActivadaGuardada = false;

    private const string PalancaActivada1Key = "PalancaActivada";
        private const string Restablecer = "Restablecer";

    void Start()
    {
        if(PlayerPrefs.GetInt(Restablecer) == 1){
             PlayerPrefs.SetInt(PalancaActivada1Key, 0);
             PlayerPrefs.SetInt(Restablecer, 0);
                PlayerPrefs.Save();
                }
        animator = GetComponent<Animator>();
        puerta = GameObject.Find("Mecanismo Puerta");
                if (PlayerPrefs.HasKey(PalancaActivada1Key))
        {
            palancaActivadaGuardada = PlayerPrefs.GetInt(PalancaActivada1Key) == 1;
            if (palancaActivadaGuardada)
            {
                ActivarPalancaYAbrirPuerta();
                activado=true;
            }
        }
    

    }

    IEnumerator EsperarActivo()
    {
        
        while (dentro)
        {
            if (Input.GetKeyDown(KeyCode.E) && !activado||Input.GetAxis("Vertical") > 0.9 && !activado)
            {
                animator.SetBool("PalancaActivada", true);
                activado = true;
                Debug.Log("activada");

                // Mover la puerta hacia arriba
                StartCoroutine(MoverPuerta());
                EfectsManager.ReproducirSonido(pincho_subiendo);
                 PlayerPrefs.SetInt(PalancaActivada1Key, 1);
                PlayerPrefs.Save();
            }

            yield return null;
        }
    }

    IEnumerator MoverPuerta()
    {
        Vector3 posicionInicial = puerta.transform.position;
        Vector3 posicionFinal = posicionInicial + Vector3.up * 3.7f; // Cambia el 2.0f por la altura deseada
        float distanciaTotal = Vector3.Distance(posicionInicial, posicionFinal);
        float distanciaRecorrida = 0.0f;

        while (distanciaRecorrida < distanciaTotal)
        {
            float distanciaFrame = velocidadMovimiento * Time.deltaTime;
            puerta.transform.position = Vector3.MoveTowards(puerta.transform.position, posicionFinal, distanciaFrame);
            distanciaRecorrida += distanciaFrame;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dentro = true;
        Debug.Log("Dentro");
        StartCoroutine(EsperarActivo());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dentro = false;
        Debug.Log("Fuera");
    }
     private void ActivarPalancaYAbrirPuerta()
    {
        animator.SetBool("PalancaActivada", true);
        velocidadMovimiento=10000;
        StartCoroutine(MoverPuerta());
    }
}
