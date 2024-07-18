using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinMenu : MonoBehaviour
{
    Scene scene;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void EnterNextLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void EnterMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
