using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour {

    // Public variables.
    public TextMeshProUGUI _highscoreDisplayText;       // This will store a reference to the highscore text for use.

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
<<<<<<< HEAD
<<<<<<< HEAD
        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        _previousDisplayText.text = "Previous score: " + PlayerPrefs.GetInt("PreviousScore").ToString(); ;
        _gemsDisplayText.text = "Gems earned: " + _gemsEarned.ToString();

        _GM.UpdateGems(_gemsEarned);

=======
        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        _previousDisplayText.text = "Previous score: " + PlayerPrefs.GetInt("PreviousScore").ToString(); ;
        _gemsDisplayText.text = "Gems earned: " + _gemsEarned.ToString();

        _GM.UpdateGems(_gemsEarned);

>>>>>>> 52b7fd22a4c2802d351f959e294a6e53ecb7812b
    #if UNITY_ANDROID
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_noob, _numOfBounces);
        GooglePlayGamesScript.IncrementAchievement(GPGSIds.achievement_kicking_novice, _numOfBounces);
    #endif
=======
        _highscoreDisplayText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString(); ;
>>>>>>> parent of c2712fe... Update
    }
}
