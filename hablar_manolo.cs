using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleTextToSpeech.Scripts.Example;


public class hablar_manolo : MonoBehaviour
{
    private TextMeshProUGUI textoBoton;
        public GameObject texto_hablar;
        public GameObject cuadro_hablando;
        public GameObject cuadro_compra;
        public Image cuadroLicor;
        public GameObject pantallanegra;

        public Image cuadroAgua;

    public TMP_Text texto_manolo;
    public TMP_Text texto_interaccion;
    bool texto_acabado=true;
      bool saltarTexto=false;
      int conversacion=0;
      private character_controller movimientoJugador;
      public GameObject personaje;

    // Start is called before the first frame update
    void Start()
    {
        
movimientoJugador=personaje.GetComponent<character_controller>();
    }
private void OnTriggerEnter2D(Collider2D collision) {
texto_hablar.SetActive(true);  

    }
     private void OnTriggerExit2D(Collider2D collision) {
    texto_hablar.SetActive(false);  

    }
    // Update is called once per frame
    void Update()
    {
                hablar();
 if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton0)){
    saltarTexto=true;
 }

    }
    private void hablar(){
        if(texto_hablar.activeSelf){

            if(Input.GetKeyDown(KeyCode.E)||Input.GetAxis("Vertical") > 0.9){

            StartCoroutine(Hablando());


}
        }
    }

     IEnumerator Hablando()
    {
        if(conversacion==0){    cuadro_hablando.SetActive(true);
}
    texto_hablar.SetActive(false);  
 string nombreEscenaActual = SceneManager.GetActiveScene().name;
 movimientoJugador.sePuedeMover=false;
 if(nombreEscenaActual=="bar manolo"&&conversacion==0){
        StartCoroutine(EscribirTextoProgresivo("Bienvenido a Bar Manolo, soy Manolo. Soy el único camarero aquí, si quieres algo pídemelo a mí"));
 while (!texto_acabado)
    {
        yield return null;
    }

            StartCoroutine(EscribirTextoProgresivo("Ultimamente el bar está muy tranquilo asi que te serviré lo rapido que la ciática me lo permita"));
 while (!texto_acabado)
    {
        yield return null;
    }
    cuadro_hablando.SetActive(false);
    texto_hablar.SetActive(true);  


            texto_interaccion.text="Pulsa E para comprar";  
conversacion=1;

    }
   else if(nombreEscenaActual=="bar manolo"&&conversacion==1){
    cuadro_compra.SetActive(true);
    bool elegir=false;
    Color color_orig =cuadroLicor.color;
    cuadroLicor.color=new Color(0.2588f, 0.4353f, 0.4314f, 0.9451f);
    while(!elegir){
                yield return null;

        if(Input.GetKeyDown(KeyCode.S)){
            cuadroLicor.color=color_orig;
            cuadroAgua.color=new Color(0.2588f, 0.4353f, 0.4314f, 0.9451f);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            cuadroLicor.color=new Color(0.2588f, 0.4353f, 0.4314f, 0.9451f);
            cuadroAgua.color=color_orig;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if(cuadroLicor.color==color_orig){
                //Poner lo del agua
            }
            if(cuadroAgua.color==color_orig){
                pantallanegra.SetActive(true);
                            cuadroLicor.color=color_orig;
            cuadroAgua.color=color_orig;
            cuadro_compra.SetActive(false);
            elegir=true;
                        yield return new WaitForSeconds(1.6f);  

                SceneManager.LoadScene("bar canton");
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            cuadroLicor.color=color_orig;
            cuadroAgua.color=color_orig;
            cuadro_compra.SetActive(false);
            elegir=true;
        }
    }

    }else


    if(nombreEscenaActual=="bar canton"){
        StartCoroutine(EscribirTextoProgresivo("Ya era hora eh, llevas un buen rato ahí tumbado, vaya una te has pillado. No te preocupes, te entiendo, con esto de la guerra es complicado no desanimarse"));
 while (!texto_acabado)
    {
        yield return null;
    }
 StartCoroutine(EscribirTextoProgresivo("Que guerra? Ja, que gracioso, pero ni con un buen vaso de mi mejor licor podrías olvidar tanta miseria"));
 while (!texto_acabado)
    {
        yield return null;
    }
 StartCoroutine(EscribirTextoProgresivo("Bueno, sal a que te dé un rato el aire que te hará falta. Y no dudes en venir si te apetece tomar un buen trago"));
 while (!texto_acabado)
    {
        yield return null;
    }   
 cuadro_hablando.SetActive(false);
  movimientoJugador.sePuedeMover=true;


 while (true)
    {
            texto_hablar.SetActive(false);  

        yield return null;
    }

    }



    }
     IEnumerator EscribirTextoProgresivo(string mensaje)
    {
         GameObject Guardado=GameObject.Find("Guardado");
         ScriptGuardado scriptSaved = Guardado.GetComponent<ScriptGuardado>();
        if(scriptSaved.narrador){
        GameObject Hablar= GameObject.Find("EXAMPLE");
        TextToSpeechExample codigoTexto=Hablar.GetComponent<TextToSpeechExample>();
        
        codigoTexto.llamar(mensaje);
         }
        texto_acabado=false;
        texto_manolo.text = "";
         saltarTexto=false;
        foreach (char letra in mensaje)
        {
           
           if(!saltarTexto){
            texto_manolo.text += letra;
            yield return new WaitForSeconds(0.015f); // Puedes ajustar el tiempo de espera entre letras
           }
            
        }
        texto_manolo.text=mensaje;
        saltarTexto=false;
         while (!Input.GetKeyDown(KeyCode.Space)&&!Input.GetKeyDown(KeyCode.JoystickButton0))
    {
        yield return null;
    }
    
    texto_acabado=true;
     yield return new WaitForSeconds(0.001f);
   
    }
}
