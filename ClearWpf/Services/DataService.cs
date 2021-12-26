using ClearWpf.Models;
using ClearWpf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace ClearWpf.Services
{
    public class DataService : IDataService
    {
        private const string __DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        public ICovidDataParser Parser { get; }

        public DataService(ICovidDataParser parser)
        {
            Parser = parser;
        }
        private async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();

            var response = await client.GetAsync(
                __DataSourceAddress);
            //await Task.Delay(5000);
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private async Task<IEnumerable<string>> GetDataLinesAsync()
        {
            using (var data_stream = await GetDataStream())
            {
                using (var data_reader = new StreamReader(data_stream))
                {
                    List<string> results = new List<string>();
                    while (!data_reader.EndOfStream)
                    {
                        var line = data_reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        results.Add(Parser.ReplacedRow(line));
                    }
                    return results;
                }
            }
        }
        private async Task<DateTime[]> GetDates() => Parser.GetDatesMas(await GetDataLinesAsync());

        private async Task<IEnumerable<CountryInfoRow>> GetCountriesDataAsync() => Parser.GetDataRows(await GetDataLinesAsync())
            .Select(row => Parser.ParseStringsToCountryInfoRow(row));

        public async Task<IEnumerable<CountryInfo>> GetDataAsync()
        {
            var dates = await GetDates();

            var data = (await GetCountriesDataAsync()).GroupBy(d => d.Country);

            List<CountryInfo> countryInfos = new List<CountryInfo>();
            foreach (var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    Provinces = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                countryInfos.Add(country);
            }
            return countryInfos;
        }
    }


}
