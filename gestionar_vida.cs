using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gestionar_vida : MonoBehaviour
{
    // Start is called before the first frame update
    int vida=3;
        private GameObject corazon;
        private character_controller movimientoJugador;
        [SerializeField] private float tiempoPerdidaControl=1;

        private Animator animator;
    void Start()
    {
        movimientoJugador=GetComponent<character_controller>();
        animator=GetComponent<Animator>();
        tiempoPerdidaControl=1;
    }
   public void DetectarDaño(bool daño)
{
    if(daño){

        vida=vida-1;
        Corazones();
    }
}
private void Corazones()
{
    corazon = GameObject.Find("Corazon"+(vida+1));
Image imagen = corazon.GetComponentInChildren<Image>();
    Animator animator = imagen.GetComponent<Animator>();
    animator.SetBool("daño_hecho", true);
}
public void HacerDaño( Vector2 posicion){
    StartCoroutine(PerderControl());
movimientoJugador.Knockback(posicion);

}
private IEnumerator PerderControl(){
    
    movimientoJugador.sePuedeMover=false;
    Debug.Log("No moverse");
    yield return new WaitForSeconds(tiempoPerdidaControl/4);
    movimientoJugador.sePuedeMover=true;
    Debug.Log("Si moverse");
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
