using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject levelCompletePanel;

    private void Awake()
    {
        instance = this;
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("LEVEL COMPLETE");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
}
