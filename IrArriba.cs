using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IrArriba : MonoBehaviour
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
            StartCoroutine(CambiarEscenaD());

    }
     private void OnTriggerExit2D(Collider2D collision) {

    }




     IEnumerator CambiarEscenaD()
    {
        pantallanegra.SetActive(true);

        yield return new WaitForSeconds(1.6f);  
          string nombreEscenaActual = SceneManager.GetActiveScene().name;
          if(nombreEscenaActual=="Pl 4")
         {
            SceneManager.LoadScene("Pl 2");
                    yield return new WaitForSeconds(0.001f);  
             personaje = GameObject.Find("Personaje");
            personaje.transform.position = new Vector3(6.146746f, 14.97224f, 3f);
            
            }
            
}
}
