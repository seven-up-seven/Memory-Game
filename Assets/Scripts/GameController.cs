using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
//using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage; 

    public List<Button> btns = new List<Button>();
    //Kho chua' hinh` anh
    public Sprite[] imagePool;
    //Kho chua' cac' hinh` anh dc chon. tu` kho chua' anh
    public List<Sprite> selectedImagePool = new List<Sprite>();

    private bool firstGuess, secondGuess;
    //private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessName, secondGuessName;
    private AudioManager audioManager;
    private Button CheatButton;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private TextMeshProUGUI timerText;
    float elapsedTime = 0;
    bool timerActive = true;
    [SerializeField] private TextMeshProUGUI timeResult;
    [SerializeField] private GameObject timer;
   
    private void Awake()
    {
  
       imagePool = Resources.LoadAll<Sprite>("Fruits");
       audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
       CheatButton = GameObject.FindWithTag("Cheat").GetComponent<Button>();

    }
    void Start ()
    {
        GetButtons();
        AddClickListener();
        AddGamePuzzlesImg();
        shuffle(selectedImagePool); 
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
        CheatButton.onClick.AddListener(() => ShowAll());
    }
    public void PickAPuzzle()
    {
        //Ten cua button vua` dc chon.(cx la` index cua no', do dat ten tu 0 -> amount)
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Neu chua chon. thi` cho chon.
        if (!firstGuess)
        {
            //k cho chon nx
            firstGuess = true;
            //parse dung` de chuyen string -> int
            firstGuessIndex = int.Parse(name);
            //luu tru ten cua hinh` anh de so sanh'
            firstGuessName = selectedImagePool[firstGuessIndex].name;
            //Gan' hinh` anh vao` button khi bam vao` -->ban than button k co hinh anh
            btns[firstGuessIndex].image.sprite = selectedImagePool[firstGuessIndex];
            audioManager.PlaySFX(audioManager.FlipCard);
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(name);
            secondGuessName = selectedImagePool[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = selectedImagePool[secondGuessIndex];
            audioManager.PlaySFX(audioManager.FlipCard);
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
            selectedImagePool.Add(imagePool[index]);
            index++; 
        }
    }
    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstGuessName == secondGuessName && firstGuessIndex != secondGuessIndex)
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            audioManager.PlaySFX(audioManager.Correct);
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
        firstGuess = secondGuess = false; 
    }
    void CheckIfGameFinished()
    {
        countCorrectGuesses++;
        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            audioManager.PlaySFX(audioManager.Finished);
            //Hien win menu
            WinMenu.SetActive(true);
            //Ngung timer va gan' time vao` win menu
            timerActive = false;
            timeResult.text = timerText.text;
            timer.SetActive(false);
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

    void ShowAll()
    {
        for(int i = 0; i < btns.Count; i++)
        {
            btns[i].image.sprite = selectedImagePool[i];
        }
    }

    public void WinGame()
    {
        countCorrectGuesses = gameGuesses - 1;
        CheckIfGameFinished();
    }

    private void Update()
    {
        if(timerActive)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    

}
