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

    [Header("Result UI")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text resultTitleText;   // TOP TEXT
    [SerializeField] private Text resultAnswerText;  // BOTTOM TEXT

    [Header("Result Images")]
    [SerializeField] private Image happyImage;
    [SerializeField] private Image sadImage;

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
        Time.timeScale = 0f;

        int index = Random.Range(0, database.questions.Length);
        currentQuestion = database.questions[index];

        questionText.text = currentQuestion.prompt;

        for (int i = 0; i < optionTexts.Length; i++)
        {
            if (i < currentQuestion.options.Length)
                optionTexts[i].text = currentQuestion.options[i];
            else
                optionTexts[i].text = "";
        }

        correctAnswerIndex = Mathf.Clamp(
            currentQuestion.answer - 1,
            0,
            currentQuestion.options.Length - 1
        );
    }

    public void SelectAnswer(int choice)
    {
        quizPanel.SetActive(false);

        HealthCollectible tempCollectible = currentCollectible;
        currentCollectible = null;

        string correctAnswerText =
            currentQuestion.options[correctAnswerIndex];

        if (choice == correctAnswerIndex)
        {
            ShowResult(true, correctAnswerText);

            if (tempCollectible != null)
                tempCollectible.OnCorrectAnswer(currentPlayer);
        }
        else
        {
            ShowResult(false, correctAnswerText);

            if (tempCollectible != null)
                tempCollectible.OnWrongAnswer();
        }
    }

    // ✅ RESULT DISPLAY
    private void ShowResult(bool isCorrect, string correctAnswer)
    {
        resultPanel.SetActive(true);
        Time.timeScale = 0f;

        happyImage.gameObject.SetActive(false);
        sadImage.gameObject.SetActive(false);
        

        if (isCorrect)
        {
            resultTitleText.text = "TAMA ANG SAGOT!";
            resultAnswerText.text = "Magaling! Tama ang sagot mo.";
            happyImage.gameObject.SetActive(true);
        }
        else
        {
            resultTitleText.text = "MALI ANG SAGOT!";
            resultAnswerText.text = "Tamang Sagot:\n" + correctAnswer;
            sadImage.gameObject.SetActive(true);
        }
    }

    // BUTTON FUNCTION
    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
