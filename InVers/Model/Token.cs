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
        int _ID;
        int _blockIdx;
        int _moveKind;

        public Move(int coord)
        {
            _ID = coord;

            if (0 < coord & coord < 7)
            {
                _moveKind = (int)MoveKind.Top;
                _blockIdx = 48 + coord;
            }
            else if (coord % 8 == 0)
            {
                _moveKind = (int)MoveKind.Left;
                _blockIdx = coord + 6;
            }
        }
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
            string flipped = IsFlipped ? "_flipped.bmp" : ".bmp";

            if (Color == PlayerColor.red)
                uri = new Uri("pack://application:,,,/Resources/Images/red" + flipped);
            else
                uri = new Uri("pack://application:,,,/Resources/Images/yellow" + flipped);
            
            return new ImageBrush(new BitmapImage(uri));
        }
    }
}
