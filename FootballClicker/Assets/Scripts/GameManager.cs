using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Instance variables.
    public static GameManager _instance = null;

    // Public variables.


    // Private variables
    private int _highscore;      // This will be used to store the current Highscore of the player.

    private void Awake()
    {
        // Instance of game manager start.
        // This will make the game manager a singleton and callable by any script that is needed.
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

    // This function will be used to 
    public void SaveHighScore(int num)
    {
        if(PlayerPrefs.GetInt("Highscore") < num)
        {
            PlayerPrefs.SetInt("Highscore", num);
            _highscore = num;

            Debug.Log("Score updated");
            Debug.Log(num);
        }
    }

    // This function will be used to return the current highscore when needed.
    public int ReturnHighScore()
    {
        return _highscore;
    }
}
