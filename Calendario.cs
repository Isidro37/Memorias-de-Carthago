using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Calendario : MonoBehaviour
{
    public GameObject dia0;
    public GameObject dia1;
    public GameObject dia2;
    public GameObject dia3;
    public GameObject dia4;
    public GameObject dia5;
    public GameObject dia6;

    public Vector3 Vdia0;
    public Vector3 Vdia1;
    public Vector3 Vdia2;
    public Vector3 Vdia3;
    public Vector3 Vdia4;
    public Vector3 Vdia5;
    public Vector3 Vdia6;

    bool plegado = true;
    Aemetcosas ScriptAemet;

    public List<Sprite> imagenesTiempo ;
    public GameObject nube1;
    public GameObject nube2;
    public GameObject nube3;
    public GameObject nubesNegras;
    public GameObject lluviaLeve;
    public GameObject lluviaFuerte;
    public GameObject nieveFloja;
    public GameObject nieveFuerte;
    public GameObject rayos;
    public GameObject niebla;
    public GameObject calima;
    public GameObject rayo1;
    public GameObject rayo2;
    public GameObject rayo3;
    public GameObject personaje;
    private Animator animator;
    public Audio_Manager AudioManager;
    
    public AudioClip rayoCayendo;
    public AudioClip lluvia;

    void Start()
    {
        
        // Almacenar las posiciones iniciales de los días
        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        if(nombreEscenaActual!="Pl 1"&&nombreEscenaActual!="Pl 2"&&nombreEscenaActual!="Pl 3"&&nombreEscenaActual!="Pl 4"&&nombreEscenaActual!="bar canton"){
        nube1.SetActive(false);
      nube2.SetActive(false);
      nube3.SetActive(false);
      nubesNegras.SetActive(false);
      lluviaFuerte.SetActive(false);
      lluviaLeve.SetActive(false);
      nieveFloja.SetActive(false);
      nieveFuerte.SetActive(false);
      rayos.SetActive(false);
      niebla.SetActive(false);
      calima.SetActive(false);
        }
      
        
        Vdia0=dia0.transform.position;
        Vdia1=dia1.transform.position;
        Vdia2=dia2.transform.position;
        Vdia3=dia3.transform.position;
        Vdia4=dia4.transform.position;
        Vdia5=dia5.transform.position;
        Vdia6=dia6.transform.position;
        dia1.transform.position = new Vector3(dia1.transform.position.x, dia0.transform.position.y, dia1.transform.position.z);
        dia2.transform.position = new Vector3(dia2.transform.position.x, dia0.transform.position.y, dia2.transform.position.z);
        dia3.transform.position = new Vector3(dia3.transform.position.x, dia0.transform.position.y, dia3.transform.position.z);
        dia4.transform.position = new Vector3(dia4.transform.position.x, dia0.transform.position.y, dia4.transform.position.z);
        dia5.transform.position = new Vector3(dia5.transform.position.x, dia0.transform.position.y, dia5.transform.position.z);
        dia6.transform.position = new Vector3(dia6.transform.position.x, dia0.transform.position.y, dia6.transform.position.z);
        StartCoroutine(Desplegar());


      

    }
    IEnumerator NecesitoAyuda(){
    yield return new WaitForSeconds (0.0001f);
    Debug.Log("Sonando");
                AudioManager.ReproducirSonidoLoop(lluvia);

    
} 
    void Update(){
    }
    IEnumerator AnimacionRayos(){
        
        rayos.SetActive(true);
        
        
        while(true){
            bool primerRayo=false;
        bool segundoRayo=false;
        bool tercerRayo=false;
            Debug.Log("Dentro");
            int randomNumber = UnityEngine.Random.Range(1, 8);
             if (randomNumber == 1){
                Vector3 vector3=personaje.transform.position;
                  rayo1.transform.position = new Vector3(UnityEngine.Random.Range(vector3.x - 30f, vector3.x + 30f), rayo1.transform.position.y, rayo1.transform.position.z);
            rayo2.transform.position = new Vector3(UnityEngine.Random.Range(vector3.x - 30f, vector3.x + 30f), rayo2.transform.position.y, rayo2.transform.position.z);
            rayo3.transform.position = new Vector3(UnityEngine.Random.Range(vector3.x - 30f, vector3.x + 30f), rayo3.transform.position.y, rayo3.transform.position.z);

                 randomNumber = UnityEngine.Random.Range(1, 5);
                    if(randomNumber==1){
                        randomNumber = UnityEngine.Random.Range(1, 3);
                        if(randomNumber==1){primerRayo=true;segundoRayo=true;}
                        if(randomNumber==2){primerRayo=true;tercerRayo=true;}
                        if(randomNumber==3){tercerRayo=true;segundoRayo=true;}
                    }else
                    if(randomNumber==2){
                        primerRayo=true;segundoRayo=true;tercerRayo=true;
                    }else{
                        randomNumber = UnityEngine.Random.Range(1, 3);
                        if(randomNumber==1){primerRayo=true;}
                        if(randomNumber==2){tercerRayo=true;}
                        if(randomNumber==3){segundoRayo=true;}
                    }
                if(primerRayo==true){
                    animator = rayo1.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", true);
                    AudioManager.ReproducirSonido(rayoCayendo);
                }
                if(segundoRayo==true){
                    animator = rayo2.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", true);
                    AudioManager.ReproducirSonido(rayoCayendo);
                }
                if(tercerRayo==true){
                    animator = rayo3.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", true);
                    AudioManager.ReproducirSonido(rayoCayendo);
                }
                 yield return new WaitForSeconds(0.3f); 
                animator = rayo1.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", false);
                    animator = rayo2.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", false);
                    animator = rayo3.GetComponent<Animator>();
                    animator.SetBool("rayoActivo", false);
                  
             }
            yield return new WaitForSeconds(0.5f); 
              

        }
    }

    public void listo(){
                GameObject aemet=GameObject.Find("Aemet");
         ScriptAemet=aemet.GetComponent<Aemetcosas>();
      

        AplicarDatos(dia0,ScriptAemet.dias[0],true);
        AplicarDatos(dia1,ScriptAemet.dias[1],false);
        AplicarDatos(dia2,ScriptAemet.dias[2],false);
        AplicarDatos(dia3,ScriptAemet.dias[3],false);
        AplicarDatos(dia4,ScriptAemet.dias[4],false);
        AplicarDatos(dia5,ScriptAemet.dias[5],false);
        AplicarDatos(dia6,ScriptAemet.dias[6],false);
    }

    void AplicarDatos(GameObject dia, Aemetcosas.Dia datos, bool primerDia){
        
         GameObject imagen=dia.transform.GetChild(0).gameObject;
        Image ImagenCielo =imagen.transform.GetChild(0).GetComponent<Image>();
        TMP_Text NombreDIa=imagen.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text TempMin=imagen.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text TempMax=imagen.transform.GetChild(3).GetComponent<TMP_Text>();
         DateTime fecha = DateTime.Parse(datos.fecha);
        
        // Obtener el nombre del día de la semana y el número del día
        string nombreDiaSemana = fecha.ToString("dddd");
        int numeroDia = fecha.Day;

        // Convertir el primer carácter a mayúscula
        nombreDiaSemana = char.ToUpper(nombreDiaSemana[0]) + nombreDiaSemana.Substring(1);

        // Formatear el texto con el nombre del día de la semana y el número del día
        string textoDia = nombreDiaSemana + " " + numeroDia;
        NombreDIa.text = textoDia;
        TempMin.text=datos.temperatura.minima.ToString()+"ºC";
        TempMax.text=datos.temperatura.maxima.ToString()+"ºC";
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        string estado=datos.estadoCielo[0].descripcion;
        if(estado==""){
             estado=datos.estadoCielo[1].descripcion;
             if(estado==""){
             estado=datos.estadoCielo[2].descripcion;
             if(estado==""){
             estado=datos.estadoCielo[3].descripcion;
             if(estado==""){
             estado=datos.estadoCielo[4].descripcion;
             if(estado==""){
             estado=datos.estadoCielo[5].descripcion;
        }
        }
        }
        }
        }
      
        
        try{
        if (estado == "Despejado" || estado == "Despejado noche")
{
          ImagenCielo.sprite=imagenesTiempo[0];
}
else if (estado == "Poco nuboso" || estado == "Poco nuboso noche")
{
    
    ImagenCielo.sprite=imagenesTiempo[1];
    if(primerDia){
        nube1.SetActive(true);
        
    }
}
else if (estado == "Intervalos nubosos" || estado == "Intervalos nubosos noche")
{
    ImagenCielo.sprite=imagenesTiempo[2];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
    }
}
else if (estado == "Nuboso" || estado == "Nuboso noche")
{
   ImagenCielo.sprite=imagenesTiempo[3];
   Debug.Log("Aqui si");
       if(primerDia){
        Debug.Log("Na de nas");
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
    }
}
else if (estado == "Muy nuboso")
{
    ImagenCielo.sprite=imagenesTiempo[4];
        if(primerDia){
        nubesNegras.SetActive(true);
    }
}
else if (estado == "Cubierto")
{
    ImagenCielo.sprite=imagenesTiempo[4];
        if(primerDia){
        nubesNegras.SetActive(true);
    }
}
else if (estado == "Nubes altas" || estado == "Nubes altas noche")
{
    ImagenCielo.sprite=imagenesTiempo[5];
        if(primerDia){
        nube3.SetActive(true);
    }
}
else if (estado == "Intervalos nubosos con lluvia escasa" || estado == "Intervalos nubosos con lluvia escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[6];
        if(primerDia){
        nube1.SetActive(true);
        lluviaLeve.SetActive(true);
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Nuboso con lluvia escasa" || estado == "Nuboso con lluvia escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[6];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        lluviaLeve.SetActive(true);
        StartCoroutine(NecesitoAyuda());
        
    }
}
else if (estado == "Muy nuboso con lluvia escasa")
{
    ImagenCielo.sprite=imagenesTiempo[7];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        lluviaLeve.SetActive(true);
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Cubierto con lluvia escasa")
{
    ImagenCielo.sprite=imagenesTiempo[8];
        if(primerDia){
        nubesNegras.SetActive(true);
        lluviaLeve.SetActive(true);
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Intervalos nubosos con lluvia" || estado == "Intervalos nubosos con lluvia noche")
{
    ImagenCielo.sprite=imagenesTiempo[9];
        if(primerDia){
        nube1.SetActive(true);
        lluviaFuerte.SetActive(true);
      StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Nuboso con lluvia" || estado == "Nuboso con lluvia noche")
{
    ImagenCielo.sprite=imagenesTiempo[10];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        lluviaFuerte.SetActive(true);
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Muy nuboso con lluvia")
{
    ImagenCielo.sprite=imagenesTiempo[10];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        lluviaFuerte.SetActive(true);
       StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Cubierto con lluvia")
{
    ImagenCielo.sprite=imagenesTiempo[11];
        if(primerDia){
        nubesNegras.SetActive(true);
        lluviaFuerte.SetActive(true);
      StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Intervalos nubosos con nieve escasa" || estado == "Intervalos nubosos con nieve escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[12];
        if(primerDia){
        nube1.SetActive(true);
        nieveFloja.SetActive(true);
    }
}
else if (estado == "Nuboso con nieve escasa" || estado == "Nuboso con nieve escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[13];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nieveFloja.SetActive(true);
    }
}
else if (estado == "Muy nuboso con nieve escasa")
{
    ImagenCielo.sprite=imagenesTiempo[14];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        nieveFloja.SetActive(true);
    }
}
else if (estado == "Cubierto con nieve escasa")
{
    ImagenCielo.sprite=imagenesTiempo[15];
        if(primerDia){
        nubesNegras.SetActive(true);
        nieveFloja.SetActive(true);
    }
}
else if (estado == "Intervalos nubosos con nieve" || estado == "Intervalos nubosos con nieve noche")
{
    ImagenCielo.sprite=imagenesTiempo[16];
        if(primerDia){
        nube1.SetActive(true);
        nieveFuerte.SetActive(true);
    }
}
else if (estado == "Nuboso con nieve" || estado == "Nuboso con nieve noche")
{
    ImagenCielo.sprite=imagenesTiempo[17];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nieveFuerte.SetActive(true);
    }
}
else if (estado == "Muy nuboso con nieve")
{
    ImagenCielo.sprite=imagenesTiempo[18];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        nieveFuerte.SetActive(true);
    }
}
else if (estado == "Cubierto con nieve")
{
    ImagenCielo.sprite=imagenesTiempo[19];
        if(primerDia){
        nubesNegras.SetActive(true);
        nieveFuerte.SetActive(true);
    }
}
else if (estado == "Intervalos nubosos con tormenta" || estado == "Intervalos nubosos con tormenta noche")
{
    ImagenCielo.sprite=imagenesTiempo[21];
        if(primerDia){
        nube1.SetActive(true);
        rayos.SetActive(true);
        lluviaFuerte.SetActive(true);
        StartCoroutine(AnimacionRayos());
       StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Nuboso con tormenta" || estado == "Nuboso con tormenta noche")
{
    ImagenCielo.sprite=imagenesTiempo[22];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        rayos.SetActive(true);
        lluviaFuerte.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Muy nuboso con tormenta")
{
    ImagenCielo.sprite=imagenesTiempo[23];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        rayos.SetActive(true);
        lluviaFuerte.SetActive(true);
        StartCoroutine(AnimacionRayos());
      StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Cubierto con tormenta")
{
    ImagenCielo.sprite=imagenesTiempo[25];
        if(primerDia){
        nubesNegras.SetActive(true);
        rayos.SetActive(true);
        lluviaFuerte.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Intervalos nubosos con tormenta y lluvia escasa" || estado == "Intervalos nubosos con tormenta y lluvia escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[21];
        if(primerDia){
        nube1.SetActive(true);
        lluviaLeve.SetActive(true);
        rayos.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Nuboso con tormenta y lluvia escasa" || estado == "Nuboso con tormenta y lluvia escasa noche")
{
    ImagenCielo.sprite=imagenesTiempo[22];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        lluviaLeve.SetActive(true);
        rayos.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Muy nuboso con tormenta y lluvia escasa")
{
    ImagenCielo.sprite=imagenesTiempo[23];
        if(primerDia){
        nube1.SetActive(true);
        nube2.SetActive(true);
        nube3.SetActive(true);
        lluviaLeve.SetActive(true);
        rayos.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Cubierto con tormenta y lluvia escasa")
{
    ImagenCielo.sprite=imagenesTiempo[25];
        if(primerDia){
        nubesNegras.SetActive(true);
        lluviaLeve.SetActive(true);
        rayos.SetActive(true);
        StartCoroutine(AnimacionRayos());
        StartCoroutine(NecesitoAyuda());
    }
}
else if (estado == "Niebla")
{
    ImagenCielo.sprite=imagenesTiempo[26];
        if(primerDia){
            niebla.SetActive(true);
        
    }
}
else if (estado == "Bruma")
{
    ImagenCielo.sprite=imagenesTiempo[26];
        if(primerDia){
        niebla.SetActive(true);
    }
}
else if (estado == "Calima")
{
    ImagenCielo.sprite=imagenesTiempo[27];
        if(primerDia){
        calima.SetActive(true);
    }
}
         }catch{}}


    
 void OnDestroy()
    {
        AudioManager.DetenerReproduccion();
    }
    IEnumerator Desplegar()
    {
        while (true)
        {
            if (plegado && Input.GetKeyDown(KeyCode.T)||plegado && Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                StartCoroutine(MoveToPosition(dia1, Vdia1,0.1f));
                StartCoroutine(MoveToPosition(dia2, Vdia2,0.2f));
                StartCoroutine(MoveToPosition(dia3, Vdia3,0.3f));
                StartCoroutine(MoveToPosition(dia4, Vdia4,0.4f));
                StartCoroutine(MoveToPosition(dia5, Vdia5,0.5f));
                StartCoroutine(MoveToPosition(dia6, Vdia6,0.6f));
                yield return new WaitForSeconds(0.1f);  
                plegado = false;
                
            }
                      else if (!plegado && Input.GetKeyDown(KeyCode.T)||!plegado && Input.GetKeyDown(KeyCode.JoystickButton6))
            {   StartCoroutine(MoveToPosition(dia6, new Vector3(dia6.transform.position.x, dia0.transform.position.y, dia6.transform.position.z),0.6f));
            yield return new WaitForSeconds(0.1f);  
                StartCoroutine(MoveToPosition(dia5, new Vector3(dia5.transform.position.x, dia0.transform.position.y, dia5.transform.position.z),0.5f));
                yield return new WaitForSeconds(0.1f);  
                StartCoroutine(MoveToPosition(dia4, new Vector3(dia4.transform.position.x, dia0.transform.position.y, dia4.transform.position.z),0.4f));
                yield return new WaitForSeconds(0.1f);  
                StartCoroutine(MoveToPosition(dia3, new Vector3(dia3.transform.position.x, dia0.transform.position.y, dia3.transform.position.z),0.3f));
                yield return new WaitForSeconds(0.1f);  
                StartCoroutine(MoveToPosition(dia2, new Vector3(dia2.transform.position.x, dia0.transform.position.y, dia2.transform.position.z),0.2f));
                yield return new WaitForSeconds(0.1f);  
                StartCoroutine(MoveToPosition(dia1, new Vector3(dia1.transform.position.x, dia0.transform.position.y, dia1.transform.position.z),0.1f));
                yield return new WaitForSeconds(0.1f);  
                plegado = true;
                
                
                
            }
            yield return null;
        }
    }

    IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition, float tiempo)
    {
        float elapsedTime = 0;

        Vector3 startingPos = obj.transform.position;

        while (elapsedTime < tiempo)
        {
            obj.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / tiempo));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
       
        
    }
}
