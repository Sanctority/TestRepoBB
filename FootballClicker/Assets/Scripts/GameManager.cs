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
    private int _coins;

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
        //PlayerPrefs.SetInt("FirstTimeLoad", 0);

        if (PlayerPrefs.GetInt("FirstTimeLoad") != 1)
        {
            PlayerPrefs.SetInt("0", 1);
            PlayerPrefs.SetInt("Equipped", 0);
            PlayerPrefs.SetInt("Coins", 1000);

            PlayerPrefs.SetInt("FirstTimeLoad", 1);
        }
        

        _highscore = PlayerPrefs.GetInt("Highscore");       // Gets the current saved highscore and stores it in the variable.
        _coins = PlayerPrefs.GetInt("Coins");
    }

    public void SaveHighScore(int num)
    {
        PlayerPrefs.SetInt("PreviousScore", num);

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

    public void RunAudio()
    {
       
    }

    public int ReturnCoins()
    {
        return _coins;
    }

    public void UpdateCoins(int num)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + num);
        _coins = PlayerPrefs.GetInt("Coins");
    }

    public bool BuyItem(int num)
    {
        if((PlayerPrefs.GetInt("Coins") - num) < 0)
        {
            return false;
        }
        else
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + num);
            return true;
        }
    }
}
