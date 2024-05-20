using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using System;
using Firebase.Storage;
using System.IO;
public class InicioSesion : MonoBehaviour
{
    private FirebaseAuth auth;
    FirebaseStorage storage;
    public GameObject inicioSesión;
    public GameObject registro;
    public GameObject popup;
    public TMP_InputField correoInicioSesión;
    public TMP_InputField contraseñaInicioSesión;
        public TMP_InputField correoRegistro;
    public TMP_InputField contraseñaRegistro;
    public TMP_Text error;
    bool fallo=false;
    string textoFallo;
    bool dentro=false;
    bool registroCorrecto=false;
    public GameObject GuardarUser;
    bool remember=false;
    public Toggle CheckRecordar;

  
    string userId;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 540, false);
         FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Inicializa Firebase Auth
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
        });
        LoadUserData();
    }
    public void SignIn(string email, string password)
    {
        if(string.IsNullOrEmpty(email)||string.IsNullOrEmpty(password)){error.text="Rellena todos los campos";}else{
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                fallo=true;
                textoFallo="Se ha cancelado el proceso, intentalo de nuevo";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                fallo=true;
                textoFallo=task.Exception.ToString();
                return;
            }
            
            AuthResult authResult = task.Result;
            
            FirebaseUser user = authResult.User;
             
            
            Debug.Log("User signed in successfully: " + user.Email);
            Debug.Log("Cuco");
            dentro=true;

            

            
        });
        }
    }

    
    public void irJuego(){Screen.SetResolution(1920, 1080, true);
         SceneManager.LoadScene("Portada");}
    public void Register(string email, string password)
    {
         if(string.IsNullOrEmpty(email)||string.IsNullOrEmpty(password)){error.text="Rellena todos los campos";}
         else{
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                fallo=true;
                textoFallo="Se ha cancelado el proceso, intentalo de nuevo";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                fallo=true;
                textoFallo=task.Exception.ToString();
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("User registered successfully: " + newUser.Email);
            registroCorrecto=true;
            
            
        });
         }
    }
    
    public void MarcarRecordar(){

        remember=CheckRecordar.isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(dentro){

            SaveUser saveUser=GuardarUser.GetComponent<SaveUser>();
            userId=correoInicioSesión.text;
            saveUser.userid=userId;
            Debug.Log("En Inicio Sesion"+userId);
            if(remember){SaveUserData();}
            if(!remember){DeleteUserData();}
            irJuego(); dentro=false;}
        if(registroCorrecto){popup.SetActive(true); 
        error.text="";}
        if(fallo){


 
    if (textoFallo.Contains("The email address is badly formatted"))
    {
        error.text = "El formato del correo electrónico es incorrecto.";
    }
    else if (textoFallo.Contains("The given password is invalid"))
    {
        error.text = "La contraseña es demasiado débil. Debe contener al menos 6 caracteres.";
    }
    else if (textoFallo.Contains("The email address is already in use by another account"))
    {
        error.text = "Ya existe un usuario con este correo electrónico.";
    }
    else if (textoFallo.Contains("An internal error has occurred"))
    {
        error.text = "La contraseña o el correo son incorrectos.";
    }
    else
    {
        error.text = "Ha ocurrido un error al intentar iniciar sesión o registrar usuario.";
    }

  

            fallo=false;

        }

    }
    public void IrJuegoIniciarSesion(){
         
         SignIn(correoInicioSesión.text,contraseñaInicioSesión.text);
    }
    public void IrJuegoRegistrarse()    
    {
        Debug.Log(correoRegistro.text);
        Register(correoRegistro.text.ToString(),contraseñaRegistro.text.ToString());
    }
    public void IrRegistro(){
        error.text="";
        registro.SetActive(true);
        inicioSesión.SetActive(false);
    }
    public void IrInicioSesión(){
        error.text="";
        correoRegistro.text="";
        contraseñaRegistro.text="";
        registroCorrecto=false;
        popup.SetActive(false);
        registro.SetActive(false);
        inicioSesión.SetActive(true);
        correoInicioSesión.text="";
        contraseñaInicioSesión.text="";
    }
     void Awake()
    {
         Screen.SetResolution(960, 540, false);
         Vector2Int windowSize = new Vector2Int(960, 540);
        // Calcular la posición para centrar la ventana
        int windowPosX = (Screen.currentResolution.width - windowSize.x) / 2;
        int windowPosY = (Screen.currentResolution.height - windowSize.y) / 2;
    }


    public void SaveUserData()
{
    if(remember){
    UserData userData = new UserData();
    userData.email = correoInicioSesión.text;
    userData.password = contraseñaInicioSesión.text;
    userData.remember = remember;

    string json = JsonUtility.ToJson(userData);

    // Ruta donde se guardará el archivo JSON
    string filePath = Application.persistentDataPath + "/userData.json";

    // Verificar si el archivo ya existe
    if (!File.Exists(filePath))
    {
        // Si el archivo no existe, crearlo
        File.WriteAllText(filePath, json);
    }
    else
    {
        // Si el archivo ya existe, sobrescribirlo con los nuevos datos
        File.WriteAllText(filePath, json);
    }
    }
}
public void LoadUserData()
    {
        // Ruta del archivo JSON
        string filePath = Application.persistentDataPath + "/userData.json";

        if (File.Exists(filePath))
        {
            // Leer el JSON desde el archivo
            string json = File.ReadAllText(filePath);

            // Deserializar el JSON a un objeto UserData
            UserData userData = JsonUtility.FromJson<UserData>(json);

            // Asignar los valores del objeto UserData a las variables del script
            if(userData.remember){
            
            correoInicioSesión.text =userData.email;
            contraseñaInicioSesión.text=userData.password;
            CheckRecordar.isOn=userData.remember;
            }
            
        }
    }
    public void DeleteUserData()
    {
        // Ruta del archivo JSON
        string filePath = Application.persistentDataPath + "/userData.json";

        // Verificar si el archivo existe antes de intentar eliminarlo
        if (File.Exists(filePath))
        {
            // Eliminar el archivo
            File.Delete(filePath);
            Debug.Log("Archivo de usuario eliminado correctamente.");
        }
        else
        {
            Debug.Log("El archivo de usuario no existe.");
        }
    }
}

[System.Serializable]
public class UserData
{
    public string email;
    public string password;
    public bool remember;
}
