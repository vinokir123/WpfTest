using ClearWpf.Models;
using ClearWpf.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ClearWpf.Services
{
    public class CovidDataParser : ICovidDataParser
    {
        public ILogger<CovidDataParser> Logger { get; }

        public CovidDataParser(ILogger<CovidDataParser> logger)
        {
            Logger = logger;
        }
        public DateTime[] GetDatesMas(IEnumerable<string> lines)
        {
            try
            {
                return lines.First()
                   .Split(',')
                   .Skip(4)
                   .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
                   .ToArray();
            }
            catch (Exception e)
            {
                Logger.LogError("Parse dates error!", e);
            }
            return Enumerable.Empty<DateTime>().ToArray();
        }
        public IEnumerable<string[]> GetDataRows(IEnumerable<string> lines)
        {
            return lines.Skip(1)
               .Select(line => line.Split(','));
        }
        public CountryInfoRow ParseStringsToCountryInfoRow(string[] strs)
        {
            try
            {
                var province = strs[0].Trim();
                var country_name = strs[1].Trim(' ', '"');
                var islatitude = double.TryParse(strs[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double latitude);
                var islongitude = double.TryParse(strs[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double longitude);
                var counts = strs.Skip(4).Select(int.Parse).ToArray();
                return new CountryInfoRow(province, country_name, (latitude, longitude), counts);
            }
            catch (Exception e)
            {
                Logger.LogError("Parse CountryInfoRow error!", e);
            }
            return new CountryInfoRow();
        }
        public string ReplacedRow(string line)
        {
            return line.Replace("Korea,", "Korea -")
                .Replace("Saint Helena,", "Saint Helena -")
                .Replace("Bonaire,", "Bonaire -");
        }
    }
}
