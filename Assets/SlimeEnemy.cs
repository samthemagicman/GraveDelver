using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour, IEnemy
{
    Rigidbody2D rb;
    Animator animator;
    bool inAir = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int ran = Random.Range(0, 100);
        if (ran == 1)
        {
            animator.SetBool("Jump", true);
        } else
        {
            animator.SetBool("Jump", false);
        }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            rb.velocity = (PlayerController.singleton.player.transform.position - transform.position).normalized * 4;
        } else
        {
            rb.velocity = rb.velocity * 0.8f;
        }
    }

    public void InAir()
    {
        inAir = true;
    }

    public void OutOfAir()
    {
        inAir = false;
    }

    public void Knockback(Vector3 origin, float power = 15)
    {
        inAir = false;
        animator.SetTrigger("Hit");
        rb.velocity = -(origin - transform.position).normalized * power;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //PlayerController.singleton.Knockback(transform.position);
        }
    }
}
