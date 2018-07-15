using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour {

    // Public variables.
    public TextMeshProUGUI _highscoreDisplayText;       // This will store a reference to the highscore text for use.
    public TextMeshProUGUI _previousDisplayText;
    public TextMeshProUGUI _gemsDisplayText;

    // Private variables.
    private GameManager _GM;                            // This will store a reference to the GameManager.
    private int _gemsEarned;
    private int _numOfBounces;

    private void Start()
    {
        _GM = GameManager._instance;

        _gemsEarned = PlayerPrefs.GetInt("GemCount");
        _numOfBounces = PlayerPrefs.GetInt("Bounces");

        UpdateHighscoreText();
    }

    // This funciton is used to refresh the highscore text.
    private void UpdateHighscoreText()
    {
        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        _previousDisplayText.text = "Previous score: " + PlayerPrefs.GetInt("PreviousScore").ToString(); ;
        _gemsDisplayText.text = "Gems earned: " + _gemsEarned.ToString();

        _GM.UpdateGems(_gemsEarned);

    #if UNITY_ANDROID
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_noob, _numOfBounces);
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_novice, _numOfBounces);
    #endif
    }
}
