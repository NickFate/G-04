using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private int speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    [Range (0.1f, 1f)]
    private float attackSpeed;
    [SerializeField]
    private int Max_HP;

    private float timer;

    private int HP;

    Rigidbody2D body;
    Vector3 Dir;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        HP = Max_HP;
    }

    void FixedUpdate()
    {
        Movement(Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space) && timer > attackSpeed)
        {
            Attack();
            timer -= attackSpeed;
        }

        timer += Time.fixedDeltaTime;
    }

    private void Movement(float delta)
    {
        Dir = new Vector3();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Dir.y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Dir.y = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Dir.x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Dir.x = 1;
        }
        Dir *= delta * speed;
        Vector3 pos = transform.position + Dir;

        body.MovePosition(pos);
    }

    private void Attack()
    {

        Vector3 vel = Dir.normalized;
        if (vel == Vector3.zero)
        {
            vel = new Vector3(0, -1, 0);
        }
        GameObject bul = Instantiate<GameObject>(Resources.Load<GameObject>("Bullet"));

        bul.GetComponent<Bullet>().Set(vel, 1);
        bul.transform.position = transform.position + vel;

    }

    public void TakeDamage(int Damage)
    {
        HP -= Damage;

        if (HP <= 0)
        {
            Debug.Log("Game end!");
        }
    }

}
