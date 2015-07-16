using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public static class ArtificialIntelligence
    {
        #region Members / Properties
        static bool MaxPlayer = true;
        static Node BestMove;

        public static Board Board;
        private static int Depth;
        public static int[] CurbsRating = new int[] {
            10, 5,  5,  5,  5,  10,
            5,  1,  1,  1,  1,  5,
            5,  1,  1,  1,  1,  5,
            5,  1,  1,  1,  1,  5,
            5,  1,  1,  1,  1,  5,
            10, 5,  5,  5,  5,  10
        };
        #endregion

        #region Functions
        public static int GetBestMove(Board board, int depth)
        {
            Depth = depth;
            Board = new Board(board);

            switch(Board.CurrentTurn.Kind)
            {
                case PlayerKind.AIRandom:
                    return FindRandom();
                case PlayerKind.AICurbs:
                    return FindCurbs();
                default:
                    throw new NotImplementedException();
            }

            return -1;
        }
        
        private static int FindRandom()
        {
            BestMove = null;
            var rndMoves = Helpers.GenerateRandom(24, 0, 23);
            foreach(var moveID in rndMoves)
            {
                var encodedMoveID = Board.Moves[moveID].ID;
                if (Board.Move(Board.Moves[moveID].ID))
                    return encodedMoveID;
            }

            return -1;
        }

        private static int FindCurbs()
        {
            Iterate(new Node(Board), Depth, int.MinValue + 1, int.MaxValue - 1, true);
            return BestMove.MoveID;
        }

        private static int Iterate(Node node, int depth, int alpha, int beta, bool Player)
        {
            if (depth == 0 || node.IsTerminal(Player))
            {
                return node.GetTotalScore(Player);
            }

            if (Player == MaxPlayer)
            {
                foreach (Node child in node.Children(Player))
                {
                    int temp = Iterate(child, depth - 1, alpha, beta, !Player);
                    if (temp > alpha)
                    {
                        BestMove = child;
                        alpha = temp;
                    }

                }

                return alpha;
            }
            else
            {
                foreach (Node child in node.Children(Player))
                {
                    beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta, !Player));

                    if (beta < alpha)
                    {
                        break;
                    }
                }

                return beta;
            }
        }
        #endregion
    }

    public class Node
    {
        #region Members / Properties
        public Board _board;
        public int MoveID = -1;
        #endregion

        public Node(Board board)
        {
            _board = board;
        }

        #region Functions
        public List<Node> Children(bool Player)
        {
            List<Node> children = new List<Node>();
            var randomMoveIdx = Helpers.GenerateRandom(24, 0, 23);
            var moves = _board.Moves;

            foreach(var moveIdx in randomMoveIdx)
            {
                var childBoard = new Board(_board);
                if (childBoard.Move(moves[moveIdx]))
                {
                    var saveMoveID = moves[moveIdx].ID;
                    children.Add(new Node(childBoard) 
                    { 
                        MoveID = moves[moveIdx].ID 
                    });
                }
            }
            
            return children;
        }

        public bool IsTerminal(bool Player)
        {
            var terminalNode = false;

            return terminalNode;
        }

        public int GetTotalScore(bool Player)
        {
            return _board.GetWeightedBoardScore(ArtificialIntelligence.CurbsRating, Player);
        }
        #endregion
    }

}
