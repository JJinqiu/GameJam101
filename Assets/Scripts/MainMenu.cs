﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PIANDUAN;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void showPIANDUAN()
    {
        PIANDUAN.GetComponent<JYandCG>().PIANDUANinit();
        PIANDUAN.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
