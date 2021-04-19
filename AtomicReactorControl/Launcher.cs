using AtomicReactorControl.Enums;
using AtomicReactorControl.Model;
using AtomicReactorControl.ViewModel.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace AtomicReactorControl
{
    internal class Launcher
    {
        public IReactorParams ReactorParams { get; set; }
        public Reactor Reactor { get; set; }

        private static CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        public Launcher(){}

        public Launcher(IReactorParams reactorParams, Reactor reactor)
        {
            ReactorParams = reactorParams;
            Reactor = reactor;
        }

        public void RunTaskReactorCycleAndResetToken()
        {
            ResetToken();
            new Task(() => Reactor.ReactorCycle(_token)).Start();
        }

        public void CancelTaskOfReactorCycleAndResetReactorParams()
        {
            _cancelTokenSource.Cancel();
            ReactorParams.ResetParams();
        }

        public void SwitchReactorModToRealTime()
        {
            if (ReactorParams != null)
            {
                ReactorParams.CurrentWorkMode = WorkMode.HeatWithinWork;
            }
        }

        public void SwitchReactorModToImmediate()
        {
            if (ReactorParams != null)
            {
                ReactorParams.CurrentWorkMode = WorkMode.HeatByFormulae;
            }
        }

        private void ResetToken()
        {
            if (_cancelTokenSource == null || _cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource = new CancellationTokenSource();
                _token = _cancelTokenSource.Token;
            }
        }
    }

        

        
}