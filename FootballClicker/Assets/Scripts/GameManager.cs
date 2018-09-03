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
    private int _gems;

    private bool _canContinue;
    private bool _continue;     //maybe change this

    private bool _isConnectedToTheInternet;

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

        if (PlayerPrefs.GetInt("FirstTimeLoadv1") != 1)
        {
            PlayerPrefs.SetInt("HelmetBall", 1);
            PlayerPrefs.SetString("Equipped", "HelmetBall");
            PlayerPrefs.SetInt("Coins", 1000);
            PlayerPrefs.SetInt("Gems", 0);

            PlayerPrefs.SetInt("FirstTimeLoadv1", 1);
        }
        

        _highscore = PlayerPrefs.GetInt("Highscore");       // Gets the current saved highscore and stores it in the variable.
        _coins = PlayerPrefs.GetInt("Coins");
        //_gems = PlayerPrefs.GetInt("Gems");
    }

    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)/*||Input.anyKeyDown*/)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position/*Input.mousePosition*/), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Pickup")
                {
                    try
                    {
                        hit.collider.gameObject.GetComponent<PickupBase>().Activate();
                    }
                    catch (System.Exception)
                    {

                        //error report or something
                    }
                }
            }
        }
    }


    #region Get and set functions

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

    public int ReturnCoins()
    {
        return _coins;
    }

    public void UpdateCoins(int num)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + num);
        _coins = PlayerPrefs.GetInt("Coins");
    }

    public int ReturnGems()
    {
        return _gems;
    }

    public void UpdateGems(int num)
    {
        PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + num);
        _gems = PlayerPrefs.GetInt("Gems");
    }

    public void SetGems(int num)
    {
        PlayerPrefs.SetInt("Gems", num);
    }

    public bool BuyItemCoins(int num)
    {
        if((PlayerPrefs.GetInt("Coins") - num) < 0)
        {
            Debug.Log("No coins removed");
            return false;
        }
        else
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - num);
            _coins = PlayerPrefs.GetInt("Coins");
            Debug.Log("Coins removed: " + num);
            return true;
        }
    }

    public bool BuyItemGems(int num)
    {
        if ((PlayerPrefs.GetInt("Gems") - num) < 0)
        {
            return false;
        }
        else
        {
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + num);
            return true;
        }
    }

    public void SetContinue(bool _cont)
    {
        _continue = _cont;
    }

    public bool GetContinue()
    {
        return _continue;
    }

    public void SetCanContinue(bool _canCont)
    {
        _canContinue = _canCont;
    }

    public bool GetCanContinue()
    {
        return _canContinue;
    }

    public void ChangeInternetBool(bool ConnectedOrNot)
    {
        _isConnectedToTheInternet = ConnectedOrNot;
    }

    #endregion get and set functions end
}
