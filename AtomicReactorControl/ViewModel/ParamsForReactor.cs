using AtomicReactorControl.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace AtomicReactorControl.ViewModel
{
    public class ParamsForReactor : INotifyPropertyChanged, Interfaces.IReactorParams
    {
        public Color EllipseTemperatureColor
        {
            get => _ellipseTemperatureColor;
            set
            {
                _ellipseTemperatureColor = value;
                OnPropertyChanged(nameof(EllipseTemperatureColor));
            }
        }

        public Color EllipseEnergyColor
        {
            get => _ellipseEnergyColor;
            set
            {
                _ellipseEnergyColor = value;
                OnPropertyChanged(nameof(EllipseEnergyColor));
            }
        }

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
                    OnPropertyChanged(nameof(Temperature));
                }

                if (Temperature >= 300)
                {
                    EllipseTemperatureColor = Colors.Orange;
                    OnPropertyChanged(nameof(EllipseTemperatureColor));

                }
                else
                {
                    EllipseTemperatureColor = Colors.Green;
                    OnPropertyChanged(nameof(EllipseTemperatureColor));
                }
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
                if (value >= 0)
                {
                    _storedEnergy = value;
                    OnPropertyChanged(nameof(StoredEnergy));
                }

                if (StoredEnergy >= 4000)
                {
                    EllipseEnergyColor = Colors.Green;
                    OnPropertyChanged(nameof(EllipseEnergyColor));

                }
                else
                {
                    EllipseEnergyColor = Colors.Orange;
                    OnPropertyChanged(nameof(EllipseEnergyColor));
                }
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
            EllipseTemperatureColor = Colors.Black;
            EllipseEnergyColor = Colors.Black;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private double _speedOfSplitting = 1;
        private double _temperature = 40;
        private double _fuel = 300;
        private double _powerConsumption = 0;
        private double _storedEnergy = 4500;
        private double _energyOutput = 0;
        private WorkMode _currentWorkMode;

        //indicators colors
        private Color _ellipseTemperatureColor = Colors.Black;
        private Color _ellipseEnergyColor = Colors.Black;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}