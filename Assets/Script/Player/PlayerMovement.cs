using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpCooldown = 0.2f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float moveDir = 0;
    private float keyboardInput = 0;

    private bool canJump = true;   // ✅ IMPORTANT
    private float jumpTimer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        keyboardInput = Input.GetAxisRaw("Horizontal");
        float finalMove = keyboardInput != 0 ? keyboardInput : moveDir;

        body.velocity = new Vector2(finalMove * speed, body.velocity.y);

        if (finalMove > 0.03f)
            transform.localScale = Vector3.one * 3;
        else if (finalMove < -0.03f)
            transform.localScale = new Vector3(-3, 3, 3);

        // RESET JUMP WHEN LANDED
        if (isGrounded())
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpCooldown)
                canJump = true;
        }

        anim.SetBool("run", finalMove != 0);
        anim.SetBool("grounded", isGrounded());
    }

    // ✅ SINGLE JUMP BUTTON
    public void JumpButton()
    {
        if (isGrounded() && canJump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");

            canJump = false;   // ❌ lock jump
            jumpTimer = 0f;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.15f,
            groundLayer
        );
        return hit.collider != null;
    }

    // MOVE BUTTONS
    public void LeftPressed()  { moveDir = -1; }
    public void RightPressed() { moveDir = 1; }
    public void StopMove()     { moveDir = 0; }
}
