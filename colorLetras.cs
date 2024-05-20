using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using GoogleTextToSpeech.Scripts.Example;

public class ColorLetras : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public Color hoverColor; // Color al pasar el cursor por encima
    public Color selectColor; // Color al seleccionar con el teclado
    private Color originalColor; // Color original del texto
    private TextMeshProUGUI textoBoton;

    void Start()
    {
        
        // Obtener el componente de texto del botón
        textoBoton = GetComponentInChildren<TextMeshProUGUI>();

        // Almacenar el color original del texto
        originalColor = textoBoton.color;
    }

    // Método para cambiar el color del texto al pasar el cursor por encima o seleccionarlo con el teclado
    public void OnPointerEnter(PointerEventData eventData)
    {
        textoBoton.color = hoverColor; // Cambiar el color del texto al pasar el cursor por encima
    }

    // Método para restaurar el color original del texto al salir del cursor o al deseleccionarlo con el teclado
    public void OnPointerExit(PointerEventData eventData)
    {
        textoBoton.color = originalColor; // Restaurar el color original del texto al salir del cursor
    }

    // Método para cambiar el color del texto al seleccionarlo con el teclado
    public void OnSelect(BaseEventData eventData)
    {
        
        DeselectAllButtons(); // Desseleccionar todos los botones
        textoBoton.color = selectColor; // Cambiar el color del texto al seleccionarlo con el teclado
         GameObject Guardado=GameObject.Find("Guardado");
         ScriptGuardado scriptSaved = Guardado.GetComponent<ScriptGuardado>();
         
         if(scriptSaved.narrador){
            Debug.Log("Volver");
        GameObject Hablar= GameObject.Find("EXAMPLE");
        TextToSpeechExample codigoTexto=Hablar.GetComponent<TextToSpeechExample>();
        codigoTexto.inputField=textoBoton;
        codigoTexto.PressBtn();
         
         }
    }

    // Método para restaurar el color original del texto al deseleccionarlo con el teclado
    public void OnDeselect(BaseEventData eventData)
    {
        textoBoton.color = originalColor; // Restaurar el color original del texto al deseleccionarlo con el teclado
    }

    // Método estático para desseleccionar todos los botones en la escena
    private static void DeselectAllButtons()
    {
        ColorLetras[] botones = FindObjectsOfType<ColorLetras>(); // Obtener todos los componentes ColorLetras en la escena
        foreach (ColorLetras boton in botones)
        {
            EventSystem.current.SetSelectedGameObject(null); // Desseleccionar el botón actual
        }
    }
}
