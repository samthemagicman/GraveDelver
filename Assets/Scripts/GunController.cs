using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject player;
    SpriteRenderer swordRenderer;
    public GameObject sword;
    public float angleOffset = 177.3f;
    public float outwardOffset = -0.085f;
    public Vector2 positionOffset = Vector2.zero;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        swordRenderer = sword.GetComponentInChildren<SpriteRenderer>();
        animator = sword.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Fire");
        }

        //Rotate sword
        sword.transform.localPosition = positionOffset;// new Vector3(0, 0, 0);
        Vector3 pos = Camera.main.WorldToScreenPoint(sword.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dir.x < 0)
        {
            //renderer.flipX = true;
            swordRenderer.flipY = false;
            //angle = angle + 180;
            angle = angle + 12;
        }
        else
        {
            //renderer.flipX = false;
            swordRenderer.flipY = true;
            angle = angle - 12;
        }
        sword.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        sword.transform.localPosition += sword.transform.right * outwardOffset;
    }
}
