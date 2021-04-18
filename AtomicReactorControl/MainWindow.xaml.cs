using AtomicReactorControl.Model;
using AtomicReactorControl.ViewModel.Interfaces;
using System.Windows;

namespace AtomicReactorControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Launcher _launcher = new Launcher();

        public MainWindow()
        {
            InitializeComponent();
            //TODO: get rid of double call " (IReactorParams)this.FindResource("ResourceReactorParams") "
            //create a variable IReactorParams _reactorParams ?
            _launcher.ReactorParams = (IReactorParams)this.FindResource("ResourceReactorParams");
            _launcher.Reactor = new Reactor((IReactorParams)this.FindResource("ResourceReactorParams"));
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            _launcher.RunTaskReactorCycleAndResetToken();

            BTstart.IsEnabled = false;
        }

        private void BTreset_Click(object sender, RoutedEventArgs e)
        {
            _launcher.CancelTaskOfReactorCycleAndResetReactorParams();

            BTstart.IsEnabled = true;
        }

        private void RadioButton_mod1_Checked(object sender, RoutedEventArgs e)
        {
            _launcher.SwitchReactorModToRealTime();
        }

        private void RadioButton_mod2_Checked(object sender, RoutedEventArgs e)
        {
            _launcher.SwitchReactorModToImmediate();
        }
    }
}