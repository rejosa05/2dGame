using UnityEngine;

public class TrophyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.LevelComplete();
            Destroy(gameObject);
        }
    }
}
