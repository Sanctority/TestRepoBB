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
        public float _scoreFloat;

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
            GooglePlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_high_scores, (long)_scoreFloat);
        }
    }
}

