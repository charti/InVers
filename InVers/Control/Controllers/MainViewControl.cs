using InVers.Model;
using InVers.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InVers.Control
{
    public class MainViewControl : BaseController
    {
        #region Properties / Members

        bool _stopAI = false;

        bool _isPlayerOne = true;
        public bool IsPlayerOne
        {
            get { return _isPlayerOne; }
            private set { _isPlayerOne = value; OnPropertyChanged("IsPlayerOne"); }
        }

        int _score = 0;
        public int Score 
        { 
            get { return _score; } 
            private set { _score = value; OnPropertyChanged("Score"); }
        }

        private ImageBrush[] _viewBoard = new ImageBrush[36];
        public ImageBrush[] ViewBoard
        {
            get { return _viewBoard; }
            private set { _viewBoard = value; }
        }

        private Brush[] _tokenColors;
        public Brush[] TokenColors
        {
            get { return _tokenColors; }
            private set { _tokenColors = value; OnPropertyChanged("TokenColors"); }
        }

        #endregion
        
        public MainViewControl()
        {
            Mediator.Register(this, new[] {
                Messages.GameInitEnd,
                Messages.RefreshView,
                Messages.RaiseError,
                Messages.RefreshScore,
                Messages.AITurn,
                Messages.NotifyWinner
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.Turn, Turn_Click));
        }
        
        private async void WaitBeforeTurn()
        {
            await Task.Delay(50);
            if(!_stopAI) 
                Mediator.NotifyColleagues(Messages.MakeTurn, null);
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.GameInitEnd:
                    Refresh(args as IEnumerable<Token>);
                    _stopAI = false;
                    break;
                case Messages.RefreshView:
                    Refresh(args as IEnumerable<Token>);
                    break;
                case Messages.RaiseError:
                    MessageBox.Show(args as string, "Error");
                    break;
                case Messages.RefreshScore:
                    Score = (int)args;
                    break;
                case Messages.NotifyCurrent:

                    break;
                case Messages.AITurn:
                    WaitBeforeTurn();
                    break;
                case Messages.NotifyWinner:
                    _stopAI = true;
                    var strWinner = ((Player)args).Color == PlayerColor.red ?
                        "The Winner is: Red" : "The Winner is Yellow";
                    MessageBox.Show(strWinner, "Congratulations");
                    new ConfigView();
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
