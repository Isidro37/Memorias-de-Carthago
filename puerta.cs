using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puerta : MonoBehaviour
{
    public GameObject textopuerta;
    public GameObject pantallanegra;
    public AudioClip sonidoPuerta;
    public Audio_Manager audioManager;
    bool dentro=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(keyCode))
            {
                Debug.Log("Tecla pulsada: " + keyCode);
                // Aquí puedes agregar cualquier lógica adicional que necesites para cada tecla
                salir();   
            }
        }
     salir();   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
textopuerta.SetActive(true);  

    }
     private void OnTriggerExit2D(Collider2D collision) {
    textopuerta.SetActive(false);  

    }
    private void salir(){
        
        if(textopuerta.activeSelf){if(Input.GetKeyDown(KeyCode.E)||Input.GetAxis("Vertical") > 0.9){
            if(!dentro){
            StartCoroutine(CambiarEscenaConEspera());
            dentro=true;
            }

}}
    }
     IEnumerator CambiarEscenaConEspera()
    {
        pantallanegra.SetActive(true);
        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        if(nombreEscenaActual!="Puerto Antiguo"){
        audioManager.ReproducirSonido(sonidoPuerta);
        }
        yield return new WaitForSeconds(1.6f);  
         
         if(nombreEscenaActual=="Pantalla 1")SceneManager.LoadScene("bar manolo");
         if(nombreEscenaActual=="Plaza Vieja")SceneManager.LoadScene("bar canton");
         if(nombreEscenaActual=="Puerto Antiguo")SceneManager.LoadScene("Antes de el puerto(antiguo)");
    }
}
