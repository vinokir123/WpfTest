using ClearWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
