using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    void Unpause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void TogglePause()
    {
        if (paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void QuitToMainMenu()
    {
        Unpause();
        EndController.lootCount = StatController.loot;
        string[] fateOptions = new string[3] {"you gave up.",
                                                    "you couldn't take it anymore.",
                                                    "you fell to your own despair." };

        EndController.fate = fateOptions[(int)Random.Range(0, 3)];
        SceneManager.LoadScene("Game Over");
    }
}
