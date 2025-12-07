using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;


    [Header ("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    [Header ("Player Parameters")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;



    private void Awake()
        {
            currentHealth = startHealth;
            anim = GetComponent<Animator>();
            spriteRend = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);
            
            if (currentHealth > 0)
            {
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
                SoundManager.instance.PlaySound(hurtSound);
            }
            else
            {
                if (!dead)
                {
                    anim.SetTrigger("die");

                    foreach (Behaviour component in components)
                        component.enabled = false;
                         
                    dead = true;
                    SoundManager.instance.PlaySound(deathSound);
                }
            }
        }
    
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startHealth);
    }
    
    public void Respawn()
    {
        dead = false;
        AddHealth(startHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invunerability()
    {   
        // invulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1,  0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
        // invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
