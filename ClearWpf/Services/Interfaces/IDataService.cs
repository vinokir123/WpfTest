using ClearWpf.Models;
using System.Collections.Generic;

namespace ClearWpf.Services.Interfaces
{
    public interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
