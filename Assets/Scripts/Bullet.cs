using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    public AudioClip hitWallSound;
    public AudioClip hitEnemySound;
    AudioSource audioSource;
    public GameObject bulletHitParticle;

    float timeCreated;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeCreated = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - timeCreated > 5)
        {
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        GameObject particles = Instantiate(bulletHitParticle, transform.position, transform.rotation);

        IEnemy slimeEnemy;
        collision.gameObject.TryGetComponent<IEnemy>(out slimeEnemy);
        if (slimeEnemy != null)
        {
            slimeEnemy.Knockback(PlayerController.singleton.transform.position);
            slimeEnemy.Damage(25);

            audioSource.PlayOneShot(hitEnemySound);
        } else
        {
            audioSource.PlayOneShot(hitWallSound);
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Light2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1);
    }
}
