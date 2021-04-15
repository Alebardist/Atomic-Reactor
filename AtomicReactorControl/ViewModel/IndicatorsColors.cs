using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using AtomicReactorControl.ViewModel.Interfaces;

namespace AtomicReactorControl.ViewModel
{
    internal class IndicatorsColors : INotifyPropertyChanged, IIndicatorsColors
    {
        //indicators colors
        private SolidColorBrush _ellipseTemperatureColor = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _ellipseEnergyColor = new SolidColorBrush(Colors.Black);

        public SolidColorBrush EllipseTemperatureColor
        {
            get => _ellipseTemperatureColor;
            set
            {
                _ellipseTemperatureColor = value;
                OnPropertyChanged(nameof(EllipseTemperatureColor));
            }
        }
        public SolidColorBrush EllipseEnergyColor
        {
            get => _ellipseEnergyColor;
            set
            {
                _ellipseEnergyColor = value;
                OnPropertyChanged(nameof(EllipseEnergyColor));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}