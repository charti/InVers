using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InVers.Model
{
    public class Token
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
