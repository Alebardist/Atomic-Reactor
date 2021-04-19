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
        /* fake initialization is necessary because
         * wpf is trying to call each method of
         * MainWindow before its constructor 
         */
        private readonly Launcher _launcher = new Launcher();

        public MainWindow()
        {
            InitializeComponent();

            IReactorParams reactorParams = (IReactorParams)this.FindResource("ResourceReactorParams");
            Reactor reactor = new Reactor(reactorParams);

            _launcher = new Launcher(reactorParams, reactor);  // real initialization
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