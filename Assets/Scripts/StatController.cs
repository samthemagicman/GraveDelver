using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class StatController : MonoBehaviour
{
    public static StatController singleton;

    public static float totalTime;
    public static int lanternUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt("LanternUpgradeCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("LanternUpgradeCount", value);
        }
    }

    public static int health;
    //The number of permanent health upgrades the player bought
    public static int healthUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt("HealthUpgradeCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("HealthUpgradeCount", value);
        }
    }
    public static int maxHealth
    {
        get
        {
            return 100 + PlayerPrefs.GetInt("HealthUpgradeCount", 0) * 10;
        }
    }

    public static int bullets;
    public static int bulletCaseUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt("BulletCaseUpgradeCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("BulletCaseUpgradeCount", value);
        }
    }
    public static int treasureUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt("TreasureUpgradeCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TreasureUpgradeCount", value);
        }
    }

    //Loot is the current loot the player has picked up in the delve
    public static int loot;

    //Wealth is the players' saved wealth
    public static int wealth
    {
        get
        {
            return PlayerPrefs.GetInt("Wealth", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Wealth", value);
        }
    }

    public float damageDebounce = 0;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        singleton = this;

        // Load initial stats
        ResetToDefault();
    }

    public static void ResetToDefault()
    {
        totalTime = 180 + lanternUpgradeCount * 60;
        health = maxHealth;
        bullets = 20;
        loot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            health = maxHealth; // fix looping
            string[] fateOptions = new string[3] {"you were torn apart.",
                                                    "you were eaten alive.",
                                                    "you were picked clean." };

            EndController.fate = fateOptions[(int)Random.Range(0, 3)];
            SceneManager.LoadScene("Game Over");
        }

        else if (totalTime < Time.timeSinceLevelLoad)
        {
            totalTime = 1000; // fix looping
            string[] fateOptions = new string[3] {"you fell to the darkness.",
                                                    "you could only hear your death.",
                                                    "the shadows took you." };

            EndController.fate = fateOptions[(int)Random.Range(0, 3)];
            SceneManager.LoadScene("Game Over");
        }
    }

    public void damagePlayer(int dmg) {
        if (Time.realtimeSinceStartup - damageDebounce > 0.2f)
        {
            damageDebounce = Time.realtimeSinceStartup;
            health -= dmg;
        }
    }
}
