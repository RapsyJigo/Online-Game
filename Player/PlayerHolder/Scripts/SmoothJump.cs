using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothJump : MonoBehaviour
{
    [SerializeField] [Tooltip("The fall acceleration multiplier")] private float fall;
    [SerializeField] [Tooltip("The upwards acceleration multiplier")] private float upwards;

    private Rigidbody2D rb;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isJumping = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.fixedDeltaTime;
        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (upwards - 1) * Time.fixedDeltaTime;
        }
    }
}
