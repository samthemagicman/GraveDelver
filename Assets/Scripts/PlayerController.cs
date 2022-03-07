using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 5;


    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer renderer;

    public GameObject player;
    public static PlayerController singleton;

    float knockbackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        player = gameObject;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("VelocityMagnitude", rb.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("VelocityMagnitude", false);
        }*/

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        if (dir.x < 0)
        {
            renderer.flipX = true;
        } else
        {
            renderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (Time.realtimeSinceStartup - knockbackTime < 0.1f)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("VelocityMagnitude", rb.velocity.magnitude);
        animator.SetFloat("VelocityNormal", rb.velocity.magnitude / walkspeed);

        animator.SetBool("Walking", horizontal != 0 || vertical != 0);

        rb.velocity = new Vector2(horizontal, vertical).normalized * walkspeed;
    }

    public void Knockback(Vector3 origin, float power = 15)
    {
        knockbackTime = Time.realtimeSinceStartup;
        rb.velocity = (transform.position - origin).normalized * power;
    }
}
