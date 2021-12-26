using ClearWpf.Infrastructure.Commands;
using ClearWpf.Models;
using ClearWpf.Services.Interfaces;
using ClearWpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClearWpf.ViewModels
{
    public class CountriesStatisticViewModel : ViewModel
    {
        private readonly IDataService _DataService;

        public MainWindowViewModel MainModel { get; internal set; }

        #region Contries : IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> _Countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #endregion

        #region SelectedCountry : CountryInfo - Выбранная страна

        /// <summary>Выбранная страна</summary>
        private CountryInfo _SelectedCountry;

        /// <summary>Выбранная страна</summary>
        public CountryInfo SelectedCountry { get => _SelectedCountry; set => Set(ref _SelectedCountry, value); }

        #endregion
        #region IsDataLoading : IsDataLoading - отметка о загрузке данных

        /// <summary>Выбранная страна</summary>
        private bool _IsDataLoading;

        /// <summary>Выбранная страна</summary>
        public bool IsDataLoading { get => _IsDataLoading; set => Set(ref _IsDataLoading, value); }

        #endregion

        #region Команды

        public ICommand RefreshDataCommand { get; }

        private async Task OnRefreshDataCommandExecutedAsync()
        {
            IsDataLoading = true;
            Countries = await _DataService.GetDataAsync();
            IsDataLoading = false;
        }

        #endregion
        /// <summary>Отладочный конструктор, используемый в процессе разработки в визуальном дизайнере</summary>
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Вызов конструктора, не предназначенного для использования в обычном режиме");

            _Countries = Enumerable.Range(1, 10)
               .Select(i => new CountryInfo
               {
                   Name = $"Country {i}",
                   Provinces = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                   {
                       Name = $"Province {i}",
                       Location = new Point(i, j),
                       Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                       {
                           Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                           Count = k
                       }).ToArray()
                   }).ToArray()
               }).ToArray();
        }
        public CountriesStatisticViewModel(IDataService DataService)
        {
            _DataService = DataService;

            #region Команды

            RefreshDataCommand = new BaseCommandAsync(OnRefreshDataCommandExecutedAsync);

            #endregion
        }
    }
}
