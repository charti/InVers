using System.Windows;

using InVers.Control;

namespace InVers.View
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainViewControl Controller { get; private set; }
        public MainView()
        {
            Controller = new MainViewControl(this);
            InitializeComponent();
        }
    }
}
