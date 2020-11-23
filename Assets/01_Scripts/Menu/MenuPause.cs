using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{
    public static bool m_gameIsPaused = false;
    public GameObject m_pauseMenuUI;

    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private PlayerEntity _playerEntity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerEntity = GameObject.Find("Brute Player").GetComponentInChildren<PlayerEntity>();
    }

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
        Cursor.lockState = CursorLockMode.Locked;
        m_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        m_gameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        m_gameIsPaused = true;
    }

    public void ClickOnSave()
    {
        _playerEventStory.PosSave = _playerEntity.PlayerTransform.position;
        Quit_Game();
    }

    public void ClickOnDontSave()
    {
        Quit_Game();
    }

    public void Quit_Game()
    {
        Application.Quit();
    }

    public void DebugPlayer()
    {
        _playerEntity.DebugPlayer();
        Resume();
    }
}
