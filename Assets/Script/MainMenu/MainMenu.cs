using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
