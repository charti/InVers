﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    class Board
    {
        #region Properties / Members
        private Token[] _board = new Token[64];
        public Token[] BoardArr
        {
            get { return _board; }
            private set { _board = value; }
        }

        public Player CurrentTurn { get; private set; }
        
        private Player[] _players = new Player[2];
        public Player[] Players 
        { 
            get { return _players; } 
            private set { _players = value; } 
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
        }

        public bool Move(int encodedCoord)
        {
            var possibleMoves = GetPossibleMoves().AsEnumerable().ToList();

            if(possibleMoves.Contains(encodedCoord))
            {
                if ((encodedCoord % 8) == 0)                     //links
                {
                    var subArr = new ArraySegment<Token>(_board, encodedCoord, 7).ToArray();
                    subArr[0] = CurrentTurn.CurrentToken;
                    CurrentTurn.CurrentToken = subArr[6];
                    CurrentTurn.CurrentToken.IsFlipped = true;
                    Array.Copy(subArr, 0, _board, encodedCoord + 1, 6);
                }
                else if ((encodedCoord % 8) == 7)               //rechts
                {
                    var subArr = new ArraySegment<Token>(_board, encodedCoord - 6, 7).ToArray();
                    subArr[6] = CurrentTurn.CurrentToken;
                    CurrentTurn.CurrentToken = subArr[0];
                    CurrentTurn.CurrentToken.IsFlipped = true;
                    Array.Copy(subArr, 1, _board, encodedCoord - 6, 6);
                }
                else if (encodedCoord <= 6)                     //oben
                {
                    var subArr = new List<Token>();
                    subArr.Add(CurrentTurn.CurrentToken);
                    
                    for (int i = 8 + encodedCoord; i <= 54; i+=8)
                        subArr.Add(_board[i]);

                    CurrentTurn.CurrentToken = subArr.LastOrDefault();
                    CurrentTurn.CurrentToken.IsFlipped = true;

                    for (int idx = 8 + encodedCoord, count = 0; count <= 5; idx += 8, count++)
                        _board[idx] = subArr[count];
                }
                else if (encodedCoord >= 57)                    //unten
                {
                    var column = encodedCoord % 8;

                    var subArr = new List<Token>();
                    subArr.Add(CurrentTurn.CurrentToken);

                    for (int i = encodedCoord - 8; i >= 9; i -= 8)
                        subArr.Add(_board[i]);

                    CurrentTurn.CurrentToken = subArr.LastOrDefault();
                    CurrentTurn.CurrentToken.IsFlipped = true;

                    for (int idx = encodedCoord - 8, count = 0; count <= 5; idx -= 8, count++)
                        _board[idx] = subArr[count];
                }

                CurrentTurn = _players.First(pl => CurrentTurn != pl);

                return true;
            }

            return false;
        }

        private IEnumerable<int> GetPossibleMoves()
        {
            var allFlippedEnemyCurbs = _board.Where(token =>
                    token != null &&
                    token.IsFlipped && 
                    token.Color != CurrentTurn.Color)
                .Select()
                .ToArray();


            return new [] {8};
        }
    }
}

