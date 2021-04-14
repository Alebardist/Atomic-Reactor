using AtomicReactorControl.Enums;

namespace AtomicReactorControl.ViewModel.Interfaces
{
    public interface IReactorParams
    {
        double Fuel { get; set; }
        double SpeedOfSplitting { get; set; }
        double Temperature { get; set; }
        double PowerConsumption { get; set; }
        double StoredEnergy { get; set; }
        WorkMode CurrentWorkMode { get; set; }
        double EnergyOutput { get; set; }

        void ResetParams();
    }
}