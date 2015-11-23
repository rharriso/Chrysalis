using UnityEngine;
using System.Collections;

namespace Chrysalis
{
    public class DeadText : MonoBehaviour
    {

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
        /// handle state change
        /// </summary>
        void OnGameStateChanged()
        {
            gameObject.SetActive(GameState.Current == GameStateOption.dead);
        }
    }
}
