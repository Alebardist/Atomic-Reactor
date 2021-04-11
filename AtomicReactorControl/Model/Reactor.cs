using AtomicReactorControl.Properties;
using System;
using System.Diagnostics;
using System.Threading;

namespace AtomicReactorControl.Model
{
    public class Reactor
    {
        private const double EnergyOutputModificator = 5;
        private const double FuelEfficiency = 0.05;
        private const double Coolant = 10;

        private readonly IReactorParams _reactorParams;

        private double _temperature;

        public double Temperature
        {
            get => _temperature;
            set
            {
                if (value > 0)
                {
                    _temperature = value;
                }
                else
                {
                    _temperature = 0;
                }
            }
        }

        private int _speedOfSplitting = 0;

        public int SpeedOfSplitting
        {
            get { return _speedOfSplitting; }
            set
            {
                if (value >= 0)
                {
                    _speedOfSplitting = value;
                }
                else
                {
#if TRACE
                    Debug.WriteLine($"{value} is < 0");
#endif
                    throw new ArgumentOutOfRangeException($"{nameof(_reactorParams.SpeedOfSplitting)}", Resources.Reactor_SpeedOfSplitting_Can_t_be_lower_0);
                }
            }
        }

        private double _fuelCount = 80;

        public double FuelCount
        {
            get => _fuelCount;
            set
            {
                if (value >= 0)
                {
                    _fuelCount = value;
                }
            }
        }

        private double _storedEnergy;

        public double StoredEnergy
        {
            get => _storedEnergy;
            set
            {
                if (value >= 0)
                {
                    _storedEnergy = 0;
                }
            }
        }

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
            while (Temperature < 380 && FuelCount >= FuelEfficiency * _reactorParams.SpeedOfSplitting)
            {
                if (token.IsCancellationRequested)
                {
#if TRACE
                    Debug.WriteLine("Operation interrupted by token");
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
            Debug.WriteLine($"{Temperature} is > 380");
#endif
        }

        private void SetReactorParams()
        {
            _reactorParams.Temperature = Temperature;
            _reactorParams.Fuel = FuelCount;
        }

        private void ReadReactorParams()
        {
            Temperature = _reactorParams.Temperature;
            FuelCount = _reactorParams.Fuel;
        }

        private void ComputeEnergyOutput()
        {
            _reactorParams.StoredEnergy += EnergyOutputModificator * _reactorParams.SpeedOfSplitting;
        }

        private void ComputeEnergyConsumption()
        {
            _reactorParams.StoredEnergy -= _reactorParams.PowerConsumption;
        }

        private void ComputeTemperatureIncrease()
        {
            if (_reactorParams.CurrentWorkMode == Enums.WorkMode.HeatWithinWork)
            {
                Temperature += _reactorParams.SpeedOfSplitting;
            }

            // 2nd mode
            else if (_reactorParams.CurrentWorkMode == Enums.WorkMode.HeatByFormulae)
            {
                Temperature = _reactorParams.SpeedOfSplitting;
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
            FuelCount -= FuelEfficiency * _reactorParams.SpeedOfSplitting;
        }

        private void ComputeCoolant()
        {
            Temperature -= Coolant;
        }
    }
}