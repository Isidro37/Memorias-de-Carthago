using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class acercarCamara : MonoBehaviour
{
    GameObject personaje;
    CinemachineVirtualCamera cinemachineCamera;
    bool isInTrigger = false;
    float posicionX;
    void Start()
    {
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // Ejecuta cambiarcamara() solo si el personaje está dentro del trigger
        if (isInTrigger)
        {
            cambiarcamara();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // El personaje ha entrado en el trigger
        isInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // El personaje ha salido del trigger
        isInTrigger = false;
    }

    void cambiarcamara()
    {
        personaje = GameObject.Find("Personaje");

        if (personaje != null && cinemachineCamera != null)
        {
         
            if (posicionX<personaje.transform.position.x) // Movimiento a la derecha
            {
                             posicionX=personaje.transform.position.x;

                if (cinemachineCamera.m_Lens.OrthographicSize > 4)
                {
                    cinemachineCamera.m_Lens.OrthographicSize -= 0.03f; // Establece el tamaño pequeño
                }
            }
            else if (posicionX>personaje.transform.position.x) // Movimiento a la izquierda
            {
                             posicionX=personaje.transform.position.x;

                if (cinemachineCamera.m_Lens.OrthographicSize < 7)
                {
                    cinemachineCamera.m_Lens.OrthographicSize += 0.03f; // Establece el tamaño grande
                }
            }
        }
    }
}
