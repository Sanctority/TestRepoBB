using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MainLevel
{
    public class UiScript : MonoBehaviour
    {
        // Public variables.
        public TextMeshProUGUI _scoreText;
        public string _textBeforeScore;

        // Private variables.
        [SerializeField]
        public float _scoreFloat;

        [SerializeField]
        private float _coinMultiplier;

        [SerializeField]
        private float _eventMultiplier;

        private void Start()
        {
            _scoreFloat = 0;
            _scoreText.text = _textBeforeScore + _scoreFloat.ToString();
        }

        private void Update()
        {
            _scoreFloat += Time.deltaTime;
            _scoreText.text = _textBeforeScore + (int)_scoreFloat; // Shows the score as an int.
        }

        public void GameOver()
        {
            GameManager._instance.SaveHighScore((int)_scoreFloat);

            GameManager._instance.UpdateCoins((int)(_scoreFloat *(_coinMultiplier * _eventMultiplier)));

            PlayerPrefs.SetInt("CoinsEarned", (int)(_scoreFloat * (_coinMultiplier * _eventMultiplier)));
#if UNITY_ANDROID
            if (GooglePlayGamesScript.CheckIfLoggedIn() == true)
            {
                GooglePlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_highest_score, (long)_scoreFloat);
            }
#endif            
        }

        public float ReturnScoreFloat() 
        {
            return _scoreFloat;
        }
    }
}

