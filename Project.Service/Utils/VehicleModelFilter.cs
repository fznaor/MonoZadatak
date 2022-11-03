using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModelFilter
    {
        public string MakeFilter { get; set; }
        public string NameFilter { get; set; }

        public VehicleModelFilter(string makeFilter, string nameFilter)
        {
            MakeFilter = makeFilter;
            NameFilter = nameFilter;
        }
    }
}
