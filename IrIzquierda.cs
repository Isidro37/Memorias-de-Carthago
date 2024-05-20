using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class IrIzquierda : MonoBehaviour
{
      public GameObject pantallanegra;
    public AudioClip sonidoSalir;
    public Audio_Manager audioManager;
     GameObject personaje;
       character_controller characterController;

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
            StartCoroutine(CambiarEscenaI());}

    }
     private void OnTriggerExit2D(Collider2D collision) {

    }




     IEnumerator CambiarEscenaI()
    {
       pantallanegra.SetActive(true);
        

    yield return new WaitForSeconds(1.6f);  
          string nombreEscenaActual = SceneManager.GetActiveScene().name;
         
         if(nombreEscenaActual=="Plaza Vieja")
         {
            SceneManager.LoadScene("Antes de el puerto(antiguo)");
            yield return new WaitForSeconds(0.001f);               
            buscarPersonaje();
            characterController.RecibirDireccion(true);
            personaje.transform.position = new Vector3(6.78f, -3.080498f, 3f);
            personaje.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);
            }
            if(nombreEscenaActual=="Arcos viejos"){
                SceneManager.LoadScene("Plaza Vieja");
            yield return new WaitForSeconds(0.001f);               
            buscarPersonaje();
            characterController.RecibirDireccion(true);
            personaje.transform.position = new Vector3(6.78f, -3.080498f, 3f);
            personaje.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);
            }
        if(nombreEscenaActual=="Pl 1"){
                SceneManager.LoadScene("Arcos viejos");
            yield return new WaitForSeconds(0.001f);               
            buscarPersonaje();
            characterController.RecibirDireccion(true);
            personaje.transform.position = new Vector3(11.35624f, -3.07993f, 3f);
            personaje.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);
                CinemachineVirtualCamera cinemachineCamera;
                        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
                        cinemachineCamera.m_Lens.OrthographicSize=4;


            }
                    if(nombreEscenaActual=="Pl 2"){
                SceneManager.LoadScene("Pl 1");
            yield return new WaitForSeconds(0.001f);               
            buscarPersonaje();
            characterController.RecibirDireccion(true);
            personaje.transform.position = new Vector3(23.75084f, -3.07993f, 3f);
            personaje.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);



            }
                                if(nombreEscenaActual=="Pl 3"){
                SceneManager.LoadScene("Pl 2");
            yield return new WaitForSeconds(0.001f);               
            buscarPersonaje();
            characterController.RecibirDireccion(true);
            personaje.transform.position = new Vector3(12.5341f, 14.97304f, 3f);
            personaje.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);



            }
         if(nombreEscenaActual=="Antes de el puerto(antiguo)")
         {
         SceneManager.LoadScene("Puerto Antiguo");
         }
    }
    void buscarPersonaje(){
             personaje = GameObject.Find("Personaje");
 characterController = personaje.GetComponent<character_controller>();
    }
    
}
