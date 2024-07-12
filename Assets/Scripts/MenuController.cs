using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button levelButton;
    private void Start()
    {
        levelButton.onClick.AddListener(LoadScene); 
    }
    void LoadScene()
    {
        SceneManager.LoadScene(levelButton.name); 
    }
}
