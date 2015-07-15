using InVers.Control;
using System.Windows;

namespace InVers.View
{
    /// <summary>
    /// Interaktionslogik für ConfigView.xaml
    /// </summary>
    public partial class ConfigView : Window
    {
        public static ConfigViewControl Controller = new ConfigViewControl();
        public ConfigView()
        {
            DataContext = Controller;
            InitializeComponent();
            this.ShowDialog();
        }
    }
}
