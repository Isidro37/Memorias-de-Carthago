using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using System.Numerics;


public class MenuController : MonoBehaviour
{
    public GameObject MenuPrincipal;
    public GameObject MenuNuevaPartida;
    public GameObject MenuOpciones;
    public GameObject MenuSalir;
    public Button NuevaSi;
    public GameObject MenuPausa;
    public Button Nueva;
    public Scrollbar ScrollVolumen;
    public Scrollbar ScrollBrillo;
    public TextMeshProUGUI textoBrillo;
    public GameObject cargarPartida;
     ScriptGuardado scriptSaved;
 public TextMeshProUGUI textoVolumen;
 public GameObject Guardado;
public GameObject pantallaNegra;
public GameObject Jugador;
public Toggle CheckboxNarrador;
bool dentro;
    // Start is called before the first frame update
    void Start()
    {
        ScrollVolumen.onValueChanged.AddListener(OnScrollbarValueChanged);
        ScrollBrillo.onValueChanged.AddListener(OnScrollbarValueChanged2);
        Guardado=GameObject.Find("Guardado");
         if(SceneManager.GetActiveScene().name=="Portada"){
            scriptSaved = Guardado.GetComponent<ScriptGuardado>();
            if(scriptSaved.playerPos!=new UnityEngine.Vector3(0,0,0)){
                cargarPartida.SetActive(true);
            }
    }
    }
    public void datosCargados(){
        Debug.Log("recibido");
        Guardado=GameObject.Find("Guardado");
         scriptSaved = Guardado.GetComponent<ScriptGuardado>();
        textoVolumen.SetText(scriptSaved.volume.ToString());
        textoBrillo.SetText(scriptSaved.bright.ToString());
        ScrollBrillo.value=((float)(scriptSaved.bright*0.1));
        ScrollVolumen.value=((float)(scriptSaved.volume*0.1));
        CheckboxNarrador.isOn=scriptSaved.narrador;
        if(SceneManager.GetActiveScene().name=="Portada"){
            if(scriptSaved.playerPos!=new UnityEngine.Vector3(0,0,0)){
                cargarPartida.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale==0){
        Debug.Log("cero");}
string nombreEscenaActual = SceneManager.GetActiveScene().name;
if(nombreEscenaActual=="Portada"){ Time.timeScale = 1;}
         if(nombreEscenaActual!="Portada"){StartCoroutine(Pausado());} }
        
         IEnumerator Pausado(){
            
            if(!dentro){
                dentro=true;
            while(!Input.GetKeyDown(KeyCode.Escape)&&!Input.GetKeyDown(KeyCode.JoystickButton7)){
                yield return null;
            }
            
            Debug.Log(Time.timeScale);
            Debug.Log("Entra");
            if(MenuPausa.activeSelf){
                        MenuPrincipal.SetActive(true);
        MenuOpciones.SetActive(false);
                MenuPausa.SetActive(false);
                Time.timeScale = 1;}else{MenuPausa.SetActive(true);
            StartCoroutine(Marcar(Nueva));
           
            }
            dentro=false;
            
         }
         }
    public void NuevaPartida(){
        DeselectAllButtons();
        MenuPrincipal.SetActive(false);
        MenuNuevaPartida.SetActive(true);
        StartCoroutine(Marcar(NuevaSi));
              
        
    }
    IEnumerator Marcar(Button boton){

        yield return  new WaitForSeconds(0.00000001f);  

SelectButton(boton);  
 
string nombreEscenaActual = SceneManager.GetActiveScene().name;
if(nombreEscenaActual!="Portada"){
    yield return  new WaitForSeconds(0.001f);  
     Time.timeScale = 1;}
           


    }
        IEnumerator MarcarScroll(Scrollbar scroll){
        yield return  new WaitForSeconds(0.00000001f);  
SelectScroll(scroll);

string nombreEscenaActual = SceneManager.GetActiveScene().name;
if(nombreEscenaActual!="Portada"){ 
    yield return  new WaitForSeconds(0.001f);  
    Time.timeScale = 0;}
          }


    public void NuevaPartidaSi(){
        StartCoroutine(EmpezarJuego());
     }
    public void NuevaPartidaNo(){
        DeselectAllButtons();
        MenuPrincipal.SetActive(true);
        MenuNuevaPartida.SetActive(false);
        SelectButton(Nueva); 
       
    }
    public void CargarPartida(){
        StartCoroutine(CargarJuego());
    }
    public void Opciones(){
         Time.timeScale = 1;
                DeselectAllButtons();
        MenuPrincipal.SetActive(false);
        MenuOpciones.SetActive(true);
        StartCoroutine(MarcarScroll(ScrollVolumen));
        
    }
    public void VolverOpciones(){
        
        Time.timeScale = 1;
         DeselectAllButtons();
        MenuPrincipal.SetActive(true);
        MenuOpciones.SetActive(false);
        Debug.Log("PR");
        StartCoroutine(Marcar(Nueva));
        
    }
    public void Check(){
         Guardado=GameObject.Find("Guardado");
         scriptSaved = Guardado.GetComponent<ScriptGuardado>();
            Debug.Log(CheckboxNarrador.isOn);
         scriptSaved.narrador=CheckboxNarrador.isOn;
    }
    public void Salir(){
        
                Application.Quit();

    }
    public void GuardarJuego(){
        GameObject guardado=GameObject.Find("Guardado");
        ScriptGuardado GuardarPartida=guardado.GetComponent<ScriptGuardado>();
        Jugador=GameObject.Find("Personaje");
        UnityEngine.Vector3 vector=Jugador.transform.position;
        scriptSaved.playerPos=vector;
        scriptSaved.narrador=CheckboxNarrador.isOn;
        scriptSaved.scene=SceneManager.GetActiveScene().name;
        GuardarPartida.GuardarDatos();
    }
    public void SalirAlMenu(){
        GameObject guardado=GameObject.Find("Guardado");
        ScriptGuardado GuardarPartida=guardado.GetComponent<ScriptGuardado>();

        Jugador=GameObject.Find("Personaje");
        UnityEngine.Vector3 vector=Jugador.transform.position;
        scriptSaved.playerPos=vector;
        scriptSaved.scene=SceneManager.GetActiveScene().name;
        GuardarPartida.GuardarDatos();
        Time.timeScale = 1;
        SceneManager.LoadScene("Portada");
        
    }
    public void Reanudar(){
        MenuPausa.SetActive(false);
        Time.timeScale = 1;
    }
     IEnumerator CargarJuego()
    {
        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(3);
    SceneManager.LoadScene(scriptSaved.scene);

    
    
     }
    IEnumerator EmpezarJuego()
    {
        scriptSaved = Guardado.GetComponent<ScriptGuardado>();
        scriptSaved.playerPos=new UnityEngine.Vector3(0,0,0);

        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(3);
    SceneManager.LoadScene("bar canton");
    }
     private static void DeselectAllButtons()
    {
        ColorLetras[] botones = FindObjectsOfType<ColorLetras>(); // Obtener todos los componentes ColorLetras en la escena
        foreach (ColorLetras boton in botones)
        {
            EventSystem.current.SetSelectedGameObject(null); // Desseleccionar el botón actual
        }
    }
    public void SelectButton(Button buttonToSelect)
    {
        // Verificar si el botón a seleccionar no es nulo
        if (buttonToSelect != null)
        {
            // Obtener el componente Selectable del botón
            Selectable selectable = buttonToSelect.GetComponent<Selectable>();

            // Verificar si el componente Selectable no es nulo
            if (selectable != null)
            {
                // Seleccionar el botón
                selectable.Select();
            }
            else
            {
                Debug.LogWarning("El botón no tiene un componente Selectable adjunto.");
            }
        }
        else
        {
            Debug.LogWarning("El botón que deseas seleccionar es nulo.");
        }
    }
     public void SelectScroll(Scrollbar scroll)
    {
        // Verificar si el botón a seleccionar no es nulo
        if (scroll != null)
        {
            // Obtener el componente Selectable del botón
            Selectable selectable = scroll.GetComponent<Selectable>();

            // Verificar si el componente Selectable no es nulo
            if (selectable != null)
            {
                // Seleccionar el botón
                selectable.Select();
            }
            else
            {
                Debug.LogWarning("El botón no tiene un componente Selectable adjunto.");
            }
        }
        else
        {
            Debug.LogWarning("El botón que deseas seleccionar es nulo.");
        }
    }
    public void Dropdown(int index){
switch(index)
{
    case 0: //guardar idioma en español
    break;
    case 1: //inglés
    break;
    case 2: //francés
    break;
}
    }
    void OnScrollbarValueChanged(float value)
    {
        StartCoroutine(pausa());
        Guardado=GameObject.Find("Guardado");
         scriptSaved = Guardado.GetComponent<ScriptGuardado>();
        // Convertir el valor del Scrollbar a un valor entre 0 y 10
        int cantidad = Mathf.RoundToInt(value * 10);

        // Actualizar el TextMeshProUGUI con la cantidad
        textoVolumen.SetText(cantidad.ToString());
                scriptSaved.VolumeChange(cantidad);
              


    }
    void OnScrollbarValueChanged2(float value)
    {
        StartCoroutine(pausa());
        Guardado=GameObject.Find("Guardado");
         scriptSaved = Guardado.GetComponent<ScriptGuardado>();
        // Convertir el valor del Scrollbar a un valor entre 0 y 10
        
        int cantidad = Mathf.RoundToInt(value * 10);

        // Actualizar el TextMeshProUGUI con la cantidad
        textoBrillo.SetText(cantidad.ToString());
        scriptSaved.BrightChange(cantidad);
        

    }
IEnumerator pausa(){
    string nombreEscenaActual = SceneManager.GetActiveScene().name;
if(nombreEscenaActual!="Portada"){

        Time.timeScale = 1;
    yield return  new WaitForSeconds(0.001f);  
            Time.timeScale = 0;
}
}
}
