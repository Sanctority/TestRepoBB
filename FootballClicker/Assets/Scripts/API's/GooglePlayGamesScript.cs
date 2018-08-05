using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;



public class GooglePlayGamesScript : MonoBehaviour {
#if UNITY_ANDROID

    public static GooglePlayGamesScript Instance { get; private set; }

    private void Start()
    {
        Instance = this;

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
        Social.localUser.Authenticate(success => {});
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

    #region Saved Games

    void ShowSelectUI()
    {
        uint maxNumToDisplay = 1;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }

    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    #endregion Saved Games End
#endif
}
