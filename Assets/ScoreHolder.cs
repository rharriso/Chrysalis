using UnityEngine;
using UnityEngine.UI;

namespace Chrysalis
{
    public class ScoreHolder : MonoBehaviour
    {
        [SerializeField]
        Text totalScoreText;
        [SerializeField]
        Text highScoreText;


        int highScore;

        // Use this for initialization
        void Start()
        {
            // load high score
            highScore = PlayerPrefs.GetInt("rharriso.chrysalis.highScore");
            highScoreText.text = highScore.ToString();

            // listen to state change
            GameState.stateChanged +=
                new GameStateChangedEventHandler(OnGameStateChanged);
            OnGameStateChanged();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// handle state change
        /// </summary>
        void OnGameStateChanged()
        {
            gameObject.SetActive(GameState.Current == GameStateOption.playing);
        }

        /// <summary>
        /// set the score
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            totalScoreText.text = score.ToString();
            if(score > highScore)
            {
                highScore = score;
                totalScoreText.color = Color.green;
                highScoreText.text = highScore.ToString();
                PlayerPrefs.SetInt("rharriso.chrysalis.highScore", highScore);
            }
            else
            {
                totalScoreText.color = Color.red;
            }
        }
    }
}
