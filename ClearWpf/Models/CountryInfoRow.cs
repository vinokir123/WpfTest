using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearWpf.Models
{
    public struct CountryInfoRow
    {
        public string Province;
        public string Country;
        public (double Lat, double Lon) Place;
        public int[] Counts;

        public CountryInfoRow(string province, string country, (double Lat, double Lon) place, int[] counts)
        {
            Province = province;
            Country = country;
            Place = place;
            Counts = counts;
        }
    }
}
