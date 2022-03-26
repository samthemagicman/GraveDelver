using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetweenController : MonoBehaviour
{
    public Text gameOver;
    public Text score;
    public Button again;

    public static string fate;

    public float fadeRate;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "You found " + StatController.loot + " Treasures\n";
        
        if (LevelDesigner.level == 1)
        {
            score.text += "in the " + LevelDesigner.level + " level you delved before \n";
        }
        else
        {
            score.text += "in the " + LevelDesigner.level + " levels you delved before \n";
        }
        
        score.text += fate;


        gameOver.color = new Color(255, 0, 0, 0);
        score.color = new Color(255, 255, 255, 0);
        again.image.color = new Color(255, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float transparency = Time.timeSinceLevelLoad / fadeRate;
        gameOver.color = new Color(255, 0, 0, transparency);
        score.color = new Color(255, 255, 255, transparency * 0.7f);
        again.image.color = new Color(255, 0, 0, transparency * 0.5f);
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
}
