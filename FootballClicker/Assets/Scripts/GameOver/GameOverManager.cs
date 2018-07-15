using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour {

    // Public variables.
    public TextMeshProUGUI _highscoreDisplayText;       // This will store a reference to the highscore text for use.
    public TextMeshProUGUI _previousDisplayText;
    public TextMeshProUGUI _gemsDisplayText;
    public TextMeshProUGUI _coinsDisplayText;

    // Private variables.
    private GameManager _GM;                            // This will store a reference to the GameManager.
    private int _gemsEarned;
    private int _numOfBounces;

    private void Start()
    {
        _gemsEarned = PlayerPrefs.GetInt("GemsEarned");
        _numOfBounces = PlayerPrefs.GetInt("Bounces");
        _GM = GameManager._instance;
        UpdateHighscoreText();
    }

    // This funciton is used to refresh the highscore text.
    private void UpdateHighscoreText()
    {

        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        _previousDisplayText.text = "Previous score: " + PlayerPrefs.GetInt("PreviousScore").ToString(); ;
        _gemsDisplayText.text = "Gems earned: " + _gemsEarned.ToString();
        _coinsDisplayText.text = "Coins earned: " + PlayerPrefs.GetInt("CoinsEarned").ToString();

        _GM.UpdateGems(_gemsEarned);


    #if UNITY_ANDROID
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_noob, _numOfBounces);
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_novice, _numOfBounces);
    #endif


    }
}
