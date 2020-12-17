using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu inst;
    

    
    void Awake()
    {
        inst = this;
        display.active = false;
    }

    public void Display(bool b) {
        display.SetActive(b);

        if (b) Time.timeScale = 0;
        else   Time.timeScale = 1;
    }
}
