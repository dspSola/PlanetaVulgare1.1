using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPause : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private string _nameSceneMenu;

    public static bool m_gameIsPaused = false;
    public GameObject m_pauseMenuUI;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerEntity = GameObject.Find("Brute Player").GetComponentInChildren<PlayerEntity>();
        Resume();
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

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        m_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        m_gameIsPaused = true;
    }

    public void ClickOnSave()
    {
        _playerEventStory.PosSave = _playerEntity.PlayerTransform.position;
        _playerEventStory.SaveAllIntoTheText();
    }

    public void ClickOnMenu()
    {
        SceneManager.LoadScene(_nameSceneMenu);
    }

    public void ClickOnQuit()
    {
        Quit_Game();
    }

    public void ClickOnDebugPlayer()
    {
        DebugPlayer();
    }

    private void Quit_Game()
    {
        Application.Quit();
    }

    private void DebugPlayer()
    {
        _playerEntity.DebugPlayer();
        Resume();
    }
}
