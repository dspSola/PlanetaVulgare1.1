using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public string m_destinationScene;
    public GameObject buton;


    public void ChangeScene()
    {
        SceneManager.LoadScene(m_destinationScene);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }

    
    

    IEnumerator MyCouroutine()
    {
        yield return new WaitForSeconds(5);
        buton.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyCouroutine());

    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
