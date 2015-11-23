using UnityEngine;
using System;

namespace Chrysalis
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        Butterfly butterfly;
        [SerializeField]
        ScoreHolder scoreHolder;
           
        // Use this for initialization
        void Start()
        {
            // respond to butteryfly death
            butterfly.Died +=
                new ButterflyEventHandler(OnButterflyDeath);
            butterfly.Reset();
        }

        public void Detach()
        {
            butterfly.Died -=
                new ButterflyEventHandler(OnButterflyDeath);
        }

        // Update is called once per frame
        void Update()
        {
            scoreHolder.SetScore(Mathf.FloorToInt(butterfly.transform.position.x));
        }


        /// <summary>
        /// Handle the buttry flies death
        /// </summary>
        /// <param name="e"></param>
        void OnButterflyDeath(object sender, EventArgs e)
        {
            // show restart dialog with high scool list and reset button
            // reset the stage
            butterfly.Reset();
            GameState.Current = GameStateOption.dead;
        }
    }
}
