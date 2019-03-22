using UnityEngine;
using System.Collections;

public class TrapLogic : MonoBehaviour
{
    public float duration;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if (collision.gameObject.layer == 8)
        {
            Rigidbody2D victim = collision.gameObject.GetComponent<Rigidbody2D>();
            victim.mass = 1000;
            victim.drag = 1000;
            StartCoroutine(debuff(victim));

            gameObject.transform.SetPositionAndRotation(new Vector3(100, 100, 0),transform.rotation);
        }
    }
    IEnumerator debuff (Rigidbody2D victim)
    {
        yield return new WaitForSeconds(duration);
        victim.mass = 1;
        victim.drag = 0;
    }

}
