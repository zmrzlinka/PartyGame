using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public BaseScren screns;
    bool paused = false;
    public PlayerManager PM;
    public bool Pause()
    {
        bool stoped = false;
        if (paused)
        {
            screns.Hide();
            Time.timeScale = 1;
        }
        else
        {
            stoped = true;
            screns.Show();
            Time.timeScale = 0;
        }
        paused = !paused;
        return stoped;
    }
    public void Quit()
    {
        GameData.sceneManager.LoadScene("testing", "MainMenu");
    }
    public void Continue()
    {
        Pause();
        PM.UnPause();
    }
}