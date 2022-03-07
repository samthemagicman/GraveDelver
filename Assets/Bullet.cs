using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletHitParticle;

    float timeCreated;

    void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particles = Instantiate(bulletHitParticle, transform.position, transform.rotation);

        IEnemy slimeEnemy;
        collision.gameObject.TryGetComponent<IEnemy>(out slimeEnemy);
        if (slimeEnemy != null)
        {
            slimeEnemy.Knockback(PlayerController.singleton.transform.position);
            slimeEnemy.Damage(25);
        }
        Destroy(gameObject);
    }
}
