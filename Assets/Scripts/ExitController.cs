using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExitController : MonoBehaviour
{
    public Text endText;

    // Start is called before the first frame update
    void Start()
    {
        //Clear Text
        endText.text = "";
        
        //Set Location of Exit
        int location = Random.Range(0, 11);

        //Top
        if (location < 5) //0 to 4
        {
            float x = 14 * (location - 2);
            Vector3 position = new Vector3(x, 38, 0);
            transform.position = position;
        }
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
            //End of Level
            endText.text = "You Win!";
        }
    }
}
