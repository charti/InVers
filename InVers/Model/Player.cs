using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public enum PlayerKind  { Human, AIRandom, AICurbs, AIPoints };
    public enum PlayerColor { red = 0, yellow = 1 }

    public class Player
    {
        #region Members / Properties
        public PlayerKind Kind { get; private set; }
        public PlayerColor Color { get; private set; }
        public Token CurrentToken;
        private static int _playerCount = 0;
        #endregion

        public Player(PlayerKind kind)
        {
            Kind = kind;
            Color = (PlayerColor)_playerCount;
            CurrentToken = new Token(Color) { IsFlipped = true };

            _playerCount = (_playerCount + 1) % 2;
        }
    }

}
