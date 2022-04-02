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
    public GameObject obstacle;
    public GameObject enemy;

    public static int level;
    public static float startingTime;



    // Start is called before the first frame update
    void Start()
    {

        //Level Testing
        if (level == 0)
        {
            level = 7;
        }
        else if (level != 1)
        {
            level++;
        }

        //Set time here to avoid a glitch
        if (startingTime != 0)
        {
            StatController.totalTime = startingTime;
            startingTime = 0;
        }

        //Fill Rooms
        for (float x = -2; x < 3; x++)
        {
           for (float y = 0; y < 3; y++)
            {
                //A room with more items has more enemies
                int fullness = (int)Random.Range(0, 3);

                for (int i = 0; i < fullness; i++)
                {
                    float xRand = Random.Range(-3, 4);
                    float yRand = Random.Range(-3, 4);
                    Vector3 position = new Vector3(x * 14 + xRand, y * 14 + 2f + yRand, 0f);

                    CreatePickup(position);
                }

                

                int maxEnemies = (fullness + 1) * (level / 5 + 1);
                int Enemies = (int)Random.Range(fullness, maxEnemies + 1);


                for (int i = 0; i < Enemies; i++)
                {
                    float xRand = Random.Range(-3, 4);
                    float yRand = Random.Range(-3, 4);
                    Vector3 position = new Vector3(x * 14 + xRand, y * 14 + 2f + yRand, 0f);

                    CallToFeast(position);
                }





                int numObstacles = Random.Range(0, 3);

                for (; numObstacles > 0; numObstacles--)
                {
                    float xRand = Random.Range(-2, 3);
                    float yRand = Random.Range(-2, 3);
                    Vector3 position = new Vector3(x * 14 + xRand, y * 14 + 2f + yRand, 0f);
                    CreateObstacle(position);
                }
            }
        }

        //Pickups in Nooks
        //Top & Bottom
        for (int x = -28; x < 30; x += 14)
        {
            Vector3 position = new Vector3(x, 38, 0);
            CreatePickup(position);

            if (x != 0)
            {
                position = new Vector3(x, -6, 0);
                CreatePickup(position);
            }
        }

        //Sides
        for (int y = 2; y <31; y += 14)
        {
            Vector3 position = new Vector3(36, y, 0);
            CreatePickup(position);

            position = new Vector3(-36, y, 0);
            CreatePickup(position);
        }

        //Obstacles in Doorways
        //Tier 1
        int where = (int)Random.Range(0, 5);
        Tier1Obstacle(where);

        //Tier 2
        where = (int)Random.Range(0, 5);
        Tier2Obstacle(where);

        where = (int)Random.Range(where + 2, 7);
        Tier2Obstacle(where);

        //Tier 3
        where = (int)Random.Range(3, 6);
        Tier3Obstacle(where);

        where = (int)Random.Range(0, where - 2);
        Tier3Obstacle(where);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Create a random pickup at the given location. 1/ chance for pickup.
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
        else if (pickup < 4 * level + 56)
        {
            Instantiate(loot, position, Quaternion.identity);
        }
        else
        {
            Instantiate(oil, position, Quaternion.identity);
        }
        
    }

    void CreateObstacle(Vector3 position)
    {
        Instantiate(obstacle, position, Quaternion.identity);
    }

    void CallToFeast(Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
    }

    //Takes a number from 0 to 3
    void Tier1Obstacle(int w)
    {
        float x;
        float y;
        if (w < 3 && w > 0)
        {
            y = 16;
        }
        else
        {
            y = 9;
        }

        x = w * 7f - 14;

        if (w > 1)
        {
            x += 7;
        }

        Vector3 position = new Vector3(x, y, 0);
        CreateObstacle(position);
    }

    //Takes a number from 0 to 6
    void Tier2Obstacle(int w)
    {
        float x;
        float y;

        if (w > 1 && w < 5)
        {
            y = 23;
        }
        else if (w == 1 || w == 5)
        {
            y = 16;
        }
        else
        {
            y = 2;
        }

        if (w > 1 && w < 5)
        {
            x = 14f * (w - 3);
        }
        else if (w < 2)
        {
            x = -21;
        }
        else
        {
            x = 21;
        }

        Vector3 position = new Vector3(x, y, 0);
        CreateObstacle(position);

    }

    //Takes a number from 0 to 5
    void Tier3Obstacle(int w)
    {
        float x;
        float y;

        if (w == 0 || w == 5)
        {
            y = 23;
        }
        else
        {
            y = 30;
        }


        if (w > 0 && w < 5)
        {
            x = 14f * (w - 2.5f);
        }
        else if (w == 0)
        {
            x = -28;
        }
        else
        {
            x = 28;
        }

        Vector3 position = new Vector3(x, y, 0);
        CreateObstacle(position);
    }
}

/*Grid
X = -28, -14, 0, 14, 28 (14i)
Y = 2, 16, 30 (2 + 14i)
*/
