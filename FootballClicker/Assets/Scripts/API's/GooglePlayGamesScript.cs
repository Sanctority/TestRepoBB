using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using System;



public class GooglePlayGamesScript : MonoBehaviour {
#if UNITY_ANDROID

    public static GooglePlayGamesScript Instance { get; private set; }

<<<<<<< HEAD
=======
    const string _saveName = "BouncyBoySave";
    const string _gemSave = "PlayersGems";

    bool _isSaving;

    bool _isGemCloudDataLoaded;
>>>>>>> parent of de98bc4... cloud save added

    private void Start()
    {
        Instance = this;

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
<<<<<<< HEAD
        Social.localUser.Authenticate(success => {  GameManager._instance.PlayerIsOnline();  });
=======
        Social.localUser.Authenticate(success => {});
>>>>>>> parent of de98bc4... cloud save added
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

<<<<<<< HEAD
    #region save



    #endregion
=======
    #region Saved Games

    private string GemsToString()
    {
        return GameManager._instance.ReturnGems().ToString();
    }

    void GemsStringToGameData(string cloudData, string localData)
    {
        if(PlayerPrefs.GetInt("FirstTimeSave") == 1)
        {
            PlayerPrefs.SetInt("FirstTimeSave", 0);

            if(int.Parse(localData) > int.Parse(cloudData))
            {
                GameManager._instance.SetGems(int.Parse(localData));
                _isGemCloudDataLoaded = true;
                SaveData();
                return;
            }
        }
        else
        {
            GameManager._instance.SetGems(int.Parse(cloudData));
            _isGemCloudDataLoaded = true;
        }
        
    }

    void GemsStringToGameData(string localData)
    {
        _isGemCloudDataLoaded = false;
        GameManager._instance.SetGems(0);
    }

    public void GemsLoadData()
    {
        if (Social.localUser.authenticated)
        {
            _isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(_gemSave, DataSource.ReadCacheOrNetwork, true, GemsResolveConflict, GemsSaveOpened);
        }

    }

    public void SaveData()
    {
        if (!_isGemCloudDataLoaded)
        {
            return;
        }

        if (Social.localUser.authenticated)
        {
            _isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(_gemSave, DataSource.ReadCacheOrNetwork, true, GemsResolveConflict, GemsSaveOpened);
        }
    }

    private void GemsResolveConflict(IConflictResolver resolver,ISavedGameMetadata original,byte[] originalData,ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if(originalData == null)
        {
            resolver.ChooseMetadata(unmerged);
        }
        else if(unmergedData == null)
        {
            resolver.ChooseMetadata(original);
        }
        else
        {
            resolver.ChooseMetadata(original);
            return;
        }

        resolver.ChooseMetadata(original);
    }

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
>>>>>>> parent of de98bc4... cloud save added

    #endregion Saved Games End
#endif
}
