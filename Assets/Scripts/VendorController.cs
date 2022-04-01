using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VendorController : MonoBehaviour
{

    public Text dialogue;

    bool bulletPressed;

    // Start is called before the first frame update
    void Start()
    {
        bulletPressed = false;

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start the game
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("RandomMap");
    }

    //Buy some bullets
    public void BuyBullets()
    {
        if (!bulletPressed)
        {
            string response = "Oh, you need some bullets?\nThey're a case of ten bullets for $20.";

            if (StatController.loot < 20)
            {
                response += "\nBut it looks like you don't have enough for even one case.";
            }

            dialogue.text = response;

            bulletPressed = true;
        }
        else
        {
            if (StatController.loot > 20)
            {
                StatController.loot -= 20;
                StatController.bullets += 10;
            }
            else
            {
                dialogue.text = "A case of ten bullets costs $20.\nYou don't have enough.";
            }
        }

    }
}
