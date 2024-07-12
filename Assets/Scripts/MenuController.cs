using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button levelButton;
    private int amount; 
    private void Start()
    { 
        levelButton.onClick.AddListener(LoadScene); 
    }
    void LoadScene()
    {
        amount = int.Parse(levelButton.name);
        if (amount == 10 )
        {
            SceneManager.LoadScene("Scene10");
        }
        else if (amount == 15)
        {
            SceneManager.LoadScene("Scene15");
        }
        
    }
}
