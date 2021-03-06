﻿using InVers.Model;

using System;

namespace InVers.Control
{
    public class GameControl : BaseController
    {
        #region Properties / Members
        private Board _board;
        #endregion

        public GameControl()
        {
            Mediator.Register(this, new[] {
                Messages.NewGame,
                Messages.MakeTurn
            });
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.NewGame:
                    InitGame(args as int[]);
                    UpdateView();
                    Mediator.NotifyColleagues(Messages.GameInitEnd, null);
                    MoveAITry();
                    break;
                case Messages.MakeTurn:
                    if (args == null)
                    {
                        _board.Move(ArtificialIntelligence.GetBestMove(_board, 3));

                        UpdateView();
                        MoveAITry();
                    }
                    else if(_board.CurrentTurn.Kind == PlayerKind.Human)
                    {
                        if (_board.Move((int)args))
                        {
                            UpdateView();
                            MoveAITry();
                        }
                        else
                            Mediator.NotifyColleagues(Messages.RaiseError, "Wrong turn!");
                    }
                    TestWinningCondition();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region Functions
        public void InitGame(int[] playerKinds)
        {
            _board = new Board(new[] {
                new Player((PlayerKind)playerKinds[0]),
                new Player((PlayerKind)playerKinds[1])
            });
        }
        private void MoveAITry()
        {
            if (_board.CurrentTurn.Kind != PlayerKind.Human)
                Mediator.NotifyColleagues(Messages.AITurn, null);
        }
        
        private void TestWinningCondition()
        {
            var playerWon = _board.Won();
            if (playerWon != null)
                Mediator.NotifyColleagues(Messages.NotifyWinner, playerWon);
        }

        private void UpdateView()
        {
            Mediator.NotifyColleagues(Messages.RefreshView, _board.Tokens);
            Mediator.NotifyColleagues(Messages.RefreshScore, _board.GetScore());

            var players = _board.CurrentTurn.Color == PlayerColor.red ?
                new [] { 100, 0 } : new [] { 0, 100 };
            Mediator.NotifyColleagues(Messages.NotifyCurrentPlayer, players);
        }
        #endregion
    }
}
