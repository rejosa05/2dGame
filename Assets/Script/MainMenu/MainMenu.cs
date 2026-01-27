using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject SandataAtTalino;
    public GameObject DaloyNgPagsipalaran;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
    }

    public void ShowSandataAtTalino()
    {
        SandataAtTalino.SetActive(true);
    }

    public void ShowDaloyNgPagsipalaran()
    {
        DaloyNgPagsipalaran.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
    }
    
    public void CloseSandataAtTalino()
    {
        SandataAtTalino.SetActive(false);
    }
    
    public void CloseShowDaloyNgPagsipalaran()
    {
        DaloyNgPagsipalaran.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
