using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AumentarCamara : MonoBehaviour
{
    // Start is called before the first frame update
        CinemachineVirtualCamera cinemachineCamera;
    bool isInTrigger = false;

    void Start()
    {
                cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update(){
      if (isInTrigger)
        {
            cambiarcamara();
        }else{
            if(!isInTrigger){
                if (cinemachineCamera.m_Lens.OrthographicSize > 7)
                {
                    cinemachineCamera.m_Lens.OrthographicSize -= 0.006f; // Establece el tamaño grande
                }
            }
        }
    }
    private void OnTriggerEnter2D()
    {
               isInTrigger = true;
  

    }
       private void OnTriggerExit2D()
    {
        // El personaje ha salido del trigger
        isInTrigger = false;
    }

    void cambiarcamara(){
        if (cinemachineCamera.m_Lens.OrthographicSize < 10)
                {
                    cinemachineCamera.m_Lens.OrthographicSize += 0.006f; // Establece el tamaño grande
                }
    }
}
