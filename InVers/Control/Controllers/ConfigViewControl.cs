using System;
using System.Windows.Input;

namespace InVers.Control
{
    public class ConfigViewControl : BaseController
    {
        #region Properties / Members
        private int _combo1 = 0;
        private int _combo2 = 1;
        public int Player1Kind
        {
            get { return _combo1; }
            set { _combo1 = value; OnPropertyChanged("Player1Kind"); }
        }
        public int Player2Kind
        {
            get { return _combo2; }
            set { _combo2 = value; OnPropertyChanged("Player2Kind"); }
        }
        #endregion

        public ConfigViewControl()
        {
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.StartGame, start_Click));
        }

        #region Functions
        private void start_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Mediator.NotifyColleagues(Messages.NewGame, new[] { Player1Kind, Player2Kind });
            ((System.Windows.Window)e.Parameter).Close();
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}
