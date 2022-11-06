using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModelFilter
    {
        public int? MakeIdFilter { get; set; }
        public string NameFilter { get; set; }

        public VehicleModelFilter(int? makeIdFilter, string nameFilter)
        {
            MakeIdFilter = makeIdFilter;
            NameFilter = nameFilter;
        }
    }
}
