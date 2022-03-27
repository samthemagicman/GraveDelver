using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //public static int highScore;

    public Text scoreText;
    public Text levelText;
    public Text titleText;

    public float fadeRate;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("High Score"))
        {
            scoreText.text = "High Score:\n" + PlayerPrefs.GetInt("High Score");
        }
        else
        {
            scoreText.text = "";
        }

        if (PlayerPrefs.HasKey("Lowest Level"))
        {
            levelText.text = "Lowest Level\nSurvived:\n" + PlayerPrefs.GetInt("Lowest Level");
        }
        else
        {
            levelText.text = "";
        }

        titleText.color = new Color(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        float transparency = Time.timeSinceLevelLoad/fadeRate;
        titleText.color = new Color(255, 255, 255, transparency);

    }

    //Load the game with the starting stats
    public void LoadGame()
    {
        SceneManager.LoadScene("RandomMap");

        StatController.health = 100;
        StatController.bullets = 20;
        StatController.loot = 0;
        StatController.totalTime = 300;

        LevelDesigner.level = 1;
        
    }

    //Go to the instructions
    public void Instruction()
    {
        SceneManager.LoadScene("Instructions");
    }
}
