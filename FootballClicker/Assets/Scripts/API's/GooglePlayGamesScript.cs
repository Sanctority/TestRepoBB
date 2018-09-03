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
        Social.localUser.Authenticate(success => { LoadData();  });
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

    #region Gem save
    string GameDataToString()
    {
        return GameManager._instance.ReturnGems().ToString();
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string cloudData, string localData)
    {
        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            if (int.Parse(cloudData) > int.Parse(localData)) //cloud save is more up to date
            {
                //set local save to be equal to the cloud save
                PlayerPrefs.SetString(_gemSave, cloudData);
                GameManager._instance.ChangeInternetBool(true);
            }
            GameManager._instance.ChangeInternetBool(true);
        }
        //if it's not the first time, get gems from the cloud
        else
        {
                GameManager._instance.SetGems(int.Parse(cloudData));
                _isGemCloudDataLoaded = true;
                //saving the updated CloudVariables to the cloud
                GameManager._instance.ChangeInternetBool(true);
                SaveData();
                return;
        }
        //id all else fails then set the gems to the cloud stored gems
        GameManager._instance.SetGems(int.Parse(cloudData));
        _isGemCloudDataLoaded = true;
        GameManager._instance.ChangeInternetBool(true);
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        GameManager._instance.ChangeInternetBool(false);
    }

    //used for loading data from the cloud or locally
    public void LoadData()
    {
        //basically if we're connected to the internet, do everything on the cloud
        if (Social.localUser.authenticated)
        {
            _isGemSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(_gemSave,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        //this will basically only run in Unity Editor, as on device,
        //localUser will be authenticated even if he's not connected to the internet (if the player is using GPG)
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(_gemSave));
    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {
        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!_isGemCloudDataLoaded)
        {
            SaveLocal();
            return;
        }
        //same as in LoadData
        if (Social.localUser.authenticated)
        {
            _isGemSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(_gemSave,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(_gemSave, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(unmerged);
        else
        {
            resolver.ChooseMetadata(unmerged);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!_isGemSaving)
                LoadGame(game);
            //if we're SAVING game data
            else
                SaveGame(game);
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //the same code that is in else statements in LoadData() and SaveData()
        else
        {
            if (!_isGemSaving)
                LoadLocal();
            else
                SaveLocal();
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)
        PlayerPrefs.SetString(_saveName, stringToSave);

        //encoding to byte array
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        //uploading data to the cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
    }

    //callback for ReadBinaryData
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
                //in such case, we want to assign "0" to our string
                cloudDataString = "0";
            //otherwise take the byte[] of data and encode it to string
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            //getting local data (if we've never played before on this device, localData is already
            //"0", so there's no need for checking as with cloudDataString)
            string localDataString = PlayerPrefs.GetString(_saveName);

            //this method will compare cloud and local data
            StringToGameData(cloudDataString, localDataString);
        }
    }

    //callback for CommitUpdate
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }
    #endregion /Gem Save

    #endregion /Saved Games

#endif
}
