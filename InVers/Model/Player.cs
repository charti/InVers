using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public enum PlayerKind { Human, AI };
    public enum PlayerColor { red = 0, yellow = 1 }

    public class Player
    {
        #region Members / Properties
        private PlayerKind _kind;
        public PlayerColor Color { get; private set; }
        public Token CurrentToken;
        private static int _playerCount = 0;
        #endregion

        public Player(PlayerKind kind)
        {
            _kind = kind;
            Color = (PlayerColor)_playerCount;
            CurrentToken = new Token(Color) { IsFlipped = true };

            _playerCount = (_playerCount + 1) % 2;
        }
    }

}
