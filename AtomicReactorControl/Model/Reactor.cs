using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AtomicReactorControl.ViewModel.Interfaces;

namespace AtomicReactorControl.Model
{
    public class Reactor
    {
        private const double _energyOutputModificator = 5;
        private const double _fuelEfficiency = 0.05;
        private const double _coolant = 10;

        private readonly IReactorParams _reactorParams;

        public Reactor(IReactorParams reactorParams)
        {
            this._reactorParams = reactorParams;
        }

        /// <summary>
        /// starts reactor cycle
        /// </summary>
        /// <param name="token">CancellationToken</param>
        public void ReactorCycle(CancellationToken token)
        {
            //set starting parameters
            ReadReactorParams();
            while (_reactorParams.Temperature < 380 && _reactorParams.Fuel >= _fuelEfficiency * _reactorParams.SpeedOfSplitting)
            {
                if (token.IsCancellationRequested)
                {
#if TRACE
                    Debug.WriteLine("ReactorCycle interrupted by token");
#endif
                    return;
                }

                ReadReactorParams();
                ComputeEnergyOutput();
                ComputeEnergyConsumption();
                ComputeTemperatureIncrease();
                ComputeFuelConsumption();
                ComputeCoolant();
                SetReactorParams();

                // pause between cycles
                Thread.Sleep(100);
            }

#if TRACE
            if (_reactorParams.Temperature > 380)
            {
                Debug.WriteLine($"{_reactorParams.Temperature} is > 380");
            }
            if (_reactorParams.Fuel <= 0)
            {
                Debug.WriteLine($"{_reactorParams.Fuel} is <= 0");
            }
#endif
        }

        private void SetReactorParams()
        {
            _reactorParams.Temperature = _reactorParams.Temperature;
            _reactorParams.Fuel = _reactorParams.Fuel;
            _reactorParams.EnergyOutput = _energyOutputModificator * _reactorParams.SpeedOfSplitting;
        }

        private void ReadReactorParams()
        {
            _reactorParams.Temperature = _reactorParams.Temperature;
            _reactorParams.Fuel = _reactorParams.Fuel;
        }

        private void ComputeEnergyOutput()
        {
            _reactorParams.StoredEnergy += _energyOutputModificator * _reactorParams.SpeedOfSplitting;
        }

        private void ComputeEnergyConsumption()
        {
            _reactorParams.StoredEnergy -= _reactorParams.PowerConsumption;
        }

        private void ComputeTemperatureIncrease()
        {
            if (_reactorParams.CurrentWorkMode == Enums.WorkMode.HeatWithinWork)
            {
                _reactorParams.Temperature += _reactorParams.SpeedOfSplitting;
            }

            // 2nd mode
            else if (_reactorParams.CurrentWorkMode == Enums.WorkMode.HeatByFormulae)
            {
                _reactorParams.Temperature = _reactorParams.SpeedOfSplitting;
            }
#if TRACE
            else
            {
                Debug.WriteLine($"reactorParams.CurrentWorkMode: {_reactorParams.CurrentWorkMode}");
                throw new ArgumentOutOfRangeException($"{nameof(_reactorParams.CurrentWorkMode)}");
            }
#endif
        }

        private void ComputeFuelConsumption()
        {
            _reactorParams.Fuel -= _fuelEfficiency * _reactorParams.SpeedOfSplitting;
        }

        private void ComputeCoolant()
        {
            _reactorParams.Temperature -= _coolant;
        }
    }
}