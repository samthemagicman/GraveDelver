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
    public int bloodCost;
    public int lanternCost;
    public int mapCost;


    public Text bulletPriceTag;
    public Text armorPriceTag;
    public Text oilPriceTag;
    public Text bloodPriceTag;
    public Text lanternPriceTag;
    public Text mapPriceTag;

    public float mouseY;
    public float mouseX;


    // Start is called before the first frame update
    void Start()
    {

        bulletPriceTag.text = "$" + bulletCost;
        armorPriceTag.text = "$" + armorCost;
        oilPriceTag.text = "$" + oilCost;
        bloodPriceTag.text = "$" + bloodCost;
        lanternPriceTag.text = "$" + lanternCost;
        mapPriceTag.text = "$" + mapCost;
        

        StatController.loot = PlayerPrefs.GetInt("Wealth");

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.mousePosition.x/ Screen.width;
        mouseY = Input.mousePosition.y/ Screen.height;


        //Mouse is in button row
        if (mouseY > 0.15 && mouseY < 0.35)
        {
            //Extra Bullets
            if (mouseX > 0.15 && mouseX < 0.22)
            {
                string response = "Oh, you need some bullets?\nThey're a case of ten bullets for $" + bulletCost + ".";

                if (StatController.loot < bulletCost)
                {
                    response += "\nBut it looks like you don't have enough for even one case.";
                }

                dialogue.text = response;
            }

            //Extra Health
            else if (mouseX > 0.275 && mouseX < 0.345)
            {
                string response = "Ah, you want some armor to protect yourself from harm?\nFor $" + armorCost + ", I can give you thirty extra STARTING health.";

                if (StatController.loot < armorCost)
                {
                    response += "\nBut it looks like you don't have enough for that.";
                }

                dialogue.text = response;
            }

            //Extra Light
            else if (mouseX > 0.4 && mouseX < 0.47)
            {
                string response = "So, you're looking for more lamp oil for more time.\nIt's $" + oilCost + " for one minute's worth of oil.";

                if (StatController.loot < oilCost)
                {
                    response += "\nOh, but it looks like that's a bit out of your price range.";
                }

                dialogue.text = response;
            }

            //Increase loot gain
            else if (mouseX > 0.525 && mouseX < 0.595)
            {
                string response = "That is Ghoul Blood. It will inure you against their pain.\n For $" + bloodCost + ", it will increase your health by ten for all subsequent delves.";

                if (StatController.loot < bloodCost)
                {
                    response += "\nBut it looks like this rarity is not within your budget.";
                }

                dialogue.text = response;
            }

            //Increase Max Health
            else if (mouseX > 0.65 && mouseX < 0.72)
            {
                string response = "For $" + lanternCost + " I can refine your lantern.\nIt will burn a minute longer for all of your future delves.";

                if (StatController.loot < lanternCost)
                {
                    response += "\nBut it looks like that is outside of your price range.";
                }

                dialogue.text = response;
                
            }

            //Increase Starting Light
            else if (mouseX > 0.775 && mouseX < 0.845)
            {
                string response = "You know, for $" + mapCost + " I can give a map to a richer part of the catacombs.\nAll chests will be worth $2 more on delves there.";

                if (StatController.loot < mapCost)
                {
                    response += "\nBut looking at how much you have, that'll have to wait";
                }

                dialogue.text = response;
            }

            else if (mouseX < 0.12 || mouseX > 0.875)
            {
                ResetSalesPitch();
            }
        }
        else
        {
            ResetSalesPitch();
        }
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
        dialogue.text = "Hello little Delver.\n"
                       + "I see you have come to the catacombs to see what you can steal.\n"
                       + "Perhaps my wares will help you survive.";
    }

    //Buy some bullets
    public void BuyBullets()
    {
        if (StatController.loot >= bulletCost)
        {
            StatController.loot -= bulletCost;
            StatController.bullets += 10;
        }

    }

    //Buy some health
    public void BuyArmor()
    {
        if (StatController.loot >= armorCost)
        {
            StatController.loot -= armorCost;
            StatController.health += 30;
        }


    }

    //Buy some more time
    public void BuyOil()
    {
        if (StatController.loot >= oilCost)
        {
            StatController.loot -= oilCost;
            StatController.totalTime += 60;
        }
    }

    //Upgrade Loot
    public void BuyMap()
    {
        if (StatController.loot >= mapCost)
        {
            StatController.loot -= mapCost;

            float currentMap = PlayerPrefs.GetFloat("BaseLoot");
            PlayerPrefs.SetFloat("BaseLoot", currentMap + 2);
        }
    }

    //Upgrade Lantern
    public void BuyBlood()
    {
        if (StatController.loot >= bloodCost)
        {
            StatController.loot -= bloodCost;
            StatController.health += 10;

            float currentLantern = PlayerPrefs.GetFloat("MaxHealth");
            PlayerPrefs.SetFloat("MaxHealth", currentLantern + 10);
        }
    }

    //Upgrade Lantern
    public void BuyLantern()
    {
        if (StatController.loot >= lanternCost)
        {
            StatController.loot -= lanternCost;
            StatController.totalTime += 60;

            float currentLantern = PlayerPrefs.GetFloat("StartTime");
            PlayerPrefs.SetFloat("StartTime", currentLantern + 60);
        }
    }
}
