using ClearWpf.Models;
using System;
using System.Collections.Generic;

namespace ClearWpf.Services.Interfaces
{
    public interface ICovidDataParser
    {
        DateTime[] GetDatesMas(IEnumerable<string> lines);
        IEnumerable<string[]> GetDataRows(IEnumerable<string> lines);
        CountryInfoRow ParseStringsToCountryInfoRow(string[] strs);
        string ReplacedRow(string line);

    }
}
