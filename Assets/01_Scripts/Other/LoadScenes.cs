using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.Find("MusicIntro"));
        SceneManager.LoadScene("Lights", LoadSceneMode.Additive);
        SceneManager.LoadScene("Level01Bis", LoadSceneMode.Additive);
        SceneManager.LoadScene("BabouchePlayer", LoadSceneMode.Additive);
        SceneManager.LoadScene("EnemyMushroomMutant", LoadSceneMode.Additive);
        SceneManager.LoadScene("UiScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("DemsPortal", LoadSceneMode.Additive);
        SceneManager.LoadScene("Audio", LoadSceneMode.Additive);
        SceneManager.LoadScene("MenuPause", LoadSceneMode.Additive);
        SceneManager.LoadScene("CutScene 1", LoadSceneMode.Additive);
    }
}
