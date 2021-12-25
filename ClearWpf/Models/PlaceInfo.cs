using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClearWpf.Models
{
    public class PlaceInfo
    {
        public string Name { get; set; }

        public virtual Point Location { get; set; }

        public virtual IEnumerable<ConfirmedCount> Counts { get; set; }

        public override string ToString() => $"{Name}({Location})";
    }
}
