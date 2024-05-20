using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class salir_bar_manolo : MonoBehaviour
{
    public GameObject pantallanegra;
    public AudioClip sonidoPuerta;
    public Audio_Manager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
            StartCoroutine(CambiarEscenaConEspera());

    }
     private void OnTriggerExit2D(Collider2D collision) {

    }




     IEnumerator CambiarEscenaConEspera()
    {
        pantallanegra.SetActive(true);
        audioManager.ReproducirSonido(sonidoPuerta);

        yield return new WaitForSeconds(1.6f);  
          string nombreEscenaActual = SceneManager.GetActiveScene().name;
         if(nombreEscenaActual=="bar manolo")SceneManager.LoadScene("Pantalla 1");
         if(nombreEscenaActual=="bar canton")SceneManager.LoadScene("Plaza Vieja");
         if(nombreEscenaActual=="Plaza Vieja")SceneManager.LoadScene("Antes de el puerto(antiguo)");
         if(nombreEscenaActual=="Antes de el puerto(antiguo)")SceneManager.LoadScene("Puerto Antiguo");

    }
    
}
