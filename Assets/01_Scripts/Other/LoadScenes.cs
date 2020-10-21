using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private void Awake()
    {
        private void Awake()
        {
            SceneManager.LoadScene("Camera", LoadSceneMode.Additive);
            SceneManager.LoadScene("Audio", LoadSceneMode.Additive);
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
            SceneManager.LoadScene("Level", LoadSceneMode.Additive);
            SceneManager.LoadScene("Enemys", LoadSceneMode.Additive);
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
    }
}
