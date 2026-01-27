using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject LevelCompletePanel;
    [SerializeField] private GameObject congratulationsPanel;

    [Header("Audio")]
    [SerializeField] private AudioClip winMusic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (LevelCompletePanel != null)
            LevelCompletePanel.SetActive(false);

        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);
    }

    // CALL THIS WHEN LEVEL IS FINISHED
    public void LevelComplete()
    {
        Time.timeScale = 0f; // pause game

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // ✅ LAST LEVEL → CONGRATULATIONS
        if (currentScene == totalScenes - 1)
        {
            ShowCongratulations();
        }
        // ✅ NOT LAST LEVEL → NEXT ERA POPUP
        else
        {
            ShowNextEra();
        }

        if (winMusic != null)
            SoundManager.instance.PlaySound(winMusic);
    }

    // ================= UI FUNCTIONS =================

    private void ShowNextEra()
    {
        if (LevelCompletePanel != null)
            LevelCompletePanel.SetActive(true);
    }

    private void ShowCongratulations()
    {
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(true);
    }

    // ================= BUTTON FUNCTIONS =================

    // BUTTON: Continue to Next Era
    public void GoToNextEra()
    {
        Time.timeScale = 1f;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    // BUTTON: Back to Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenu");
    }
}
