using ClearWpf;
using ClearWpf.Models;
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
            _dataService = App.Host.Services.GetRequiredService<IDataService>();
        }

        [Test]
        public void GetData_IsCountryInfoCollection()
        {
            var result = _dataService.GetData();
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(CountryInfo));
        }
    }
}
