using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UI : MonoBehaviour
{
    public GameObject PIANDUAN;
    public GameObject menu;
    public GameObject[] life;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") )
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        for (int i =0; i < life.Length; i++)
        {
            life[i].gameObject.SetActive(false);
        }
        int health = player.GetComponent<PlayerController>().health;
        for (int i = 0; i < health; i++)
        {
            life[i].gameObject.SetActive(true);
        }
        
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void changeMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
