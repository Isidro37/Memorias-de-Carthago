using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Auth;

public class SaveUser : MonoBehaviour
{
    // Variable para almacenar la instancia de este script
    private static SaveUser instance;
    public string userid;

    void Awake()
    {
        // Verificar si ya hay una instancia de este script en la escena
        if (instance == null)
        {
            // Si no hay una instancia, establecer esta como la instancia activa y evitar que se destruya al cargar una nueva escena
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruir este objeto para mantener solo una instancia activa
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
