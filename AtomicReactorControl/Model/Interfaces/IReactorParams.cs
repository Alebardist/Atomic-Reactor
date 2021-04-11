using AtomicReactorControl.Model.Enums;

namespace AtomicReactorControl.Model
{
    public interface IReactorParams
    {
        double Fuel { get; set; }
        double SpeedOfSplitting { get; set; }
        double Temperature { get; set; }
        double PowerConsumption { get; set; }
        double StoredEnergy { get; set; }
        WorkMode CurrentWorkMode { get; set; }
    }
}