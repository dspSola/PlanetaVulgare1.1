using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private void Awake()
    {
            SceneManager.LoadScene("Lights", LoadSceneMode.Additive);
            SceneManager.LoadScene("Level01Bis", LoadSceneMode.Additive);
            SceneManager.LoadScene("BabouchePlayer", LoadSceneMode.Additive);
            SceneManager.LoadScene("WaterBossScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("EnemyMushroomMutant", LoadSceneMode.Additive);
            //SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}
