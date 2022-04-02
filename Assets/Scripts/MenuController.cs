﻿using System.Collections;
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
    public Text clearText;

    public float fadeRate;

    public bool verified;

    // Start is called before the first frame update
    void Start()
    {
        verified = false;

        //Set starting values for a new game
        if (!PlayerPrefs.HasKey("MaxHealth"))
        {
            PlayerPrefs.SetInt("MaxHealth", 100);
        }
        if (!PlayerPrefs.HasKey("StartTime"))
        {
            PlayerPrefs.SetFloat("StartTime", 180);
        }
        if (!PlayerPrefs.HasKey("StartBullets"))
        {
            PlayerPrefs.SetInt("StartBullets", 20);
        }
        if (!PlayerPrefs.HasKey("BaseLoot"))
        {
            PlayerPrefs.SetInt("BaseLoot", 5);
        }

        //Set Progress Text
        if (PlayerPrefs.HasKey("Wealth"))
        {
            scoreText.text = "Wealth:\n" + PlayerPrefs.GetInt("Wealth");
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

        //Set title to transparent
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
        LevelDesigner.level = 1;

        StatController.totalTime = PlayerPrefs.GetFloat("StartTime");
        StatController.health = PlayerPrefs.GetInt("MaxHealth");
        StatController.bullets = PlayerPrefs.GetInt("StartBullets");

        SceneManager.LoadScene("Vendor");
    }

    //Go to the instructions
    public void Instruction()
    {
        SceneManager.LoadScene("Instructions");
    }

    //Clear progress
    public void Clear()
    {
        if (verified)
        {
            PlayerPrefs.DeleteAll();
            scoreText.text = "";
            levelText.text = "";
            clearText.text = "All Gone!";
        }
        else
        {
            clearText.text = "Are you sure?";
            verified = true;
        }
    }

    //Exit Game
    public void ExitGame()
    {
        Application.Quit();
    }
}
