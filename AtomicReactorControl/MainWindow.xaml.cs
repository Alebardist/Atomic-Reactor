using AtomicReactorControl.Enums;
using AtomicReactorControl.Model;
using AtomicReactorControl.Model.Interfaces;
using AtomicReactorControl.ViewModel;
using AtomicReactorControl.ViewModel.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AtomicReactorControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IReactorParams _reactorParams;
        private IIndicatorsColors _indicatorsColors = new IndicatorsColors();
        
        private Reactor _reactor;
        private IIndicatorUpdater _indicatorUpdater;

        private static CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        public MainWindow()
        {
            InitializeComponent();
            _reactorParams = (IReactorParams)this.FindResource("ResourceReactorParams");
            _indicatorsColors = (IIndicatorsColors)this.FindResource("ResourceIndicatorsColors");

            _reactor = new Reactor(_reactorParams);
            _indicatorUpdater = new IndicatorUpdater(_indicatorsColors);
        }

        private void ResetToken()
        {
            if (_cancelTokenSource == null || _cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource = new CancellationTokenSource();
                _token = _cancelTokenSource.Token;
            }
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            ResetToken();
            new Task(() => _reactor.ReactorCycle(_token)).Start();
            BTstart.IsEnabled = false;
        }

        private void BTreset_Click(object sender, RoutedEventArgs e)
        {
            _cancelTokenSource.Cancel();
            _reactorParams.ResetParams();
            BTstart.IsEnabled = true;
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