using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    public Rigidbody2D rb;

    public bool isJumping = true;

    public SpriteRenderer body;
    public SpriteRenderer head;

    public float speed;
    public float jumpSpeed;

    private float move;
    private bool jump;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        if (isLocalPlayer)
        {
            body.color = Color.green;
            head.color = Color.green;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        move = Input.GetAxis("Horizontal");
        jump = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (jump && !isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
            isJumping = true;
        }
    }
}
