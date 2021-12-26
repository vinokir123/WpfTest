using ClearWpf.ViewModels.Base;

namespace ClearWpf.ViewModels
{
    public class MainWindowViewModel : TittledViewModel
    {
        public CountriesStatisticViewModel CountriesStatistic { get; }

        public MainWindowViewModel()
        {
            
            VmTitle = "Главное окно";
        }
        public MainWindowViewModel(CountriesStatisticViewModel statistic) : this()
        {
            this.CountriesStatistic = statistic;
            CountriesStatistic.MainModel = this;
        }
    }
}
