using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public bool displayWealth = false;
    public bool displayMaxHealth = false;

    public Text timeText;
    public Text healthText;
    public Text bulletText;
    public Text lootText;

    public static StatUI singleton;

    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = StatController.health + "";
        bulletText.text = StatController.bullets + "";
        lootText.text = StatController.loot + "";

        if (displayWealth)
        {
            lootText.text = StatController.wealth + "";
        }

        setTimeText();
    }

    void setTimeText()
    {
        int seconds = (int) StatController.totalTime - (int)(Time.timeSinceLevelLoad);
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
