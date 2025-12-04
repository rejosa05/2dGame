using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(collectSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
            // Health playerHealth = collision.GetComponent<Health>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(-healthAmount); // Negative damage to heal
            //     Destroy(gameObject); // Destroy the collectible after use
            // }
        }
    }
}
