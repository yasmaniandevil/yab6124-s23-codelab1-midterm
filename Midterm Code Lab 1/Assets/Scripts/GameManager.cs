using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //singleton
    public static GameManager Instance;

    //how long the game is
    public int gameLength = 60;
    public float timer = 0;

    //public TextMeshPro displayText;

    //made it UI instead of object because 3D Space, UI has to follow player camera
    public TextMeshProUGUI _counter;
    public TextMeshProUGUI _scoreText;

    //create a bool set it to true
    bool inGame = true;

    //set int score to 0
    int score = 0;

    //create a property
    //gets the score variable
    //sets the score var to the value of it
    public int Score
    {
        get
        {
            return score;
            
        }
        set
        {
            score = value;
            Debug.Log("value: "+ (value));
            Debug.Log("score: " + (score));
        }
    }

    //create a list called highScores
    public List<int> highScores = new List<int>();

    //create a string for the file path
    private string FILE_PATH;
    //the folder the file will be in
    private const string FILE_DIR = "/Data/";
    //the name of the file
    private const string FILE_NAME = "highScores.txt";
    
    //singleton
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //timer starts at 0
        timer = 0;

        //the file path/directory is the folder and file name
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
    }

    // Update is called once per frame
    void Update()
    {
        //if the bool we made which is true
        if (inGame)
        {
            //make the timer the same speed as time.deltatime
            timer += Time.deltaTime;
            //the text that will display will be the timer: how much longer the game is minus the timer
            _counter.text = "Timer: " + (gameLength - (int)timer);
            _scoreText.text = "Legs Closed: " + (score);
        }

        //if the timer is greater than or equal to game length and inGame is set to true
        if (timer >= gameLength && inGame)
        {
            //then set inGame to false
            inGame = false;
            //load end scene
            SceneManager.LoadScene("EndScreen");
            //update the high scores
            UpdateHighScores();
        }

        //delete the scores in file text
        if (Input.GetKeyDown(KeyCode.R))
        {
            File.Delete(FILE_PATH);
        }
    }

    void UpdateHighScores()
    {
        //take the high scores out of the file and put them in the highscores list
        if (highScores.Count == 0) //will check size of the list, so if there is nothing on the list
        {
            //if we already have high scores
            if (File.Exists(FILE_PATH))
            {
                //get the highscores from the file and save it as a string after its read
                string fileContents = File.ReadAllText(FILE_PATH);

                //split the string into an array
                string[] fileSplit = fileContents.Split('\n');

                //go through all the strings that are numbers
                for (int i = 1; i < fileSplit.Length - 1; i++)
                {
                    //add the number (converted from a string) to highScores list
                    highScores.Add(Int32.Parse(fileSplit[i]));
                }
            }
            else
            {
                //add a placeholder to the list, which would be zero
                highScores.Add(0);
            }
        }

        //insert our score into the high scores list, if its large enough
        //loop through all elements of the array
        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i] < Score)//if its less than the Score
            {
                highScores.Insert(i, Score);//then insert it
                break;//found what youre looking for, stop the loop
            }
        }

        //if we have more than 5 high scores
        if (highScores.Count > 5)
        {
            //cut it to 5 high scores
            highScores.RemoveRange(5, highScores.Count - 5);
        }

        //make a string of all our high scores
        string highScoreStr = "High Scores: \n";

        
        for (int i = 0; i < highScores.Count; i++)//size of list
        {
            highScoreStr += highScores[i] + "\n";//save all info into var highScoreStr
        }

        //display high scores
        _counter.text = highScoreStr;
        
        File.WriteAllText(FILE_PATH, highScoreStr);
    }
}
