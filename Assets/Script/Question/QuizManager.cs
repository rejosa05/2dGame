using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionDatabase database;
    public Text questionText;
    public Text[] optionTexts;
    public GameObject quizPanel;

    private Question currentQuestion;
    private int correctAnswerIndex;

    public static QuizManager instance;

    private HealthCollectible currentCollectible;
    private Collider2D currentPlayer;

    private void Awake()
    {
        instance = this;
    }

    public void StartQuestion(HealthCollectible collectible)
    {
        currentCollectible = collectible;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            currentPlayer = playerObj.GetComponent<Collider2D>();

        ShowRandomQuestion();
    }

    public void ShowRandomQuestion()
    {
        quizPanel.SetActive(true);
        Time.timeScale = 0;

        // Pick a random question
        int index = Random.Range(0, database.questions.Length);
        currentQuestion = database.questions[index];

        // Display question
        questionText.text = currentQuestion.prompt;

        // Display options safely
        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentQuestion.options.Length)
                optionTexts[i].text = currentQuestion.options[i];
            else
                optionTexts[i].text = "N/A";
        }

        // Convert 1-based Inspector answer to 0-based index
        correctAnswerIndex = Mathf.Clamp(currentQuestion.answer - 1, 0, currentQuestion.options.Length - 1);
        Debug.Log("Correct Answer Index: " + correctAnswerIndex);
    }

    public void SelectAnswer(int choice)
    {

        Debug.Log("Player selected option index: " + choice);
        Debug.Log("Correct answer index: " + correctAnswerIndex);
        Debug.Log("Is Correct? " + (choice == correctAnswerIndex));
        quizPanel.SetActive(false);
        Time.timeScale = 1;

        HealthCollectible tempCollectible = currentCollectible;
        currentCollectible = null;

        if (choice == correctAnswerIndex)
        {
            if (tempCollectible != null)
                tempCollectible.OnCorrectAnswer(currentPlayer);
        }
        else
        {
            if (tempCollectible != null)
                tempCollectible.OnWrongAnswer();
        }
    }
}
