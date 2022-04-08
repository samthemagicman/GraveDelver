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

    public GameObject head;

    public bool infiniteLoot;


    float mouseY;
    public float mouseX;


    // Start is called before the first frame update
    void Start()
    {
        LevelDesigner.level = 0;
        //LevelDesigner.startingTime = PlayerPrefs.GetFloat("StartTime");

        //StatController.totalTime = PlayerPrefs.GetFloat("StartTime");
        StatController.bullets = PlayerPrefs.GetInt("StartBullets");

        //Set the price tags
        bulletPriceTag.text = "$" + bulletCost;
        armorPriceTag.text = "$" + armorCost;
        oilPriceTag.text = "$" + oilCost;
        bloodPriceTag.text = "$" + bloodCost;
        lanternPriceTag.text = "$" + lanternCost;
        mapPriceTag.text = "$" + mapCost;

        infiniteLoot = false;

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        //Choose Dialogue Based on Mouse Position
        mouseX = Input.mousePosition.x/ Screen.width;
        mouseY = Input.mousePosition.y/ Screen.height;


        //Mouse is in button row
        if (mouseY > 0.25 && mouseY < 0.40)
        {
            //Extra Bullets
            if (mouseX > 0.17 && mouseX < 0.23)
            {
                string response = "Oh, you need some bullets?\nThey're a case of ten bullets for $" + bulletCost + ".";

                if (StatController.wealth < bulletCost)
                {
                    response += "\nBut it looks like you don't have enough for even one case.";
                }

                dialogue.text = response;

            }

            //Extra Health
            else if (mouseX > 0.285 && mouseX < 0.345)
            {
                string response = "Ah, you want some armor to protect yourself from harm?\nFor $" + armorCost + ", I can give you thirty extra STARTING health.";

                if (StatController.wealth < armorCost)
                {
                    response += "\nBut it looks like you don't have enough for that.";
                }

                dialogue.text = response;

            }

            //Extra Light
            else if (mouseX > 0.4 && mouseX < 0.46)
            {
                string response = "So, you're looking for more lamp oil for more time.\nIt's $" + oilCost + " for one minute's worth of oil.";

                if (StatController.wealth < oilCost)
                {
                    response += "\nOh, but it looks like that's a bit out of your price range.";
                }

                dialogue.text = response;

            }

            //Increase Max. Health
            else if (mouseX > 0.515 && mouseX < 0.575)
            {
                string response = "That is Ghoul Blood. It will inure you against their pain.\n For $" + bloodCost + ", it will increase your health by ten for all subsequent delves.";

                if (StatController.wealth < bloodCost)
                {
                    response += "\nBut it looks like this rarity is not within your budget.";
                }

                dialogue.text = response;

            }

            //Increase Starting Light
            else if (mouseX > 0.63 && mouseX < 0.69)
            {
                string response = "For $" + lanternCost + " I can refine your lantern.\nIt will burn a minute longer for all of your future delves.";

                if (StatController.wealth < lanternCost)
                {
                    response += "\nBut it looks like that is outside of your price range.";
                }

                dialogue.text = response;


            }

            //Increase Loot Gain
            else if (mouseX > 0.745 && mouseX < 0.805)
            {
                string response = "For $" + mapCost + " I can give a map to a richer part of the catacombs.\nAll chests will be worth $2 more on delves there.";

                if (StatController.wealth < mapCost)
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

        if (mouseX > 0 && mouseX < 1)
        {
            //Turn head towards mouse
            float headPos = 0.35f * (mouseX - 0.5f);
            Vector3 position = new Vector3(headPos, 3.2f, -2.81f);
            head.transform.position = position;
        }
        

    }


    //Start the game
    public void LoadGame()
    {
        //PlayerPrefs.SetInt("Wealth", StatController.loot);
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
        if (StatController.wealth >= bulletCost)
        {
            StatController.wealth -= bulletCost;
            StatController.bullets += 10;
        }

    }

    //Buy some health
    public void BuyArmor()
    {
        if (StatController.wealth >= armorCost || infiniteLoot)
        {
            StatController.wealth -= armorCost;
            StatController.health += 30;
        }


    }

    //Buy some more time
    public void BuyOil()
    {
        if (StatController.wealth >= oilCost || infiniteLoot)
        {
            StatController.wealth -= oilCost;
            StatController.totalTime += 60;
        }
    }

    //Upgrade Loot
    public void BuyMap()
    {
        if (StatController.wealth >= mapCost || infiniteLoot)
        {
            StatController.wealth -= mapCost;

            int currentMap = PlayerPrefs.GetInt("BaseLoot");
            PlayerPrefs.SetInt("BaseLoot", currentMap + 2);
        }
    }

    //Upgrade Health
    public void BuyBlood()
    {
        if (StatController.wealth >= bloodCost || infiniteLoot)
        {
            StatController.wealth -= bloodCost;
            StatController.healthUpgradeCount += 1;
        }
    }

    //Upgrade Lantern
    public void BuyLantern()
    {
        if (StatController.wealth >= lanternCost || infiniteLoot)
        {
            StatController.wealth -= lanternCost;
            StatController.totalTime += 60;
            StatController.lanternUpgradeCount += 1;

            //float currentLantern = PlayerPrefs.GetFloat("StartTime");
            //PlayerPrefs.SetFloat("StartTime", currentLantern + 60);
        }
    }
}
