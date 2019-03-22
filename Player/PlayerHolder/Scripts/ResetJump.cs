using UnityEngine;

public class ResetJump : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        gameObject.GetComponentInParent<Movement>().isJumping = false;
    }
}
