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

    public Button startButton;

    //Elements in Options
    public Button menuButton;
    public Button resetButton;
    public Button exitButton;

    public Text credits;
    public Image redBack;

    public Text clearText;

    //Changing between handbook & menu
    public Canvas handbook;
    public GameObject menu;

    public float fadeRate;

    public bool verified;

    // Start is called before the first frame update
    void Start()
    {
        
        verified = false;

        //Set starting values for a new game
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
            scoreText.text = "Wealth:\n" + StatController.wealth;

            resetButton.enabled = true;
            resetButton.transform.localScale = new Vector3(1, 1, 0);

        }
        else
        {
            scoreText.text = "";

           // startButton.enabled = false;
            //startButton.transform.localScale = new Vector3(0, 0, 0);
        }

        if (PlayerPrefs.HasKey("Lowest Level"))
        {
            levelText.text = "Lowest Level\nSurvived:\n" + PlayerPrefs.GetInt("Lowest Level");
            
        }
        else
        {
            levelText.text = "";
        }

        StatController.ResetToDefault(); // Load all stats properly

        //Hide the options menu
        HideOptions();

        //Set title to transparent
        titleText.color = new Color(255,255,255,0);
    }

    public void IncreaseWealth()
    {
        StatController.wealth += 10;
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
        SceneManager.LoadScene("Vendor");
    }

    //Go to the instructions
    public void Instruction()
    {
        startButton.enabled = true;
        startButton.transform.localScale = new Vector3(1, 1, 0);

        Canvas menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;

        Canvas bookCanvas = handbook.GetComponent<Canvas>();
        bookCanvas.enabled = true;
    }

    //Show Options Menu
    public void ShowOptions()
    {
        menuButton.enabled = true;
        menuButton.transform.localScale = new Vector3(1, 1, 0);

        if (PlayerPrefs.HasKey("Wealth"))
        {
            resetButton.enabled = true;
            resetButton.transform.localScale = new Vector3(1, 1, 0);
        }

        exitButton.enabled = true;
        exitButton.transform.localScale = new Vector3(1, 1, 0);

        credits.enabled = true;
        credits.transform.localScale = new Vector3(1, 1, 0);

        redBack.enabled = true;
        redBack.transform.localScale = new Vector3(1, 1, 0);
    }

    //Hide Options Menu
    public void HideOptions()
    {
        menuButton.enabled = false;
        menuButton.transform.localScale = new Vector3(0, 0, 0);

        resetButton.enabled = false;
        resetButton.transform.localScale = new Vector3(0, 0, 0);

        exitButton.enabled = false;
        exitButton.transform.localScale = new Vector3(0, 0, 0);

        credits.enabled = false;
        credits.transform.localScale = new Vector3(0, 0, 0);

        redBack.enabled = false;
        redBack.transform.localScale = new Vector3(0, 0, 0);
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

            //Set starting values for a new game
            PlayerPrefs.SetFloat("LanternUpgradeCount", 0);
            PlayerPrefs.SetFloat("HealthUpgradeCount", 0);
            if (!PlayerPrefs.HasKey("StartBullets"))
            {
                PlayerPrefs.SetInt("StartBullets", 20);
            }
            if (!PlayerPrefs.HasKey("BaseLoot"))
            {
                PlayerPrefs.SetInt("BaseLoot", 0);
            }
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
