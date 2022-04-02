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
        }
        //Sides
        else if (location > 7) //8 to 10
        {
            float y = 14 * (location - 8) + 2;
            Vector3 position = new Vector3(36, y, 0);
            transform.position = position;
        }
        else //5 to 7
        {
            float y = 14 * (location - 5) + 2;
            Vector3 position = new Vector3(-36, y, 0);
            transform.position = position;
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
            StatController.totalTime = StatController.totalTime - (Time.timeSinceLevelLoad);
            LevelDesigner.level++;
            SceneManager.LoadScene("Between Levels");
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
