using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;



public class GooglePlayGamesScript : MonoBehaviour {
#if UNITY_ANDROID

    public static GooglePlayGamesScript Instance { get; private set; }

    const string _saveName = "BouncyBoySave";
    const string _gemSave = "PlayersGems";

    bool _isGemSaving;

    bool _isGemCloudDataLoaded;

    private void Start()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey(_saveName))
        {
            PlayerPrefs.SetString(_saveName, "0");
        }

        if (!PlayerPrefs.HasKey("FirstTimeSave"))
        {
            PlayerPrefs.SetInt("FirstTimeSave", 1);
        }

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesClientConfiguration _config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(_config);

        PlayGamesPlatform.Activate();

        SignIn();
    }

    private void SignIn()
    {
        Social.localUser.Authenticate(success => {  GameManager._instance.PlayerIsOnline();  });
    }

    public static bool CheckIfLoggedIn()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Achievements

    public static void UnlockAchievement(string AchievementID)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(AchievementID, 100, success => { });
        }
    }

    public static void IncrementAchievement(string AchievementID, int AchievementIncrement)
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(AchievementID, AchievementIncrement, success => { });
        }
    }

    public static void ShowAchievementsUI()
    {
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
        }
    }

    #endregion AchievementsEnd

    #region Leaderboards

    public static void AddScoreToLeaderboard(string LeaderboardID,long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, LeaderboardID, success => { });
        }
        
    }

    public static void ShowLeaderboardsUI()
    {
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_highest_score);
        }
    }

    #endregion LeaderboardsEnd

   

#endif
}
