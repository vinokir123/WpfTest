using ClearWpf.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearWpf.Services.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<CountryInfo>> GetDataAsync();
    }
}
