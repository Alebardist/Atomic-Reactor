using AtomicReactorControl.Model.Interfaces;
using AtomicReactorControl.ViewModel.Interfaces;
using System.Windows.Media;

namespace AtomicReactorControl.Model
{
    internal class IndicatorUpdater : IIndicatorUpdater
    {
        private SolidColorBrush _ellipseTemperatureColor = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush _ellipseEnergyColor = new SolidColorBrush(Colors.Blue);
        private IIndicatorsColors _indicatorsColors;

        public SolidColorBrush EllipseTemperatureColor
        {
            get => _ellipseTemperatureColor;
            set
            {
                _ellipseTemperatureColor = value;
            }
        }
        public SolidColorBrush EllipseEnergyColor
        {
            get => _ellipseEnergyColor;
            set
            {
                _ellipseEnergyColor = value;
            }
        }

        //TODO: IndicatorsColors replace with interface
        public IndicatorUpdater(IIndicatorsColors indicatorsColors)
        {
            this.EllipseTemperatureColor = indicatorsColors.EllipseTemperatureColor;
            this.EllipseEnergyColor = indicatorsColors.EllipseEnergyColor;
            _indicatorsColors = indicatorsColors;

            Reactor.OnHighTemperature += UpdateEllipseTemperatureColor;
        }

        ~IndicatorUpdater()
        {
            Reactor.OnHighTemperature -= UpdateEllipseTemperatureColor;
        }

        private void UpdateEllipseTemperatureColor()
        {
            _indicatorsColors.EllipseTemperatureColor = new SolidColorBrush(Colors.Yellow);
        }

        private void UpdateEllipseEnergyColor()
        {
            _indicatorsColors.EllipseEnergyColor = new SolidColorBrush(Colors.Yellow);
        }

        //unused
        private void ResetColors()
        {
            EllipseTemperatureColor = new SolidColorBrush(Colors.Blue);
            EllipseEnergyColor = new SolidColorBrush(Colors.Blue);
        }
    }
}