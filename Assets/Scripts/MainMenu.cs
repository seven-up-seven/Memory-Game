using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void EnterLeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
    public void EnterSetting()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
