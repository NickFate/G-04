using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float range = 1;

    private int damage;
    private Vector3 vel;
    private int speed = 7;

    private float timer = 7;

    public void Set(Vector3 dir, int damage)
    {
        this.damage = damage;
        vel = dir;
        vel *= speed;
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + vel * Time.fixedDeltaTime;

        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, range);

        if (coll.Length > 0)
        {
            foreach (Collider2D col in coll)
            {

                if (col.GetComponent<Character>())
                {
                    col.GetComponent<Character>().TakeDamage(1);
                }
            }
        }

        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
