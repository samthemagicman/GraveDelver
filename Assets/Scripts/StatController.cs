using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{

    public static float totalTime;
    public static int health;
    public static int bullets;
    public static int loot;

    public Text timeText;
    public Text healthText;
    public Text bulletText;
    public Text lootText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + "";
        bulletText.text = bullets + "";
        lootText.text = loot + "";

        setTimeText();
    }

    void setTimeText()
    {
        int seconds = (int)totalTime - (int)(Time.timeSinceLevelLoad);
        int minutes = seconds / 60;
        seconds = seconds % 60;

        string time = minutes + ":";

        if (seconds < 10)
        {
            time += "0";
        }

        time += seconds;

        timeText.text = time;
    }
}
