using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio_Manager : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
void Start()
{
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        Debug.LogError("AudioSource component not found!");
    }
}

public void ReproducirSonido(AudioClip audio)
{
    Debug.Log("Attempting to play audio clip: " + audio.name);
    if (audioSource == null)
    {
        Debug.LogError("AudioSource component is null!");
        return;
    }

    audioSource.PlayOneShot(audio);
}
    public void ReproducirSonidoLoop(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.loop = true; // Establece el loop en true para reproducir en bucle
       audioSource.Play();
    }

    public void DetenerReproduccion()
    {
        audioSource.Stop();
    }

}
