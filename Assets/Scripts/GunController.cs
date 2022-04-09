using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPlacement;
    public GameObject player;
    SpriteRenderer swordRenderer;
    public GameObject sword;
    public float angleOffset = 177.3f;
    public float outwardOffset = -0.085f;
    public Vector2 positionOffset = Vector2.zero;

    AudioSource audioSource;
    public AudioClip fireSound;

    public Animator animator;

    float lastTimeFireButtonDown = 0;
    public float fireRatePerSecond = 1f;
    float lastFire;

    float currentAngle;

    float accuracy = 5;

    // Start is called before the first frame update
    void Start()
    {
        swordRenderer = sword.GetComponentInChildren<SpriteRenderer>();
        animator = sword.GetComponentInChildren<Animator>();
        audioSource = sword.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuController.paused)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                lastTimeFireButtonDown = Time.realtimeSinceStartup; // Provides a small buffer for input
            }

            //Check Reload Time
            if (Time.realtimeSinceStartup - lastFire >= fireRatePerSecond && (Time.realtimeSinceStartup - lastTimeFireButtonDown < 0.1f))
            {
                //Check bullets
                if (StatController.bullets > 0)
                {
                    FireBullet();
                    lastFire = Time.realtimeSinceStartup;
                    animator.SetTrigger("Fire");
                }
            
            }

            //Rotate sword
            sword.transform.localPosition = positionOffset;// new Vector3(0, 0, 0);
            Vector3 pos = Camera.main.WorldToScreenPoint(sword.transform.position);
            Vector3 dir = Input.mousePosition - pos;
            currentAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (dir.x < 0)
            {
                //swordRenderer.flipY = false;
                swordRenderer.transform.localScale = new Vector3(swordRenderer.transform.localScale.x, Mathf.Abs(swordRenderer.transform.localScale.y), swordRenderer.transform.localScale.z);
                //angle = angle + 180;
                currentAngle = currentAngle;
            }
            else
            {
                //swordRenderer.flipY = true;
                swordRenderer.transform.localScale = new Vector3(swordRenderer.transform.localScale.x, -Mathf.Abs(swordRenderer.transform.localScale.y), swordRenderer.transform.localScale.z);
                currentAngle = currentAngle;
            }
            sword.transform.rotation = Quaternion.AngleAxis(currentAngle + 180, Vector3.forward);
            sword.transform.localPosition += sword.transform.right * outwardOffset;

        }
    }

    void FireBullet()
    {
        float p = Random.Range(0.7f, 1.3f);
        audioSource.pitch = p;
        audioSource.PlayOneShot(fireSound);

        //Decrement Bullets
        StatController.bullets--;

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletPlacement.transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.velocity = -bulletPlacement.transform.right * 10;
        bullet.transform.rotation = Quaternion.AngleAxis(currentAngle + Random.Range(-accuracy, accuracy), Vector3.forward);
        rb.velocity = bullet.transform.right * 20;
        //bullet.transform.Rotate(bulletPlacement.transform.right, 45f, Space.World);
        //Debug.Log(new Vector3(Mathf.Sin(Mathf.PI/4), Mathf.Cos(Mathf.PI / 4), 0));
        
    }
}
