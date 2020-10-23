using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour
{
    public string m_destinationScene;


    public void ChangeScene()
    {
        SceneManager.LoadScene(m_destinationScene);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
