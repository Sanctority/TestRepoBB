using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Instance variables.
    public static GameManager _instance = null;

    // Public variables.


    // Private variables
    private int _highscore;      // This will be used to store the current Highscore of the player.
    private int _endOfLevelScore;   // This will be used to store the score that the player will get at the end of the level.

    private void Awake()
    {
        // Instance of game manager start.
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        // Instance of game manager end.
    }

    private void Start()
    {
        _highscore = PlayerPrefs.GetInt("Highscore");       // Gets the current saved highscore and stores it in the variable.
    }

    public void SaveHighScore(int num)
    {
        if(PlayerPrefs.GetInt("Highscore") < num)
        {
            PlayerPrefs.SetInt("Highscore", num);
            _highscore = num;

            Debug.Log("Score updated to "+ num);
        }
    }

    public int ReturnHighScore()
    {
        return _highscore;
    }
}
