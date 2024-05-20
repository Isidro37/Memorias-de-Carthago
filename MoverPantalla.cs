using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowMouse : MonoBehaviour
{
    public float sensitivity = 0.1f; // Sensibilidad del movimiento de la cámara
    private CinemachineBrain cineMachineBrain;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        // Obtener la referencia de la CinemachineBrain
        cineMachineBrain = GetComponent<CinemachineBrain>();

        // Obtener las dimensiones de la pantalla
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Centrar el cursor del ratón en la pantalla
        CenterMouse();
    }

    void Update()
    {
        // Obtener la posición del ratón
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Verificar si el ratón está en los bordes de la pantalla
        bool mouseAtEdgeX = (mouseX == 0f || (mouseX > 0f && Input.mousePosition.x >= screenWidth - 1) || (mouseX < 0f && Input.mousePosition.x <= 1));
        bool mouseAtEdgeY = (mouseY == 0f || (mouseY > 0f && Input.mousePosition.y >= screenHeight - 1) || (mouseY < 0f && Input.mousePosition.y <= 1));

        if (mouseAtEdgeX || mouseAtEdgeY)
        {
            // Detener el movimiento de la cámara
            return;
        }

        // Obtener el delta de tiempo para suavizar el movimiento
        float deltaTime = Time.deltaTime;

        // Mover la cámara en función del movimiento del ratón
        Vector3 cameraOffset = new Vector3(mouseX, mouseY, 0f) * sensitivity;
        Vector3 newPosition = cineMachineBrain.transform.position + cameraOffset;

        // Aplicar la nueva posición a la CinemachineBrain
        cineMachineBrain.transform.position = Vector3.Lerp(cineMachineBrain.transform.position, newPosition, deltaTime);
    }

    void CenterMouse()
    {
        // Centrar el cursor del ratón en la pantalla
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
