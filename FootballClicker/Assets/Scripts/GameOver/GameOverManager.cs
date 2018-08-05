using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverManager : MonoBehaviour {

    private readonly int GEMSFORAD = 1;

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

        AdsManager._instance.RequestBanner();
        AdsManager._instance.RequestRewardBasedVideo();
    }

    public void RequestRewardAd()
    {
        if (AdsManager._instance._rewardBasedVideo.IsLoaded())
        {
            AdsManager._instance._rewardBasedVideo.Show();
            AdsManager._instance._rewardBasedVideo.OnAdRewarded += _rewardBasedVideo_OnAdRewarded;
        }

    }

    private void _rewardBasedVideo_OnAdRewarded(object sender, GoogleMobileAds.Api.Reward e)
    {
        GameManager._instance.UpdateGems(GEMSFORAD);
        _gemsEarned += GEMSFORAD;
        _gemsDisplayText.text = "Gems earned: " + _gemsEarned.ToString();
    }

    public void Continue()
    {
        if (GameManager._instance.GetCanContinue())
        {
            if (GameManager._instance.BuyItemGems(1))
            {
                Debug.LogError("CHANGE THE GEM COST");
                GameManager._instance.SetContinue(true);
                SceneController._instance.ReplayLastLevel();
                GameManager._instance.SetCanContinue(false);
            }
        }
    }

   public void PlayAgain()
    {
        GameManager._instance.SetContinue(false);
        GameManager._instance.SetCanContinue(false);
        SceneController._instance.ReplayLastLevel();
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
