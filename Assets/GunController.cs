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

    // Start is called before the first frame update
    void Start()
    {
        swordRenderer = sword.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate sword
        sword.transform.localPosition = positionOffset;// new Vector3(0, 0, 0);
        Vector3 pos = Camera.main.WorldToScreenPoint(sword.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        if (dir.x < 0)
        {
            //renderer.flipX = true;
            swordRenderer.flipY = false;
            //angle = angle + 180;
        }
        else
        {
            //renderer.flipX = false;
            swordRenderer.flipY = true;
        }
        sword.transform.rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
        sword.transform.localPosition += sword.transform.right * outwardOffset;
    }
}
