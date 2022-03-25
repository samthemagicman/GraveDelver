using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static int highScore;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "High Score:\n" + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
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
