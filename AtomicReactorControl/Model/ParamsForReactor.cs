using System.ComponentModel;
using System.Runtime.CompilerServices;

using AtomicReactorControl.Model.Enums;
namespace AtomicReactorControl.Model
{
    public class ParamsForReactor : INotifyPropertyChanged, IReactorParams
    {
        private double _speedOfSplitting = 1;
        private double _temperature = 40;
        private double _fuel = 300;
        private double _powerConsumption = 0;
        private double _storedEnergy = 555;
        private WorkMode _currentWorkMode;

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
                _temperature = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}