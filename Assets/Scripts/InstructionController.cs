using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionController : MonoBehaviour
{
    public Text handbook;
    public Text commentary;

    public GameObject prev;
    public GameObject next;

    string[] pages;
    string[] writing;
    int pageNum;

    // Start is called before the first frame update
    void Start()
    {
        pages = new string[7];
        writing = new string[7];
        pageNum = 0;

        pages[0] = "The catacombs are a sprawling maze of underground tombs.\n\n"
                    + "They hold the bones and possessions of the dead.\n"
                    + "Free for the taking if ignore the moral qualms.";
        writing[0] = "Some say there is no bottom.\n"
                    + "Some say there is a gate to Hell somewhere.";

        pages[1] = "Use arrow keys or ASDW to move.\n\n\n"
                    + "Right click to sprint, if only for a short distance.";
        writing[1] = "Don’t get cornered in. You’re faster than they are.";

        pages[2] = "You have a gun to shoot with.\n\n"
                    + "Use your mouse to aim and left-click to shoot.\n"
                    + "You can only shoot if you have bullets.\n"
                    + "You can pick up bullets dropped by others by walking over them.";
        writing[2] = "They take four bullets to kill, but you'll never kill them all.";

        pages[3] = "Always carry a lantern with you.\n\n"
                    + "You can always see how much time you have left in your lantern.\n"
                    + "You can pick up bottles of oil for some extra time and light.";
        writing[3] = "There is nothing but death in the darkness.";

        pages[4] = "Red Chests hold the possessions of those buried here.\n\n"
                    + "Collect as many of these chests as you can.\n"
                    + "It is the reason you are here.";
        writing[4] = "There's always more to take, but it will never be enough.";

        pages[5] = "If you are injured, you can find red hearts to heal yourself up.\n\n\n"
                    + "You can always look in your heart to see how close to death you are.";
        writing[5] = "You never get used to their teeth.";

        pages[6] = "Each level of the catacomb has a staircase down to the next level.\n"
                    + "They will always be found in nooks on the edges.\n\n"
                    + "The farther down you go, the more loot and less oil you will find.";
        writing[6] = "I've never found a bottom.";
    }

    // Update is called once per frame
    void Update()
    {
        handbook.text = pages[pageNum];
        commentary.text = writing[pageNum];

        if (pageNum == 0)
        {
            prev.SetActive(false);
        }
        else
        {
            prev.SetActive(true);
        }

        if (pageNum == 6)
        {
            next.SetActive(false);
        }
        else
        {
            next.SetActive(true);
        }
    }

    //Turn the page
    public void NextPage()
    {
        pageNum++;
    }

    public void LastPage()
    {
        pageNum--;
    }

    //Return to the main menu
    public void Menu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
