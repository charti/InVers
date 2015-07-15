namespace InVers.Control
{
    public static class Messages
    {
        /// <summary>
        /// Tries to make a move.
        /// </summary>
        public const string MakeTurn = "makeTurn";

        /// <summary>
        /// Tries to make a move.
        /// </summary>
        public const string AITurn = "aITurn";

        /// <summary>
        /// Starts a new game
        /// </summary>
        public const string NewGame = "newGame";

        /// <summary>
        /// Signals end of game initialisation
        /// </summary>
        public const string GameInitEnd = "gameInitEnd";
        
        /// <summary>
        /// Refreshes the View
        /// </summary>
        public const string RefreshView = "refreshView";

        /// <summary>
        /// Raises an Error Box
        /// </summary>
        public const string RaiseError = "raiseError";

        /// <summary>
        /// Refreshes the Score
        /// </summary>
        public const string RefreshScore = "refreshScore";

        /// <summary>
        /// Notifies Winner
        /// </summary>
        public const string NotifyWinner = "notifyWinner";
    }
}
