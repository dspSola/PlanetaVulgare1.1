using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    public static bool m_gameIsPaused = false;
    public GameObject m_pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if(m_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        m_gameIsPaused = false;
    }

    void Pause()
    {
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        m_gameIsPaused = true;
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
