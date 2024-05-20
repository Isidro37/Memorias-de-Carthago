using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restablecer : MonoBehaviour
{
    // Start is called before the first frame update
            

    void Start()
    {
        PlayerPrefs.SetInt("Restablecer", 1);
                PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
