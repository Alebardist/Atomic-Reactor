using System.ComponentModel;
using System.Runtime.CompilerServices;
using AtomicReactorControl.Enums;

namespace AtomicReactorControl.ViewModel
{
    public class ParamsForReactor : INotifyPropertyChanged, Interfaces.IReactorParams
    {
        private double _speedOfSplitting = 1;
        private double _temperature = 40;
        private double _fuel = 300;
        private double _powerConsumption = 0;
        private double _storedEnergy = 1500;
        private WorkMode _currentWorkMode;

        private double _energyOutput = 0;

        public double SpeedOfSplitting
        {
            get => _speedOfSplitting;
            set
            {
                _speedOfSplitting = value;
                OnPropertyChanged(nameof(SpeedOfSplitting));
            }
        }

        public double PowerConsumption
        {
            get => _powerConsumption;
            set
            {
                _powerConsumption = value;
                OnPropertyChanged(nameof(PowerConsumption));
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                if (value >= 0)
                {
                    _temperature = value;
                }
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public double Fuel
        {
            get => _fuel;
            set
            {
                _fuel = value;
                OnPropertyChanged(nameof(Fuel));
            }
        }

        public WorkMode CurrentWorkMode 
        { 
            get => _currentWorkMode;
            set => _currentWorkMode = value; 
        }
        public double StoredEnergy
        {
            get => _storedEnergy; 
            set
            {
                if (value < 0)
                {
                    _storedEnergy = 0;
                }
                else
                {
                    _storedEnergy = value;
                }

                OnPropertyChanged(nameof(StoredEnergy));
            }
        }

        public double EnergyOutput
        {
            get => _energyOutput; 
            set
            {
                _energyOutput = value;
                OnPropertyChanged(nameof(EnergyOutput));
            }
        }


        public void ResetParams()
        {
            SpeedOfSplitting = 0;
            PowerConsumption = 0;
            Temperature = 40;
            Fuel = 300;
            CurrentWorkMode = WorkMode.HeatWithinWork;
            StoredEnergy = 1500;
            EnergyOutput = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}