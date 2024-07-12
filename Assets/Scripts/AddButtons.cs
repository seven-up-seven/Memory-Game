using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private GameObject btn;

    public int amount; 
    
    private void Awake()
    {
        for(int i=0; i<amount; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false); 
        }
    }
}
