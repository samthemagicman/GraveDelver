using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BetweenController : MonoBehaviour
{
    public Text level;
    public Text lootCount;

    // Start is called before the first frame update
    void Start()
    {
        level.text = "Now Entering Level " + (LevelDesigner.level + 1);

        string loot;
        if (StatController.loot == 0)
        {
            loot = "no";
        }
        else
        {
            loot = StatController.loot + "";
        }

        lootCount.text = "You currently have " + loot + " loot.\n"
                        + "Would you rather keep going or leave with what you have ?";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Continue to the next level
    public void Continue()
    {
        SceneManager.LoadScene("RandomMap");
    }

    //Leave the game with what you have
    public void Leave()
    {
        
        //Track your deeds.
        int wealth = StatController.loot;
        wealth += PlayerPrefs.GetInt("Wealth");
        PlayerPrefs.SetInt("Wealth", wealth);
        

        if (!PlayerPrefs.HasKey("Lowest Level") || LevelDesigner.level > PlayerPrefs.GetInt("Lowest Level"))
        {
            PlayerPrefs.SetInt("Lowest Level", LevelDesigner.level);
        }


        //Track your fate
        string[] fateOptions = new string[3] {"you got out with your life.",
                                                    "you ran like a coward.",
                                                    "you couldn't take it any more." };

        EndController.fate = fateOptions[(int)Random.Range(0, 3)];
        SceneManager.LoadScene("Game Over");
    }
}
