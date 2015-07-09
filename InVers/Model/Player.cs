using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InVers.Model
{
    public class Player
    {
        private PlayerKind kind;
        public PlayerColor Color { get; private set; }
        private static int playerCount = 0;
        public Player(PlayerKind kind)
        {
            this.kind = kind;
            Color = (PlayerColor)playerCount;

            playerCount = (playerCount + 1) % 2;
        }
    }

    public enum PlayerKind { Human, AI };
    public enum PlayerColor { red = 0, yellow = 1}
}
