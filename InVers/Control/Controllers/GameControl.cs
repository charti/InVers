using InVers.Model;
using InVers.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    InitGame();
                    Mediator.NotifyColleagues(Messages.GameInitEnd, GetTokenBoard());
                    break;
                case Messages.MakeTurn:
                      if(_board.Move((int)args))
                        Mediator.NotifyColleagues(Messages.RefreshView, GetTokenBoard());
                    else
                        Mediator.NotifyColleagues(Messages.RaiseError, "Wrong turn!");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region Functions
        public void InitGame()
        {
            _board = new Board(new[] {
                new Player(PlayerKind.Human),
                new Player(PlayerKind.AI)
            });
        }
        private IEnumerable<Token> GetTokenBoard()
        {
            return _board.BoardArr.Where(el => el != null).AsEnumerable<Token>();
        }
        #endregion
    }
}
