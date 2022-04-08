using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VendorController : MonoBehaviour
{

    public Text dialogue;

    public int bulletCost;
    public int bulletCaseCost;
    public int armorCost;
    public int oilCost;
    public int bloodCost;
    public int lanternCost;
    public int mapCost;

    public Button bulletButton;
    public Button bulletCaseButton;
    public Button armorButton;
    public Button oilButton;
    public Button bloodButton;
    public Button lanternButton;
    public Button mapButton;

    public GameObject head;

    public bool infiniteLoot;


    float mouseY;
    public float mouseX;

    int bulletsBought = 0;
    int armorBought = 0;
    int oilBought = 0;

    // Start is called before the first frame update
    void Start()
    {
        LevelDesigner.level = 0;
        //LevelDesigner.startingTime = PlayerPrefs.GetFloat("StartTime");

        //StatController.totalTime = PlayerPrefs.GetFloat("StartTime");
        StatController.bullets = PlayerPrefs.GetInt("StartBullets");

        //Set the price tags
        UpdateItemPricesAndQuantity();

        infiniteLoot = false;

        Time.timeScale = 0;
    }

    void UpdateItemPricesAndQuantity()
    {
        bulletButton.GetComponentInChildren<Text>().text = "$" + bulletCost + "("+ bulletsBought + ")";
        armorButton.GetComponentInChildren<Text>().text = "$" + armorCost + "("+ armorBought + ")";
        oilButton.GetComponentInChildren<Text>().text = "$" + oilCost + "("+ oilBought + ")";
        bloodButton.GetComponentInChildren<Text>().text = "$" + bloodCost + "("+StatController.healthUpgradeCount+")";
        lanternButton.GetComponentInChildren<Text>().text = "$" + lanternCost + "(" + StatController.lanternUpgradeCount + ")";
        mapButton.GetComponentInChildren<Text>().text = "$" + mapCost + "(" + StatController.treasureUpgradeCount + ")";
        bulletCaseButton.GetComponentInChildren<Text>().text = "$" + mapCost + "(" + StatController.bulletCaseUpgradeCount + ")";
    }

    public void OnButtonEnter(GameObject btn)
    {
        if (btn == bulletButton.gameObject)
        {
            string response = "Oh, you need some bullets?\nThey're a case of ten bullets for $" + bulletCost + ".";

            if (StatController.wealth < bulletCost)
            {
                response += "\nBut it looks like you don't have enough for even one case.";
            }

            dialogue.text = response;
        } else if (btn == armorButton.gameObject)
        {
            string response = "Ah, you want some armor to protect yourself from harm?\nFor $" + armorCost + ", I can give you thirty extra STARTING health.";

            if (StatController.wealth < armorCost)
            {
                response += "\nBut it looks like you don't have enough for that.";
            }

            dialogue.text = response;
        } else if (btn == oilButton.gameObject)
        {
            string response = "So, you're looking for more lamp oil for more time.\nIt's $" + oilCost + " for one minute's worth of oil.";

            if (StatController.wealth < oilCost)
            {
                response += "\nOh, but it looks like that's a bit out of your price range.";
            }

            dialogue.text = response;
        } else if (btn == bloodButton.gameObject)
        {
            string response = "That is Ghoul Blood. It will inure you against their pain.\n For $" + bloodCost + ", it will increase your health by ten for all subsequent delves.";

            if (StatController.wealth < bloodCost)
            {
                response += "\nBut it looks like this rarity is not within your budget.";
            }

            dialogue.text = response;
        } else if (btn == lanternButton.gameObject)
        {
            string response = "For $" + lanternCost + " I can refine your lantern.\nIt will burn a minute longer for all of your future delves.";

            if (StatController.wealth < lanternCost)
            {
                response += "\nBut it looks like that is outside of your price range.";
            }

            dialogue.text = response;
        } else if (btn == mapButton.gameObject)
        {
            string response = "For $" + mapCost + " I can give a map to a richer part of the catacombs.\nAll chests will be worth $2 more on delves there.";

            if (StatController.wealth < mapCost)
            {
                response += "\nBut looking at how much you have, that'll have to wait.";
            }

            dialogue.text = response;
        }
        else if (btn == bulletCaseButton.gameObject)
        {
            string response = "Ah, a very good upgrade indeed.\nAll bullet cases will have 3 more bullets on future delves.";

            if (StatController.wealth < mapCost)
            {
                response += "\nBut it looks like you can't afford it.";
            }

            dialogue.text = response;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Choose Dialogue Based on Mouse Position
        mouseX = Input.mousePosition.x/ Screen.width;
        mouseY = Input.mousePosition.y/ Screen.height;

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
        bulletsBought++;
        UpdateItemPricesAndQuantity();
        if (StatController.wealth >= bulletCost)
        {
            StatController.wealth -= bulletCost;
            StatController.bullets += 10;
        }

    }

    //Buy some bullets
    public void BuyBulletCase()
    {
        if (StatController.wealth >= bulletCaseCost)
        {
            StatController.wealth -= bulletCaseCost;
            StatController.bulletCaseUpgradeCount += 1;
        }
        UpdateItemPricesAndQuantity();
    }

    //Buy some health
    public void BuyArmor()
    {
        armorBought++;
        if (StatController.wealth >= armorCost || infiniteLoot)
        {
            StatController.wealth -= armorCost;
            StatController.health += 30;
        }

        UpdateItemPricesAndQuantity();
    }

    //Buy some more time
    public void BuyOil()
    {
        oilBought++;
        if (StatController.wealth >= oilCost || infiniteLoot)
        {
            StatController.wealth -= oilCost;
            StatController.totalTime += 60;
        }
        UpdateItemPricesAndQuantity();
    }

    //Upgrade Loot
    public void BuyMap()
    {
        if (StatController.wealth >= mapCost || infiniteLoot)
        {
            StatController.wealth -= mapCost;

            StatController.treasureUpgradeCount += 1;
            int currentMap = PlayerPrefs.GetInt("BaseLoot");
            PlayerPrefs.SetInt("BaseLoot", currentMap + 2);
        }
        UpdateItemPricesAndQuantity();
    }

    //Upgrade Health
    public void BuyBlood()
    {
        if (StatController.wealth >= bloodCost || infiniteLoot)
        {
            StatController.wealth -= bloodCost;
            StatController.healthUpgradeCount += 1;
        }
        UpdateItemPricesAndQuantity();
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
        UpdateItemPricesAndQuantity();
    }
}
