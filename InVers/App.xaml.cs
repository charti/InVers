using InVers.View;
using InVers.Control;

using System.Windows;

namespace InVers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var app = (App)sender;
            var game = new GameControl();
            app.MainWindow = new MainView();
            app.MainWindow.Show();            
        }
    }
}
