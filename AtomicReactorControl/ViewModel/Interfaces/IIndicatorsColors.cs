using System.Windows.Media;

namespace AtomicReactorControl.ViewModel.Interfaces
{
    internal interface IIndicatorsColors
    {
        SolidColorBrush EllipseEnergyColor { get; set; }
        SolidColorBrush EllipseTemperatureColor { get; set; }
    }
}