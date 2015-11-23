namespace Chrysalis
{
    public delegate void GameStateChangedEventHandler();

    public enum GameStateOption
    {
        playing,
        menu,
        dead
    };

    class GameState
    {
        public static event GameStateChangedEventHandler stateChanged;
        private static GameStateOption sharedState = GameStateOption.menu; 

        public static GameStateOption Current{
            get
            {
                return sharedState;
            }
            set
            {
                sharedState = value;
                if (stateChanged != null)
                    stateChanged();
            }
        }
    }
}
