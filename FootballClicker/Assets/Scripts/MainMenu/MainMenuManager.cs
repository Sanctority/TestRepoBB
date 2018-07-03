using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour {

    // Public variables.
    public TextMeshProUGUI _highscoreDisplayText;       // This will store a reference to the highscore text for use.
    public TextMeshProUGUI _previousScore;

    // Private variables.
    private GameManager _GM;                            // This will store a reference to the GameManager.

    private void Start()
    {
        _GM = GameManager._instance;
        UpdateHighscoreText();
    }

    // This funciton is used to refresh the highscore text.
    private void UpdateHighscoreText()
    {
        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString(); ;
        _previousScore.text = "Previous score: " + PlayerPrefs.GetInt("PreviousScore").ToString();
    }

    public void ShowAchievements()
    {
        GooglePlayGamesScript.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        GooglePlayGamesScript.ShowLeaderboardsUI();
    }
}
