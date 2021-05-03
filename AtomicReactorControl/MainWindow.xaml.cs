using System.Windows;

namespace AtomicReactorControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            BTstart.IsEnabled = false;
        }

        private void BTreset_Click(object sender, RoutedEventArgs e)
        {
            BTstart.IsEnabled = true;
        }
    }
}