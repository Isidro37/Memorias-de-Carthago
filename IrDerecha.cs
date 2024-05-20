using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IrDerecha : MonoBehaviour
{
        public GameObject pantallanegra;
    public AudioClip sonidoSalir;
    public Audio_Manager audioManager;
     GameObject personaje;
  void Start()
    {
            DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Personaje"){
            StartCoroutine(CambiarEscenaD());}

    }
     private void OnTriggerExit2D(Collider2D collision) {

    }




     IEnumerator CambiarEscenaD()
    {
        pantallanegra.SetActive(true);


        yield return new WaitForSeconds(1.6f);  
          string nombreEscenaActual = SceneManager.GetActiveScene().name;
         
         if(nombreEscenaActual=="Antes de el puerto(antiguo)")
         {
            SceneManager.LoadScene("Plaza Vieja");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(-45.8f, -3.080498f, 3f);
            
            }
        if(nombreEscenaActual=="Plaza Vieja")
         {
            SceneManager.LoadScene("Arcos viejos");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(-45.8f, -3.080498f, 3f);
            
            }
             if(nombreEscenaActual=="Arcos viejos")
         {
            SceneManager.LoadScene("Pl 1");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(-63.81797f, -3.079954f, 3f);
            
            }
        if(nombreEscenaActual=="Pl 1")
         {
            SceneManager.LoadScene("Pl 2");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(-63.81797f, -3.079954f, 3f);
            
            }
                    if(nombreEscenaActual=="Pl 2")
         {
            SceneManager.LoadScene("Pl 3");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(-63.81797f, -3.079954f, 3f);
            
            }
         if(nombreEscenaActual=="Puerto Antiguo")
         {
         SceneManager.LoadScene("Antes de el puerto(antiguo)");
         }
    }
    
}
