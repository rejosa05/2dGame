using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip collectSound;

    [SerializeField] private AudioClip correctSound; // play if correct
    [SerializeField] private AudioClip wrongSound;   // play if wrong

    private bool waitingForAnswer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !waitingForAnswer)
        {
            if (QuizManager.instance == null)
            {
                Debug.LogError("QuizManager instance is NULL!");
                return;
            }

            waitingForAnswer = true;
            QuizManager.instance.StartQuestion(this);
        }
    }

    // Called if ANSWER is CORRECT
    public void OnCorrectAnswer(Collider2D player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
            health.AddHealth(healthValue);
        else
            Debug.LogError("Player has no Health script!");

        if (correctSound != null && SoundManager.instance != null)
            SoundManager.instance.PlaySound(correctSound);

        if (collectSound != null && SoundManager.instance != null)
            SoundManager.instance.PlaySound(collectSound);

        gameObject.SetActive(false);
        waitingForAnswer = false;
    }

    // Called if ANSWER is WRONG
    public void OnWrongAnswer()
    {
        // Play wrong sound
        if (wrongSound != null)
            SoundManager.instance.PlaySound(wrongSound);

        // Shake screen / UI
        // ShakeManager.instance.Shake();

        waitingForAnswer = false;

        // Still remove the collectible
        gameObject.SetActive(false);
    }
}
