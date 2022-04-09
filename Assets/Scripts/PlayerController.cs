using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 5;


    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer renderer;

    public GameObject player;
    public static PlayerController singleton;

    float knockbackTime = 0;
    bool knockedBack = false;

    // A value between 0 and 1 that indicates how much the player has control
    float control = 1;
    bool rolling = false;

    Vector2 walkVector;

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

        if (!PauseMenuController.paused)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            if (dir.x < 0)
            {
                renderer.flipX = true;
            }
            else
            {
                renderer.flipX = false;
            }

            if (rolling == false && Input.GetKeyDown(KeyCode.Mouse1))
            {
                rolling = true;
                StartCoroutine("RollStop");
                animator.SetTrigger("Roll");
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        walkVector = new Vector2(horizontal, vertical).normalized;
        if (!knockedBack && !rolling)
        {
            control = control + 0.05f;

            animator.SetFloat("VelocityMagnitude", rb.velocity.magnitude);
            animator.SetFloat("VelocityNormal", rb.velocity.magnitude / walkspeed);

            animator.SetBool("Walking", horizontal != 0 || vertical != 0);
            rb.velocity = Vector2.Lerp(rb.velocity, (walkVector * walkspeed), control);
        }

        if (rolling)
        {
            rb.velocity = walkVector * 7;
        }
    }

    public void Knockback(Vector3 origin, float power = 20)
    {
        knockedBack = true;
        StartCoroutine("KnockbackStun");
        knockbackTime = Time.realtimeSinceStartup;
        rb.velocity = (transform.position - origin).normalized * power;
    }

    IEnumerator KnockbackStun()
    {
        yield return new WaitForSeconds(0.1f);
        knockedBack = false;
    }

    IEnumerator RollStop()
    {
        yield return new WaitForSeconds(0.2f);
        rolling = false;
    }

    //Check when the player meets with items
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Oil"))
        {
            Destroy(other, 0f);
            StatController.totalTime += 20;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other, 0f);
            StatController.bullets += 10 + StatController.bulletCaseUpgradeCount * 3;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Loot"))
        {
            Destroy(other, 0f);

            int level = LevelDesigner.level;
            int lootValue = (int)Random.Range(level, level + 5);
            lootValue += StatController.treasureUpgradeCount * 2;//PlayerPrefs.GetInt("BaseLoot");
            StatController.loot += lootValue;
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("Health"))
        {
            if (StatController.health < StatController.maxHealth)
            {
                Destroy(other, 0f);
                other.gameObject.SetActive(false);
            }

            if (StatController.health + 20 < StatController.maxHealth)
            {
                StatController.health += 20;
            }

            else if (StatController.health < StatController.maxHealth)
            {
                StatController.health = StatController.maxHealth;
            }
        }
    }
}
