using UnityEngine;
using UnityEngine.UI;

namespace Chrysalis
{
    public class Menu : MonoBehaviour
    {
        [SerializeField]
        Text highScoreText;

        // Use this for initialization
        void Start()
        {
            GameState.stateChanged +=
                new GameStateChangedEventHandler(OnGameStateChanged);
            OnGameStateChanged();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        void Refresh()
        {
            highScoreText.text =
                PlayerPrefs.GetInt("rharriso.chrysalis.highScore").ToString();
        }
        
        /// <summary>
        /// handle state change
        /// </summary>
        void OnGameStateChanged()
        {
            gameObject.SetActive(GameState.Current == GameStateOption.menu ||
                GameState.Current == GameStateOption.dead);
            Refresh();
        }

        /// <summary>
        /// Start playing
        /// </summary>
        void OnPlayClicked()
        {
            GameState.Current = GameStateOption.playing;
        }
    }
}