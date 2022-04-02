using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VendorController : MonoBehaviour
{

    public Text dialogue;

    public int bulletCost;
    public int armorCost;
    public int oilCost;

    public Text bulletPriceTag;
    public Text armorPriceTag;
    public Text oilPriceTag;

    bool bulletPressed;
    bool armorPressed;
    bool oilPressed;

    // Start is called before the first frame update
    void Start()
    {
        bulletPressed = false;
        armorPressed = false;
        oilPressed = false;

        bulletPriceTag.text = "$" + bulletCost;
        armorPriceTag.text = "$" + armorCost;
        oilPriceTag.text = "$" + oilCost;


        StatController.loot = PlayerPrefs.GetInt("Wealth");

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start the game
    public void LoadGame()
    {
        PlayerPrefs.SetInt("Wealth", StatController.loot);
        StatController.loot = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("RandomMap");
    }

    void ResetSalesPitch()
    {
        bulletPressed = false;
        armorPressed = false;
        oilPressed = false;
    }

    //Buy some bullets
    public void BuyBullets()
    {
        if (!bulletPressed)
        {
            string response = "Oh, you need some bullets?\nThey're a case of ten bullets for $" + bulletCost + ".";

            if (StatController.loot < bulletCost)
            {
                response += "\nBut it looks like you don't have enough for even one case.";
            }

            dialogue.text = response;

            ResetSalesPitch();
            bulletPressed = true;
        }
        else
        {
            if (StatController.loot >= bulletCost)
            {
                StatController.loot -= bulletCost;
                StatController.bullets += 10;
            }
            else
            {
                dialogue.text = "A case of ten bullets costs $" + bulletCost + ".\nYou don't have enough.";
            }
        }

    }

    //Buy some health
    public void BuyArmor()
    {
        if (!armorPressed)
        {
            string response = "Ah, you want some armor to protect yourself from harm?\nFor $" + armorCost + ", I can give you 30 extra STARTING health.";

            if (StatController.loot < armorCost)
            {
                response += "\nBut it looks like you don't have enough for that.";
            }

            dialogue.text = response;

            ResetSalesPitch();
            armorPressed = true;
        }
        else
        {
            if (StatController.loot >= armorCost)
            {
                StatController.loot -= armorCost;
                StatController.health += 30;
            }
            else
            {
                dialogue.text = "30-health worth of armor costs $" + armorCost + ".\nYou don't have enough.";
            }
        }

    }

    //Buy some more time
    public void BuyOil()
    {
        if (!oilPressed)
        {
            string response = "So, you're looking for more lamp oil for more time.\nIt's $" + oilCost + " for one minute's worth of oil.";

            if (StatController.loot < oilCost)
            {
                response += "\nOh, but it looks like that's a bit out of your price range.";
            }

            dialogue.text = response;

            ResetSalesPitch();
            oilPressed = true;
        }
        else
        {
            if (StatController.loot >= oilCost)
            {
                StatController.loot -= oilCost;
                StatController.totalTime += 60;
            }
            else
            {
                dialogue.text = "A minute more of oil is worth $" + oilCost + ".\nYou don't have enough.";
            }
        }

    }
}
