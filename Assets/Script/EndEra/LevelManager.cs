using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public static LevelManager instance;

    [Header("UI")]
    [SerializeField] public GameObject congratulationsPanel;
    [SerializeField] public AudioClip winMusic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);
    }

    public void LevelComplete()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // ✅ IF LAST LEVEL
        if (currentScene == totalScenes - 1)
        {
            ShowCongratulations();
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    void ShowCongratulations()
    {
        Time.timeScale = 0f; // pause game
        congratulationsPanel.SetActive(true);

        if(winMusic != null)
            SoundManager.instance.PlaySound(winMusic);

        Time.timeScale = 0f;
    }

    // BUTTON FUNCTION
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenu");
    }
}
