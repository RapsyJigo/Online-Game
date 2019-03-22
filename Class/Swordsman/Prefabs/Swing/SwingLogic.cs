using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLogic : MonoBehaviour
{
    public int damage;
    private bool hit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        if (collision.transform.gameObject.layer == 8)
        {
            collision.transform.gameObject.GetComponent<Health>().damage(damage);
        }
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        Destroy(gameObject);
    }
}
