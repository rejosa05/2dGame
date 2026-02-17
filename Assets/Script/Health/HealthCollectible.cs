using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;

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

    public void OnCorrectAnswer(Collider2D player)
    {
        Health health = player.GetComponent<Health>();
        if (health != null)
            health.AddHealth(healthValue);

        if (correctSound != null)
            SoundManager.instance.PlaySound(correctSound);

        if (collectSound != null)
            SoundManager.instance.PlaySound(collectSound);

        gameObject.SetActive(false);
        waitingForAnswer = false;
    }

    public void OnWrongAnswer()
    {
        if (wrongSound != null)
            SoundManager.instance.PlaySound(wrongSound);

        gameObject.SetActive(false);
        waitingForAnswer = false;
    }
}
