using UnityEngine;
using UnityEngine.UI;
using System;

namespace Chrysalis
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        Butterfly butterfly;
        [SerializeField]
        ScoreHolder scoreHolder;
        [SerializeField]
        Text DeathMsg;
        [SerializeField]
        Slider FlowerEnergy;
           
        // Use this for initialization
        void Start()
        {
            // respond to butteryfly death
            butterfly.Died +=
                new ButterflyDeathEventHandler(OnButterflyDeath);
            butterfly.Reset();
        }

        public void Detach()
        {
            butterfly.Died -=
                new ButterflyDeathEventHandler(OnButterflyDeath);
        }

        // Update is called once per frame
        void Update()
        {
            scoreHolder.SetScore(Mathf.FloorToInt(butterfly.transform.position.x));
            FlowerEnergy.value = butterfly.FlowerEnergy;
        }


        /// <summary>
        /// Handle the buttry flies death
        /// </summary>
        /// <param name="e"></param>
        void OnButterflyDeath(object sender, String killerName)
        {
            // show restart dialog with high scool list and reset button
            // reset the stage
            butterfly.Reset();
            GameState.Current = GameStateOption.dead;

            switch (killerName)
            {
                case "Spiderweb":
                    DeathMsg.text = "You ran into a spiderweb :(";
                    break;
                case "Branch":
                    DeathMsg.text = "You ran into branch, and a squirrle got you :(";
                    break;
                case "FlowerEnergy":
                    DeathMsg.text = "You ran our of energy (Land on more flowers) :(";
                    break;
            }
        }
    }
}
