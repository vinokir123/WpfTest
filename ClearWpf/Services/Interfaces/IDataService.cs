using ClearWpf.Models;
using System.Collections.Generic;

namespace ClearWpf.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
