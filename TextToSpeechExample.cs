using System;
using GoogleTextToSpeech.Scripts.Data;
using TMPro;
using UnityEngine;

namespace GoogleTextToSpeech.Scripts.Example
{
    public class TextToSpeechExample : MonoBehaviour
    {
        [SerializeField] private VoiceScriptableObject voice;
        [SerializeField] private TextToSpeech textToSpeech;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] public TextMeshProUGUI inputField;

        private Action<AudioClip> _audioClipReceived;
        private Action<BadRequestData> _errorReceived;
        
        public void PressBtn()
        {
            Debug.Log("Una");
            _errorReceived += ErrorReceived;
            _audioClipReceived += AudioClipReceived;
            textToSpeech.GetSpeechAudioFromGoogle(inputField.text, voice, _audioClipReceived, _errorReceived);
            
        }
         public void llamar(string texto)
        {
            _errorReceived += ErrorReceived;
            _audioClipReceived += AudioClipReceived;
            textToSpeech.GetSpeechAudioFromGoogle(texto, voice, _audioClipReceived, _errorReceived);
            
        }

        private void ErrorReceived(BadRequestData badRequestData)
        {
            Debug.Log($"Error {badRequestData.error.code} : {badRequestData.error.message}");
        }

        private void AudioClipReceived(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
