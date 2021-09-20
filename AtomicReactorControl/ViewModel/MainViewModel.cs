using AtomicReactorControl.Enums;
using AtomicReactorControl.Model;
using AtomicReactorControl.ViewModel.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AtomicReactorControl.ViewModel
{
    internal class MainViewModel
    {
        public IReactorParams ParamsForReactor { get => _paramsForReactor; set => _paramsForReactor = value; }
        public ICommand Start { get => _start; set => _start = value; }
        public ICommand Reset { get => _reset; set => _reset = value; }
        public ICommand SetModRealTime { get => _setModRealTime; set => _setModRealTime = value; }
        public ICommand SetModByFormulae { get => _setModByFormulae; set => _setModByFormulae = value; }

        private Reactor _reactor;
        private IReactorParams _paramsForReactor;
        private ICommand _start;
        private ICommand _reset;
        private ICommand _setModRealTime;
        private ICommand _setModByFormulae;

        // cancellation tokens
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private CancellationToken _token;

        public MainViewModel()
        {
            ParamsForReactor = new ParamsForReactor();
            _reactor = new Reactor(ParamsForReactor);
            _token = _cancellationTokenSource.Token;

            _start = new RelayCommand(StartCycle);
            _reset = new RelayCommand(ResetParamsAndToken);
            SetModRealTime = new RelayCommand(SwitchReactorModToRealTime);
            SetModByFormulae = new RelayCommand(SwitchReactorModToImmediate);
        }

        public void SwitchReactorModToRealTime()
        {
            if (_paramsForReactor != null)
            {
                _paramsForReactor.CurrentWorkMode = WorkMode.HeatWithinWork;
            }
        }

        public void SwitchReactorModToImmediate()
        {
            if (_paramsForReactor != null)
            {
                _paramsForReactor.CurrentWorkMode = WorkMode.HeatByFormulae;
            }
        }

        private void StartCycle()
        {
            new Task(() => _reactor.ReactorCycleAsync(_token)).Start();
        }

        private void ResetParamsAndToken()
        {
            _cancellationTokenSource.Cancel();

            _paramsForReactor.ResetParams();
            ResetToken();
        }

        private void ResetToken()
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _token = _cancellationTokenSource.Token;
            }
        }
    }
}