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

    public Animator animator;

    public float fireRatePerSecond = 1f;
    float lastFire;

    // Start is called before the first frame update
    void Start()
    {
        swordRenderer = sword.GetComponentInChildren<SpriteRenderer>();
        animator = sword.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - lastFire >= fireRatePerSecond && Input.GetButton("Fire1"))
        {
            FireBullet();
            lastFire = Time.realtimeSinceStartup;
            animator.SetTrigger("Fire");
        }

        //Rotate sword
        sword.transform.localPosition = positionOffset;// new Vector3(0, 0, 0);
        Vector3 pos = Camera.main.WorldToScreenPoint(sword.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dir.x < 0)
        {
            //swordRenderer.flipY = false;
            swordRenderer.transform.localScale = new Vector3(swordRenderer.transform.localScale.x, Mathf.Abs(swordRenderer.transform.localScale.y), swordRenderer.transform.localScale.z);
            //angle = angle + 180;
            angle = angle + 12;
        }
        else
        {
            //swordRenderer.flipY = true;
            swordRenderer.transform.localScale = new Vector3(swordRenderer.transform.localScale.x, -Mathf.Abs(swordRenderer.transform.localScale.y), swordRenderer.transform.localScale.z);
            angle = angle - 12;
        }
        sword.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        sword.transform.localPosition += sword.transform.right * outwardOffset;
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletPlacement.transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -bulletPlacement.transform.right * 10;
        
    }
}
