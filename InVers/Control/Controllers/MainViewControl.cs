using InVers.Control;
using InVers.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InVers.Control
{
    public class MainViewControl : BaseController
    {
        #region Properties / Members
        private ImageBrush[] _viewBoard = new ImageBrush[36];
        public ImageBrush[] ViewBoard
        {
            get
            {
                return _viewBoard;
            }
            private set
            {
                _viewBoard = value;
            }
        }
        #endregion
        
        public MainViewControl()
        {
            Mediator.Register(this, new[] {
                Messages.GameInitEnd,
                Messages.RefreshView,
                Messages.RaiseError
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.Turn, Turn_Click));

            Mediator.NotifyColleagues(Messages.NewGame, null);

        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.GameInitEnd:
                    Refresh(args as IEnumerable<Token>);
                    break;
                case Messages.RefreshView:
                    Refresh(args as IEnumerable<Token>);
                    break;
                case Messages.RaiseError:
                    MessageBox.Show(args as string, "Error");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region Functions
        private void Refresh(IEnumerable<Token> viewBoard)
        {
            var arr = viewBoard.ToArray();

            for (int i = 0; i < 36; i++)
                _viewBoard[i] = arr[i].GetImage();

            OnPropertyChanged("ViewBoard");
        }

        private void Turn_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var encodedCoord = Grid.GetRow(sender as UIElement) * 8 +
                Grid.GetColumn(sender as UIElement);
            Mediator.NotifyColleagues(Messages.MakeTurn, encodedCoord);
        }
        #endregion
    }
}
