using UnityEngine;

public class Health : MonoBehaviour
{
 [SerializeField] private float startHealth;
 public float currentHealth { get; private set; }

 private void Awake()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
        
        if (currentHealth > 0)
        {
            
        }
        else
        {
            
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}
