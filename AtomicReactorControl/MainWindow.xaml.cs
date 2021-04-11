using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AtomicReactorControl.Model;
using AtomicReactorControl.Model.Enums;


namespace AtomicReactorControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IReactorParams _reactorParams;
        Reactor _rb;
        Task _launch;

        static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        CancellationToken _token = _cancelTokenSource.Token;

        public MainWindow()
        {
            InitializeComponent();
            _reactorParams = (IReactorParams)this.FindResource("ResourceReactorParams");
            _rb = new Reactor(_reactorParams);
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            // ? push this "if" block to separate method named ResetToken ?
            if (_cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource = new CancellationTokenSource();
                _token = _cancelTokenSource.Token;
            }

            _launch = new Task(() => _rb.ReactorCycle(_token));
            _launch.Start();
        }

        private void BTreset_Click(object sender, RoutedEventArgs e)
        {
            _cancelTokenSource.Cancel();

            _reactorParams.Temperature = 30;
            _reactorParams.Fuel = 300;
            SliderSplitting.Value = 1;

        }

        private void RadioButton_mod1_Checked(object sender, RoutedEventArgs e)
        {
            if (_reactorParams != null)
            {
                _reactorParams.CurrentWorkMode = WorkMode.HeatWithinWork;
            }
        }

        private void RadioButton_mod2_Checked(object sender, RoutedEventArgs e)
        {
            if (_reactorParams != null)
            {
                _reactorParams.CurrentWorkMode = WorkMode.HeatByFormulae;
            }
        }
    }
}