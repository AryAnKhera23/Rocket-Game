using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Update() 
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Closing Application");
            Application.Quit();
        }
    }
}
