using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InVers.Model
{
    public enum MoveKind { Left = 0, Right = 1, Top = 2, Bottom = 3 }

    public class Field
    {
        public Type Type { get; private set; }
        public Field()
        {
            Type = this.GetType();
        }
    }

    public class Move : Field
    {
        #region Members / Properties
        public int ID { get; private set; }
        int _blockIdx;
        int _moveKind;
        #endregion

        public Move(int coord)
        {
            ID = coord;

            if (coord < 7)
            {
                _moveKind = (int)MoveKind.Top;
                _blockIdx = 48 + coord;
            }
            else if (coord % 8 == 0)
            {
                _moveKind = (int)MoveKind.Left;
                _blockIdx = coord + 6;
            }
            else if (coord % 8 == 7)
            {
                _moveKind = (int)MoveKind.Right;
                _blockIdx = coord - 6;
            }
            else if (coord > 56)
            {
                _moveKind = (int)MoveKind.Bottom;
                _blockIdx = (coord % 8) + 8;
            }
        }

        #region Functions
        public bool Slide(IEnumerable<Token> board, Player currentPlayer)
        {
            var boardArr = board.ToArray();
            if (boardArr[_blockIdx].IsFlipped &&
                boardArr[_blockIdx].Color != currentPlayer.Color)
                return false;

            var rowColor = new List<PlayerColor>();
            var rowFlip = new List<bool>();
            switch ((MoveKind)_moveKind)
            {
                case MoveKind.Left:
                    rowColor = new List<PlayerColor>() { currentPlayer.CurrentToken.Color };
                    rowFlip = new List<bool>() { currentPlayer.CurrentToken.IsFlipped };
                    for (int i = ID + 1; i < _blockIdx; i++)
                    {
                        rowColor.Add(boardArr[i].Color);
                        rowFlip.Add(boardArr[i].IsFlipped);
                    }

                    currentPlayer.CurrentToken.Color = boardArr[_blockIdx].Color;

                    for (int i = ID + 1, count = 0; count < 6; i++, count++)
                    {
                        boardArr[i].Color = rowColor[count];
                        boardArr[i].IsFlipped = rowFlip[count];
                    }

                    break;
                case MoveKind.Right:
                    rowColor = new List<PlayerColor>() { currentPlayer.CurrentToken.Color };
                    rowFlip = new List<bool>() { currentPlayer.CurrentToken.IsFlipped };
                    for (int i = ID - 1; i > _blockIdx; i--)
                    {
                        rowColor.Add(boardArr[i].Color);
                        rowFlip.Add(boardArr[i].IsFlipped);
                    }

                    currentPlayer.CurrentToken.Color = boardArr[_blockIdx].Color;

                    for (int i = ID - 1, count = 0; count < 6; i--, count++)
                    {
                        boardArr[i].Color = rowColor[count];
                        boardArr[i].IsFlipped = rowFlip[count];
                    }
                    break;
                case MoveKind.Top:
                    rowColor = new List<PlayerColor>() { currentPlayer.CurrentToken.Color };
                    rowFlip = new List<bool>() { currentPlayer.CurrentToken.IsFlipped };
                    for (int i = ID + 8; i < _blockIdx; i+=8)
                    {
                        rowColor.Add(boardArr[i].Color);
                        rowFlip.Add(boardArr[i].IsFlipped);
                    }

                    currentPlayer.CurrentToken.Color = boardArr[_blockIdx].Color;

                    for (int i = ID + 8, count = 0; count < 6; i+=8, count++)
                    {
                        boardArr[i].Color = rowColor[count];
                        boardArr[i].IsFlipped = rowFlip[count];
                    }
                    break;
                case MoveKind.Bottom:
                    rowColor = new List<PlayerColor>() { currentPlayer.CurrentToken.Color };
                    rowFlip = new List<bool>() { currentPlayer.CurrentToken.IsFlipped };
                    for (int i = ID - 8; i > _blockIdx; i-=8)
                    {
                        rowColor.Add(boardArr[i].Color);
                        rowFlip.Add(boardArr[i].IsFlipped);
                    }

                    currentPlayer.CurrentToken.Color = boardArr[_blockIdx].Color;

                    for (int i = ID - 8, count = 0; count < 6; i-=8, count++)
                    {
                        boardArr[i].Color = rowColor[count];
                        boardArr[i].IsFlipped = rowFlip[count];
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
        #endregion
    }

    public class Token : Field
    {
        public PlayerColor Color;
        public bool IsFlipped;

        public Token(PlayerColor color)
        {
            Color = color;
            IsFlipped = false;
        }

        public ImageBrush GetImage()
        {
            Uri uri;
            string fileExtension = IsFlipped ? "_flipped.bmp" : ".bmp";

            if (Color == PlayerColor.red)
                uri = new Uri("pack://application:,,,/Resources/Images/red" + fileExtension);
            else
                uri = new Uri("pack://application:,,,/Resources/Images/yellow" + fileExtension);
            
            return new ImageBrush(new BitmapImage(uri));
        }
    }
}
