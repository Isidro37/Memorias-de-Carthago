using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Extensions;
using Firebase.Auth;

public class ScriptGuardado : MonoBehaviour
{
    // Variables para almacenar los datos
    public int volume = 10;
    public int bright = 10;
    public string scene;
    public string language;
    public bool lever1;
    public bool narrador = false; // Nuevo campo agregado
    public int money;
    public Vector3 playerPos = new Vector3(0, 0, 0);
    public GameObject musicManager;
    public GameObject pasosManager;
    public GameObject audioManager;
    public GameObject Menu;
    FirebaseAuth auth;

    // Nombre del archivo donde se guardan los datos
    string filename = "saveData.json";

    // Ruta completa del archivo
    string filePath;

    // Singleton para asegurarse de que solo hay una instancia del script en la escena
    static ScriptGuardado instance;
    FirebaseStorage storage;
    StorageReference storageReference;

    // Método para asegurarse de que solo hay una instancia del script en la escena
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // Obtener la ruta completa del archivo
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://memorias-de-carthago.appspot.com");

        // Cargar los datos desde el archivo
        CargarDatos();
    }

    // Método para cargar los datos desde el archivo JSON
    void CargarDatos()
    {
        // Obtener el ID único del usuario actual
        GameObject guardado = GameObject.Find("SaveUser");
        SaveUser saveUser = guardado.GetComponent<SaveUser>();
        string userId = saveUser.userid;
        Debug.Log(userId);

        // Crear referencia al archivo del usuario
        StorageReference userFileRef = storageReference.Child("userFiles/" + userId + "/saveData.json");

        // Descargar el archivo
        userFileRef.GetBytesAsync(long.MaxValue).ContinueWithOnMainThread((task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogWarning("No se pudo cargar el archivo del usuario.");

                // Actualizar la escena con los datos cargados
                bright = 10;
                volume = 10;
                ChangeDataOnScene();
                Menu = GameObject.Find("Canvas Menu");
                MenuController menuController = Menu.GetComponent<MenuController>();
                menuController.datosCargados();

            }
            else
            {
                byte[] bytes = task.Result;
                string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
                ScriptGuardadoData data = JsonUtility.FromJson<ScriptGuardadoData>(jsonString);

                // Actualizar los datos del juego con los datos del archivo
                volume = data.volume;
                bright = data.bright;
                scene = data.scene;
                language = data.language;
                lever1 = data.lever1;
                narrador = data.narrador; // Asignar valor al narrador
                money = data.money;
                playerPos = data.playerPos;
                Debug.Log(playerPos);
                // Actualizar la escena con los datos cargados
                ChangeDataOnScene();
                Menu = GameObject.Find("Canvas Menu");
                MenuController menuController = Menu.GetComponent<MenuController>();
                menuController.datosCargados();
            }
        });
    }

    // Método para guardar los datos en Firebase
    public void GuardarDatos()
    {
        // Obtener el ID único del usuario actual
        GameObject guardado = GameObject.Find("SaveUser");
        SaveUser saveUser = guardado.GetComponent<SaveUser>();
        string userId = saveUser.userid;

        // Serializar los datos en formato JSON
        ScriptGuardadoData data = new ScriptGuardadoData();
        data.volume = volume;
        data.bright = bright;
        data.scene = scene;
        data.language = language;
        data.lever1 = lever1;
        data.narrador = narrador; // Incluir el narrador en los datos a guardar
        data.money = money;
        data.playerPos = playerPos;

        string jsonString = JsonUtility.ToJson(data);

        // Convertir el string JSON a bytes
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        // Crear referencia al archivo del usuario
        StorageReference userFileRef = storageReference.Child("userFiles/" + userId + "/saveData.json");

        // Subir los datos a Firebase Storage
        StartCoroutine(UploadDataToFirebase(userFileRef, bytes));
    }

    IEnumerator UploadDataToFirebase(StorageReference reference, byte[] data)
    {
        bool isUploadSuccessful = false;

        while (!isUploadSuccessful)
        {
            reference.PutBytesAsync(data).ContinueWithOnMainThread((task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogWarning("No se pudo guardar el archivo del usuario.");
                }
                else
                {
                    Debug.Log("Datos guardados en Firebase Storage.");
                    isUploadSuccessful = true;
                }
            });

            // Esperar un corto tiempo antes de intentar nuevamente
            // para evitar consumir demasiados recursos
            WaitForSeconds wait = new WaitForSeconds(0.5f);
            yield return wait;
        }
    }

    // Método para guardar los datos al cerrar la aplicación
    void OnApplicationQuit()
    {
        GuardarDatos();
    }

    // Update is called once per frame
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeDataOnScene();
        Menu = GameObject.Find("Canvas Menu");
        MenuController menuController = Menu.GetComponent<MenuController>();
        Debug.Log("Llamando a datos cargados");
        menuController.datosCargados();
    }
    void Update()
    {

    }
    public void VolumeChange(int vol)
    {
        volume = vol;
        AjustarBrilloyVolumen();
    }
    public void BrightChange(int bri)
    {
        bright = bri;
        AjustarBrilloyVolumen();
    }
    public void ChangeDataOnScene()
    {
        audioManager = GameObject.Find("Audio Manager");
        pasosManager = GameObject.Find("Pasos manager");
        musicManager = GameObject.Find("Music Manager");
        AudioSource audioSource = audioManager.GetComponent<AudioSource>();
        AudioSource pasosSource = pasosManager.GetComponent<AudioSource>();
        AudioSource musicSource = musicManager.GetComponent<AudioSource>();
        audioSource.volume = (float)(1 * (volume * 0.1));
        pasosSource.volume = (float)(0.24 * (volume * 0.1));
        musicSource.volume = (float)(0.02 * (volume * 0.1));
        GameObject ObjectBrillo = GameObject.Find("Imagen Brillo");

        Image ImagenBrillo = ObjectBrillo.GetComponent<Image>();
        Color colorImagen = ImagenBrillo.color;
        ImagenBrillo.color = new Color(colorImagen.r, colorImagen.g, colorImagen.b, (float)((10 - bright) * 0.08181818181));
        if (SceneManager.GetActiveScene().name == scene)
        {
            GameObject Jugador = GameObject.Find("Personaje");
            if (playerPos != new Vector3(0, 0, 0))
            {
                Jugador.transform.position = playerPos;
            }
        }

    }
    public void AjustarBrilloyVolumen()
    {
        audioManager = GameObject.Find("Audio Manager");
        pasosManager = GameObject.Find("Pasos manager");
        musicManager = GameObject.Find("Music Manager");
        AudioSource audioSource = audioManager.GetComponent<AudioSource>();
        AudioSource pasosSource = pasosManager.GetComponent<AudioSource>();
        AudioSource musicSource = musicManager.GetComponent<AudioSource>();
        audioSource.volume = (float)(1 * (volume * 0.1));
        pasosSource.volume = (float)(0.24 * (volume * 0.1));
        musicSource.volume = (float)(0.02 * (volume * 0.1));
        GameObject ObjectBrillo = GameObject.Find("Imagen Brillo");

        Image ImagenBrillo = ObjectBrillo.GetComponent<Image>();
        Color colorImagen = ImagenBrillo.color;
        ImagenBrillo.color = new Color(colorImagen.r, colorImagen.g, colorImagen.b, (float)((10 - bright) * 0.08181818181));


    }
}

// Clase para almacenar los datos del juego
[System.Serializable]
public class ScriptGuardadoData
{
    public int volume;
    public int bright;
    public string scene;
    public string language;
    public bool lever1;
    public bool narrador; // Nuevo campo agregado
    public int money;
    public Vector3 playerPos;
}
