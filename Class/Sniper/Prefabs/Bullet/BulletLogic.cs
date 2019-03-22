using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public int damage;
    public int headShot;

    private bool colided = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colided)
        {
            if (collision.gameObject.layer == 8)
            {
                colided = true;
                if (collision.gameObject.tag == "Player")
                    collision.gameObject.GetComponentInParent<Health>().damage(damage);
                else
                    collision.gameObject.GetComponentInParent<Health>().damage(damage * headShot);
            }
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject, 0.1f);
        }
    }
}
