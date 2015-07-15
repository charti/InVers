using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public static class ArtificialIntelligence
    {
        public static Board Board;
        private static int Depth;

        public static int GetBestMove(Board board, int depth)
        {
            Depth = depth;
            Board = new Board(board);

            switch(Board.CurrentTurn.Kind)
            {
                case PlayerKind.AIRandom:
                    var rndMoves = Helpers.GenerateRandom(24, 0, 23);
                    foreach(var moveID in rndMoves)
                    {
                        var encodedMoveID = Board.Moves[moveID].ID;
                        if (Board.Move(Board.Moves[moveID].ID))
                            return encodedMoveID;
                    }
                    break;
                case PlayerKind.AIPoints:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return 0;
        }


    }
}
