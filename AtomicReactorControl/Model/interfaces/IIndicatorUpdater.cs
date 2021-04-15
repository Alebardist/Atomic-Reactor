using System.Windows.Media;

namespace AtomicReactorControl.Model.Interfaces
{
    internal interface IIndicatorUpdater
    {
        SolidColorBrush EllipseEnergyColor { get; set; }
        SolidColorBrush EllipseTemperatureColor { get; set; }
    }
}