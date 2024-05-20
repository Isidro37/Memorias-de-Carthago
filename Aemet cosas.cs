using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic; // Agregado para List
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;
public class Aemetcosas : MonoBehaviour
{
    private string apiKey = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJpc2lkcm9nYXJjaWFhcGFyaWNpb0BvdXRsb29rLmVzIiwianRpIjoiZjhlNzY3M2UtZTE1My00MDgwLWIxNzktNTk1ZDA0NTg1MWVlIiwiaXNzIjoiQUVNRVQiLCJpYXQiOjE3MDMwMDE0NDcsInVzZXJJZCI6ImY4ZTc2NzNlLWUxNTMtNDA4MC1iMTc5LTU5NWQwNDU4NTFlZSIsInJvbGUiOiIifQ.UNX2b-vhQbWX40gWtYPc-gIGeQvpfOPJNpLbE8Z40Q8";
    private string baseUrl = "https://opendata.aemet.es/opendata/api/prediccion/especifica/municipio/diaria/30016";
    public List<Dia> dias = new List<Dia>(); // Corregido
    bool terminado=false;
    static Aemetcosas instance;
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

    async void Start()
{
    SceneManager.sceneLoaded += DetectarCambioDeEscena;
    string url = $"{baseUrl}?api_key={apiKey}";
    Debug.Log(url);

    using (HttpClient client = new HttpClient())
    {
        bool success = false;
        while (!success)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Debug.Log(json);

                    var responseData = JsonConvert.DeserializeObject<ResponseData>(json);

                    if (responseData.estado == 200)
                    {
                        HttpResponseMessage newDataResponse = await client.GetAsync(responseData.datos);

                        if (newDataResponse.IsSuccessStatusCode)
                        {
                            string newDataJson = await newDataResponse.Content.ReadAsStringAsync();
                            Debug.Log(newDataJson);

                            var weatherData = JsonConvert.DeserializeObject<WeatherData[]>(newDataJson);

                            foreach (var diaData in weatherData[0].prediccion.dia)
                            {
                                string lluvia = "";

                                // Verifica si hay probabilidad de lluvia
                                foreach (var probPrecipitacion in diaData.probPrecipitacion)
                                {
                                    if (probPrecipitacion.value > 0)
                                    {
                                        lluvia = "Lluvioso";
                                        break;
                                    }
                                }

                                Dia dia = new Dia
                                {
                                    fecha = diaData.fecha,
                                    temperatura = diaData.temperatura,
                                    estadoCielo = diaData.estadoCielo,
                                };

                                dias.Add(dia);
                                Debug.Log($"Día: {dia.fecha}, Temperatura máxima: {dia.temperatura.maxima}, Temperatura mínima: {dia.temperatura.minima}, Lluvia: {lluvia}, Descripción estado cielo: {dia.estadoCielo}");
                            }
                            success = true; // Marcamos la solicitud como exitosa
                        }
                        else
                        {
                            Debug.LogError($"Error en la nueva solicitud: {newDataResponse.StatusCode}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Error en la solicitud: {responseData.estado}");
                    }
                }
                else
                {
                    Debug.LogError($"Error en la solicitud: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error al realizar la solicitud: {ex.Message}");
            }

            // Espera antes de volver a intentar la solicitud
            await Task.Delay(1000); // Espera 1 segundo antes de intentar nuevamente
        }
    }
    terminado = true;
}
 
 void DetectarCambioDeEscena(Scene scene, LoadSceneMode mode)
    {
        // Aquí puedes poner el código que deseas ejecutar cuando cambias de escena
        StartCoroutine(tiempo());
    }
    IEnumerator tiempo(){
        yield return new WaitForSeconds(0.0001f);
        if(terminado){
            GameObject Calend=GameObject.Find("Calendario");
         Calendario calendario=Calend.GetComponent<Calendario>();
         calendario.listo();
        }
    }
    public class ResponseData
    {
        public string descripcion { get; set; }
        public int estado { get; set; }
        public string datos { get; set; }
        public string metadatos { get; set; }
    }

    public class WeatherData
    {
        public Prediccion prediccion { get; set; }
    }

    public class Prediccion
    {
        public Dia[] dia { get; set; }
    }

public class Dia
{
    public string fecha { get; set; }
    public Temperatura temperatura { get; set; }
    public EstadoCielo[] estadoCielo { get; set; } 
    public ProbPrecipitacion[] probPrecipitacion { get; set; }
    public string lluvia { get; set; }
}
    public class Temperatura
    {
        public int maxima { get; set; }
        public int minima { get; set; }
    }

    public class ProbPrecipitacion
    {
        public int value { get; set; }
    }

    public class EstadoCielo
{
    public string value;
    public string periodo;
    public string descripcion;
}
}
