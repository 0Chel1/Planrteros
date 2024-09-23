using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MenuAndPouseMenuButtons : MonoBehaviour
{
    public GameObject PouseMenu;
    private bool pushed = false;
    public int MenuType;

    private void Update()
    {
        if(MenuType == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !pushed)
            {
                PouseMenu.SetActive(true);
                pushed = true;
                Time.timeScale = 0;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && pushed)
            {
                PouseMenu.SetActive(false);
                pushed = false;
                Time.timeScale = 1;
            }
        }
    }

    public void PlayTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void ExitInMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        PouseMenu.SetActive(false);
        pushed = false;
        Time.timeScale = 1;
    }
}
