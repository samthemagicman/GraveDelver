using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ExitController : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        
        
        //Set Location of Exit
        int location = Random.Range(0, 11);

        //Top
        if (location < 5) //0 to 4
        {
            float x = 14 * (location - 2);
            Vector3 position = new Vector3(x, 38, 0);
            transform.position = position;

            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = rotation;
        }
        //Sides
        else if (location > 7) //8 to 10
        {
            float y = 14 * (location - 8) + 2;
            Vector3 position = new Vector3(36, y, 0);
            transform.position = position;

            Quaternion rotation = Quaternion.Euler(0, 0, -90);
            transform.rotation = rotation;
        }
        else //5 to 7
        {
            float y = 14 * (location - 5) + 2;
            Vector3 position = new Vector3(-36, y, 0);
            transform.position = position;

            Quaternion rotation = Quaternion.Euler(0, 0, 90);
            transform.rotation = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelDesigner.level++;
            float nextTime = StatController.totalTime - (Time.timeSinceLevelLoad);
            SceneManager.LoadScene("Between Levels");
            StatController.totalTime = nextTime;


        }
        else if (other.gameObject.CompareTag("Oil") ||
                other.gameObject.CompareTag("Bullet") ||
                other.gameObject.CompareTag("Health") ||
                other.gameObject.CompareTag("Loot"))
        {
            Destroy(other, 0f);
            other.gameObject.SetActive(false);
        }
    }
}
