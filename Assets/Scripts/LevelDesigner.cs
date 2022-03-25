using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelDesigner : MonoBehaviour
{
    public GameObject bullets;
    public GameObject health;
    public GameObject loot;
    public GameObject oil;

    public static int level;

    // Start is called before the first frame update
    void Start()
    {
        
        for (float x = -2; x < 3; x++)
        {
           for (float y = 0; y < 3; y++)
            {
                Vector3 position = new Vector3(x * 14, y * 14 + 2f, 0f);
                CreatePickup(position);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Create a random pickup at the given location
    void CreatePickup(Vector3 position)
    {
        int pickup = Random.Range(0, 100);

        if (pickup < 25)
        {
            Instantiate(bullets, position, Quaternion.identity);
        }
        else if (pickup < 50)
        {
            Instantiate(health, position, Quaternion.identity);
        }
        else if (pickup < 3 * level + 57)
        {
            Instantiate(loot, position, Quaternion.identity);
        }
        else
        {
            Instantiate(oil, position, Quaternion.identity);
        }
    }
}

/*Grid
X = -28, -14, 0, 14, 28 (14i)
Y = 2, 16, 30 (2 + 14i)
*/
