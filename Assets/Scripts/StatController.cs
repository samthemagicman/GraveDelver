using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        if (LevelDesigner.level == 0)
        {
            totalTime = 100;
            health = 100;
            bullets = 20;
            loot = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + "";
        bulletText.text = bullets + "";
        lootText.text = loot + "";

        setTimeText();

        if (health == 0)
        {
            string[] fateOptions = new string[3] {"you were torn apart.",
                                                    "you were eaten alive.",
                                                    "you were picked clean." };

            BetweenController.fate = fateOptions[(int)Random.Range(0, 3)];
            SceneManager.LoadScene("Game Over");
        }

        else if (totalTime < Time.timeSinceLevelLoad)
        {
            string[] fateOptions = new string[3] {"you fell to the darkness.",
                                                    "you could only hear your death.",
                                                    "the shadows took you." };

            BetweenController.fate = fateOptions[(int)Random.Range(0, 3)];
            SceneManager.LoadScene("Game Over");
        }
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
