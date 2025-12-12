using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // MOBILE BUTTON CONTROL
    private float moveDir = 0;   // -1 = left, 1 = right, 0 = idle

    private float keyboardInput = 0; // for keyboard movement

    // Jump cooldown variables
    private bool canJump = true;
    private float jumpCooldown = 0.3f; // seconds

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // KEYBOARD SUPPORT
        keyboardInput = Input.GetAxisRaw("Horizontal");

        // PRIORITY: Buttons override keyboard
        float finalMove = keyboardInput != 0 ? keyboardInput : moveDir;

        // MOVE
        body.velocity = new Vector2(finalMove * speed, body.velocity.y);

        // FLIP PLAYER
        if (finalMove > 0.03f)
            transform.localScale = Vector3.one * 3;
        else if (finalMove < -0.03f)
            transform.localScale = new Vector3(-3, 3, 3);

        // KEYBOARD JUMP
        if (Input.GetKeyDown(KeyCode.Space))
            JumpButton();

        anim.SetBool("run", finalMove != 0);
        anim.SetBool("grounded", isGrounded());
    }

    // JUMP FOR BOTH BUTTON & KEYBOARD
    public void JumpButton()
    {
        if (isGrounded() && canJump)
        {
            Jump();
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0, Vector2.down,
            0.1f,
            groundLayer
        );
        return hit.collider != null;
    }

    public bool canAttack()
    {
        return moveDir == 0 && isGrounded();
    }

    // --- BUTTON FUNCTIONS ---
    public void LeftPressed()  { moveDir = -1; }
    public void RightPressed() { moveDir = 1; }
    public void StopMove()     { moveDir = 0; }

    
    private bool canAttackFlag = true;
    // [SerializeField] private float attackCooldown = 0.5f;

    public void AttackButton()
    {
        if (canAttack() && canAttackFlag)
        {
            Attack();
            canAttackFlag = false;
            // Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        Debug.Log("Attack triggered");
    }

    private void ResetAttack()
    {
        canAttackFlag = true;
    }
}
