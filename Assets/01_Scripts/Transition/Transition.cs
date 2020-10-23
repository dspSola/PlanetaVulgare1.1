using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private string m_destinationScene;
    [SerializeField] private GameObject button;
    [SerializeField] private float _timedelay;

    public void ChangeScene()
    {
        SceneManager.LoadScene(m_destinationScene);
    }

    IEnumerator MyCouroutine()
    {
        yield return new WaitForSeconds(_timedelay);
        button.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(MyCouroutine());

    }
}
