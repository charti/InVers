using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public class Board
    {
        #region Properties / Members
        private Field[] _board = new Field[64];

        public Player CurrentTurn { get; private set; }
        
        private Player[] _players = new Player[2];
        public Player[] Players 
        { 
            get { return _players; } 
            private set { _players = value; } 
        }
        public Token[] Tokens
        {
            get
            {
                return _board.Where(field => field != null && field.Type == typeof(Token))
                    .Select(token => token as Token).ToArray();
            }
        }

        public Move[] Moves
        {
            get
            {
                return _board.Where(field => field != null && field.Type == typeof(Move))
                    .Select(move => move as Move).ToArray();
            }
        }
        #endregion

        public Board(IEnumerable<Player> players)
        {
            _players = players.ToArray();
            CurrentTurn = _players[0];
            InitBoard();
        }

        private void InitBoard()
        {

            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 5; j += 2)
                {
                    var encodedIdx = 8 * i + j;
                    if ((i % 2) == 1)
                    {
                        _board[encodedIdx] = new Token(PlayerColor.red);
                        _board[encodedIdx + 1] = new Token(PlayerColor.yellow);
                    }
                    else
                    {
                        _board[encodedIdx + 1] = new Token(PlayerColor.red);
                        _board[encodedIdx] = new Token(PlayerColor.yellow);
                    }
                }
            }

            for (int i = 1; i < 63; i++)
                if (_board[i] == null && i != 7 && i != 56)
                    _board[i] = new Move(i);

        }

        public bool Move(int encodedCoord)
        {
            var success = Moves.First(move => move.ID == encodedCoord)
                .Slide(_board.Select(token => token as Token).AsEnumerable(), CurrentTurn);

            if (success)
            {
                CurrentTurn = _players.First(pl => CurrentTurn != pl);
                return true;
            }

            return false;
        }

    }
}

