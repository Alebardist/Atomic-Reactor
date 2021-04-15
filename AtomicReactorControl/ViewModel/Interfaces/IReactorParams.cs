using AtomicReactorControl.Enums;
using System.ComponentModel;

namespace AtomicReactorControl.ViewModel.Interfaces
{
    public interface IReactorParams
    {
        WorkMode CurrentWorkMode { get; set; }
        double EnergyOutput { get; set; }
        double Fuel { get; set; }
        double PowerConsumption { get; set; }
        double SpeedOfSplitting { get; set; }
        double StoredEnergy { get; set; }
        double Temperature { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void ResetParams();
    }
}