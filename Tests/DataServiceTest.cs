using ClearWpf;
using ClearWpf.Models;
using ClearWpf.Services;
using ClearWpf.Services.Interfaces;
using ClearWpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class DataServiceTest
    {
        private IDataService _dataService;
        
        [SetUp]
        public void SetUp()
        {
            _dataService = new DataService(App.Host.Services.GetRequiredService<ICovidDataParser>()) ;
        }

        [Test]
        public async Task GetData_IsCountryInfoCollectionAsync()
        {
            var result = await _dataService.GetDataAsync();
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(CountryInfo));
        }

        [Test]
        public async Task GetData_IsReaded()
        {
            var result = await _dataService.GetDataAsync();
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
