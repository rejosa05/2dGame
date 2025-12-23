// using UnityEngine;

// public class PlayerAttack : MonoBehaviour
// {
//     [SerializeField] private float attackCooldown;
//     [SerializeField] private Transform firePoint;
//     [SerializeField] private GameObject[] bullets;
//     [SerializeField] private AudioClip bulletSound;
//     private Animator anim;
//     private PlayerMovement playerMovement;
//     private float cooldownTimer = Mathf.Infinity;

//      private void Awake()
//     {
//         anim = GetComponent<Animator>();
//         playerMovement = GetComponent<PlayerMovement>();
//     }

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown)
//             Attack();

//         cooldownTimer += Time.deltaTime;
//     }
    
//     private void Attack()
//     {   
//         SoundManager.instance.PlaySound(bulletSound);
//         anim.SetTrigger("attack");
//         cooldownTimer = 0;

//         bullets[findBullet()].transform.position = firePoint.position;
//         bullets[findBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
//     }

//     private int findBullet()
//     {
//         for (int i = 0; i < bullets.Length; i++)
//         {
//             if (!bullets[i].activeInHierarchy)
//                 return i;
//         }
//         return 0; // no bullet found
//     }
// }

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private AudioClip bulletSound;

    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    // ✅ THIS IS CALLED BY BUTTON B ONLY
    public void AttackButton()
    {
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0f;

        if (bulletSound != null)
            SoundManager.instance.PlaySound(bulletSound);

        GameObject bullet = bullets[FindBullet()];
        bullet.transform.position = firePoint.position;
        bullet.SetActive(true);
        bullet.GetComponent<Projectile>()
              .SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
