using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using TMPro;

public class FacebookScript : MonoBehaviour {

    public TextMeshProUGUI _friendsText;

    [SerializeField]
    private GameObject _facebookLoginButton;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.Log("Facebook sdk could not initialize");
                }
            },
            _isGameShown =>
            {
                if (!_isGameShown)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
            );
        }
        else
        {
            FB.ActivateApp();
        }

        _facebookLoginButton.SetActive(false);
    }

    private void Start()
    {
        CheckIfLoggedIntoFacebook();
    }

    #region Facebook log in and out

    public void FacebookLogin()
    {
        List<string> permissions = new List<string>() { "public_profile","email","user_friends" };

         FB.LogInWithReadPermissions(permissions); // add callbacks

        bool _loggedIn = FB.IsLoggedIn;

        if(_loggedIn == true)
        {
            GetFriendsPlayingThisGame();
            CheckIfLoggedIntoFacebook();
        }
        else if(_loggedIn == false)
        {
            CheckIfLoggedIntoFacebook();
        }
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }

    #endregion

    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("www.google.com"),"Cool new game!",
            "I just found this awsome new game and I think you should download the game and play it to");
    }

    #region Facebook inviting

    public void FacebookGameRequest()
    {
        FB.AppRequest("Check out this cool game im playing.", title: "Football clicker!");
    }

    public void FacebookCustomGameRequesrt(string Description, string Title)
    {
        FB.AppRequest(Description, title: Title);
    }

    public void FacebookInvite()
    {
        FB.Mobile.AppInvite(new System.Uri(""));
    }

    #endregion Facebook inviting end

    public void GetFriendsPlayingThisGame()
    {
        string query = "/me/friends";

        FB.API(query, HttpMethod.GET, result =>
        {
            var _dictionary = (Dictionary<string,object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);

            var _friendsList = (List<object>)_dictionary["data"];

            _friendsText.text = string.Empty;

            foreach(var _diction in _friendsList)
            {
                _friendsText.text += ((Dictionary<string,object>)_diction)["name"];
            }
        });
    }

    public void CheckIfLoggedIntoFacebook()
    {
        bool _loggedIn = FB.IsLoggedIn;

        if (_loggedIn == true)
        {
            _facebookLoginButton.SetActive(false);
        }
        
    }
}
