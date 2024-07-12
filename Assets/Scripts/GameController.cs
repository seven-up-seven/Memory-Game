using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage; 

    public List<Button> btns = new List<Button>();

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;
    //private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    
   
    private void Awake()
    {
  
       puzzles = Resources.LoadAll<Sprite>("Fruits");

    }
    void Start ()
    {
        GetButtons();
        AddClickListener();
        AddGamePuzzlesImg();
        shuffle(gamePuzzles); 
        gameGuesses = btns.Count / 2;   
    }
        
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++) 
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage; 
        }
    }
    void AddClickListener()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle()); 
        }
    }
    public void PickAPuzzle()
    { 
        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex]; 
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            StartCoroutine(CheckIfThePuzzleMatch()); 
        }
    }
    void AddGamePuzzlesImg()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        {
            if(index == looper/2)
            {
                index = 0; 
            }
            gamePuzzles.Add(puzzles[index]);
            index++; 
        }
    }
    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessPuzzle == secondGuessPuzzle && firstGuessIndex != secondGuessIndex)
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfGameFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage; 
        }
        yield return new WaitForSeconds(0.5f);
        firstGuess = secondGuess = false; 
    }
    void CheckIfGameFinished()
    {
        countCorrectGuesses++;
        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
        }
    }
    void shuffle(List<Sprite> pz)
    {
        for(int i=0; i<pz.Count; i++)
        {
            Sprite temp = pz[i];
            int id = Random.Range(i, pz.Count);
            pz[i] = pz[id];
            pz[id] = temp; 
        }
    }
}
