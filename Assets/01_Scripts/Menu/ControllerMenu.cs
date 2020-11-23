using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerMenu : MonoBehaviour
{
    public string m_destinationSceneNewGame, m_destinationSceneContinue;
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private GameObject _buttonContinue, _buttonNew;
    [SerializeField] private EventSystem _eventSystem;

    private void Start()
    {
        if(!_playerEventStory.GameExist)
        {
            _buttonContinue.gameObject.SetActive(false);
        }
    }

    public void ChangeScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    public void NewGame()
    {
        _playerEventStory.NewGame();
        ChangeScene(m_destinationSceneNewGame);
    }

    public void Continue()
    {
        _playerEventStory.Continue();
        ChangeScene(m_destinationSceneContinue);
    }

    public void ClickOnPlay()
    {
        if (!_playerEventStory.GameExist)
        {
            _eventSystem.SetSelectedGameObject(_buttonNew);
        }
        else
        {
            _eventSystem.SetSelectedGameObject(_buttonContinue);
        }
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
