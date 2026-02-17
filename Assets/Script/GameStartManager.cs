using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel;

    private void Start()
    {
        // Show instruction panel at start
        instructionPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    // Button function
    public void StartGame()
    {
        instructionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
