using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chrysalis
{
    public class GameState
    {
        private static GameState gameState = null;

        /// <summary>
        /// The shared instance singleton
        /// </summary>
        public static GameState sharedInstance
        {
            get
            {
                if (gameState == null)
                {
                    gameState = new GameState();
                }
                return gameState;
            }
        }


        /// when did this game state start
        private DateTime startTime;
        private bool running;


        /// <summary>
        /// Start the timer
        /// </summary>
        public void start()
        {
            if (!running)
            {
                running = true;
                startTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Returns the ammoutn of time since start
        /// </summary>
        /// <returns></returns>
        public TimeSpan currentTime()
        {
            var currTime = DateTime.Now;
            return currTime.Subtract(startTime);
        }
    }
}
