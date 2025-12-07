using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionDatabase database;
    public Text questionText;
    public Text[] optionTexts;
    public GameObject quizPanel;

    private Question currentQuestion;
    private int correctAnswer;

    public static QuizManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowRandomQuestion()
    {
        quizPanel.SetActive(true);
        Time.timeScale = 0; // freeze game

        int index = Random.Range(0, database.questions.Length);
        currentQuestion = database.questions[index];

        questionText.text = currentQuestion.prompt;

        for (int i = 0; i < optionTexts.Length; i++)
            optionTexts[i].text = currentQuestion.options[i];

        correctAnswer = currentQuestion.answer;
    }

    public void SelectAnswer(int choice)
    {
        if (choice == correctAnswer)
        {
            Debug.Log("Correct!");
            quizPanel.SetActive(false);
            Time.timeScale = 1; 
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }
}